// <copyright file="JobExceptionListener.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.SchedulerJobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Diplom.Core.Services.Email;

    using Microsoft.Extensions.Logging;

    using Quartz;
    using Quartz.Listener;

    /// <summary>
    /// When configured listens for exceptions in scheduled jobs and reports them.
    /// </summary>
    public class JobExceptionListener : JobListenerSupport
    {
        private readonly EmailService emailService;
        private readonly ILogger<JobExceptionListener> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobExceptionListener"/> class.
        /// </summary>
        /// <param name="emailService">Email service.</param>
        /// <param name="logger">Logging service.</param>
        public JobExceptionListener(EmailService emailService, ILogger<JobExceptionListener> logger)
        {
            this.emailService = emailService;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override string Name => nameof(JobExceptionListener);

        /// <inheritdoc/>
        public override async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            // According to quartz documentation job listeners should not throw exceptions under no circumstances.
            try
            {
                if (jobException != null)
                {
                    this.logger.LogWarning("JobExceptionListener detected unhandled exception during job execution and will attempt to report it.");

                    var sb = new StringBuilder($"An exception was caught using {this.GetType().FullName}. <br><br>");
                    sb.Append($"{context}<br><br>");
                    sb.Append($"Exception: <pre>{jobException}</pre><br><br>");

                    if (this.emailService.EmailConfig != null && !string.IsNullOrWhiteSpace(this.emailService.EmailConfig.EmailTo))
                    {
                        var mailHtml = sb.ToString();

                        mailHtml = mailHtml.Replace("line ", "<b>line </b>", StringComparison.InvariantCulture);

                        await this.emailService.SendEmailAsync(this.emailService.EmailConfig.EmailTo, $"Exception: {this.GetType().FullName}.", mailHtml);
                    }
                }
            }
            catch
            {
                // Do nothing.
            }
        }
    }
}
