// <copyright file="Program.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using Diplom.Core.Controllers;
using Diplom.Core.Data;
using Diplom.Core.Data.Entities;
using Diplom.Core.Services;
using Diplom.Core.Services.Email;

using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using NLog;
using NLog.Web;

using Quartz;

// Early init of NLog to allow startup and exception logging, before host is built
// Loads nlog configuration. Loads either nlog.config or nlog.{environment}.config.
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("init main");

    var builder = WebApplication.CreateBuilder(args);

    // Setup NLog.
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    });

    // Add services to the container.
    builder.Services.AddRazorPages()
        .AddRazorRuntimeCompilation()
        .AddRazorPagesOptions(options =>
        {
            options.Conventions
                .AuthorizeFolder("/")
                .AuthorizeAreaFolder("Admin", "/", "RequireAdminRole")
                .AuthorizeAreaFolder("Employee", "/", "RequireEmployeeUserRole")
                .AllowAnonymousToAreaFolder("Identity", "/")
                .AllowAnonymousToPage("/InitDatabase");
        });

    // This prevents russian text to be written similar to &#x441;.
    builder.Services.AddWebEncoders(o =>
    {
        o.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
    });

    // Configure ASP.NET identity core as per
    // https://stackoverflow.com/questions/51004516/net-core-2-1-identity-get-all-users-with-their-associated-roles/51005445#51005445
    // https://github.com/aspnet/Identity/issues/1361#issuecomment-419612809
    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Stores.MaxLengthForKeys = 128;
            options.Password.RequireNonAlphanumeric = false;
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(ApplicationRole.SystemAdmin));
        options.AddPolicy("RequireEmployeeUserRole", policy => policy.RequireRole(ApplicationRole.SystemAdmin, ApplicationRole.EmployeeUser));
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllersWithViews();

    // Email sender.
    var emailConfig = new EmailConfiguration();

    builder.Configuration.Bind("EmailSettings", emailConfig);

    builder.Services.AddSingleton(emailConfig);

    builder.Services.AddSingleton<EmailService>();

    builder.Services.AddSingleton<IEmailSender, EmailService>();

    builder.Services.AddScoped<EmailTemplatingService>();

    // This enables enqueuing tasks for backround processing.
    // To use get IBackgroundTaskQueue via DI (add IBackgroundTaskQueue backgroundTaskQueue parameter to construtor) and call QueueBackgroundWorkItem to enqueue work item for background processing.
    // this.backgroundTaskQueue.QueueBackgroundWorkItem(async stoppingToken =>
    // {
    //    // Work to do in background. Be careful that captured variables may be disposed.
    // });
    // Unhandled exception in work item will be caught and logged.
    // See also https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services.
    builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
    builder.Services.AddHostedService<QueuedTasksBackgroundExecutorHostedService>();

    builder.Services.AddScoped<MainController>();

    // Add task scheduler (quartz).
    builder.Services.AddQuartz(q =>
    {
        // Base quartz scheduler, job and trigger configuration.
        q.UseMicrosoftDependencyInjectionJobFactory();

        // Job that writes to log. Used for testing.
        ////q.ScheduleJob<LogWriterJob>(trigger => trigger
        ////    .WithIdentity(nameof(LogWriterJob))
        ////    .WithSimpleSchedule(x => x.WithIntervalInSeconds(15).RepeatForever()));
    });

    // Quartz ASP.NET Core hosting.
    builder.Services.AddQuartzServer(options =>
    {
        // When shutting down we want jobs to complete gracefully.
        options.WaitForJobsToComplete = true;

        // Delay scheduler start slightly to improve startup performance.
        ////options.StartDelay = TimeSpan.FromSeconds(15);
    });

    // Remove x-powered-by header.
    builder.WebHost.ConfigureKestrel(option => option.AddServerHeader = false);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");

        // The default HSTS value is 30 days. See https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseStaticFiles();

    // Uncomment this to enable static file caching on client with no asking server if file has changed.
    ////app.UseStaticFiles(new StaticFileOptions
    ////{
    ////    // Enable static file caching on client with no asking server if file has changed.
    ////    OnPrepareResponse = staticFileResponseContext =>
    ////    {
    ////        staticFileResponseContext.Context.Response.Headers.Append("Cache-Control", $"public, max-age={365 * 24 * 60 * 60}");
    ////    },
    ////});

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    app.MapControllers();

    var supportedCultures = new[] { "ru-RU" };
    var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    app.UseRequestLocalization(localizationOptions);

    // Email exceptions.
    app.Use(async (context, next) =>
    {
        try
        {
            await next.Invoke();
        }
        catch (ConnectionResetException)
        {
            // Do nothing. This is expected exception when client stops TCP session during upload. This happens. It's normal. There is no need to report it.
        }
        catch (Exception e)
        {
            CreateAndSendExceptionMessage(app, context, e);

            throw;
        }
    });

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors.
    logger.Error(exception, "Stopped program because of exception.");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}

static void CreateAndSendExceptionMessage(IApplicationBuilder app, HttpContext context, Exception ex)
{
    var userName = "Unknown";
    if (context.User.Identity != null && context.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(context.User.Identity.Name))
    {
        userName = context.User.Identity.Name;
    }

    var sb = new StringBuilder($"An error has occurred on {context.Request.Host}. <br><br>");
    sb.Append($"Method = {context.Request.Method}<br>");
    sb.Append($"Url = {context.Request.GetDisplayUrl()}<br>");
    sb.Append($"User = {userName}<br><br>");

    sb.Append($"Headers:<br>");
    foreach (var header in context.Request.Headers)
    {
        sb.Append($"<small>'{header.Key}' = {header.Value}</small><br>");
    }

    sb.Append($"<br>");

    var exceptionStringHtml = ex.ToString();

    sb.Append($"Exception Source = {ex.Source} <br>");
    sb.Append($"Exception: <pre>{exceptionStringHtml}</pre><br><br>");

    // Send email only if email service is available.
    var emailServiceObject = app.ApplicationServices.GetService(typeof(EmailService));
    if (emailServiceObject != null)
    {
        var emailService = (EmailService)emailServiceObject;

        if (emailService.EmailConfig != null && !string.IsNullOrWhiteSpace(emailService.EmailConfig.EmailTo))
        {
            var mailHtml = sb.ToString();

            mailHtml = mailHtml.Replace("line ", "<b>line </b>", StringComparison.InvariantCulture);

            _ = emailService.SendEmailAsync(emailService.EmailConfig.EmailTo, $"Exception: {context.Request.Host}.", mailHtml, ignoreExceptions: true);
        }
    }
}