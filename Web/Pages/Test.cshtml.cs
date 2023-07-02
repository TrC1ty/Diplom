// <copyright file="Test.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Pages
{
    using Diplom.Core.Diagnostics;
    using Diplom.Core.Services;
    using Diplom.Core.Services.Email;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for Test page.
    /// </summary>
    public class TestModel : PageModel
    {
        private readonly EmailTemplatingService emailTemplatingService;
        private readonly IEmailSender emailSender;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestModel"/> class.
        /// </summary>
        /// <param name="emailTemplatingService">Email templating service.</param>
        /// <param name="emailSender">Email service.</param>
        /// <param name="backgroundTaskQueue">Background task service.</param>
        public TestModel(EmailTemplatingService emailTemplatingService, IEmailSender emailSender, IBackgroundTaskQueue backgroundTaskQueue)
        {
            this.emailTemplatingService = emailTemplatingService;
            this.emailSender = emailSender;
            this.backgroundTaskQueue = backgroundTaskQueue;
        }

        /// <summary>
        /// Gets success message.
        /// </summary>
        public string? SuccessMessage { get; private set; }

        /// <summary>
        /// Automatically executed as a result of a POST request when SimulateException page handler is specified.
        /// </summary>
        public void OnPostSimulateException()
        {
            throw new GeneralException("Test exception.");
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when SendTestEmail page handler is specified.
        /// </summary>
        public void OnPostSendTestEmail()
        {
            this.backgroundTaskQueue.QueueBackgroundWorkItem(async stoppingToken =>
            {
                var emailBody = await this.emailTemplatingService.GenerateHtml("CustomerFeedback", DateTime.Now);
                await this.emailSender.SendEmailAsync("DeveloperNotifications@1d61.com", "Test email", emailBody);
            });

            this.SuccessMessage = "Message enqueued for delivery.";
        }
    }
}
