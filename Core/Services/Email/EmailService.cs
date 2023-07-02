// <copyright file="EmailService.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services.Email
{
    using System;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;

    using MimeKit;

    /// <summary>
    /// Service that sends emails.
    /// </summary>
    public class EmailService : IEmailSender
    {
        private readonly ILogger<EmailService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="emailConfig">Email configuration service.</param>
        /// <param name="logger">Logging service.</param>
        public EmailService(EmailConfiguration emailConfig, ILogger<EmailService> logger)
        {
            this.EmailConfig = emailConfig;
            this.logger = logger;
        }

        /// <summary>
        /// Gets email configuration.
        /// </summary>
        public EmailConfiguration EmailConfig { get; }

        /// <summary>
        /// Sends email.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="htmlMessage">Message body in HTML format.</param>
        /// <returns>Async method result task.</returns>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return this.SendEmailAsync(email, subject, htmlMessage, ignoreExceptions: false);
        }

        /// <summary>
        /// Sends email.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="htmlMessage">Message body in HTML format.</param>
        /// <param name="ignoreExceptions">Specifies that exceptions that occure during send should be ignored.</param>
        /// <returns>Async method result task.</returns>
        public virtual async Task SendEmailAsync(string email, string subject, string htmlMessage, bool ignoreExceptions)
        {
            await this.SendEmailAsync(new string[] { email }, subject, htmlMessage, ignoreExceptions);
        }

#pragma warning disable SA1011 // This is clearly a StyleCop bug.

        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="emails">Email addresses to send to.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="htmlMessage">Message body in HTML format.</param>
        /// <param name="ignoreExceptions">Specifies that exceptions that occure during send should be ignored.</param>
        /// <param name="bccEmails">"Blind carbon copy" email address.</param>
        /// <returns>Async method result task.</returns>
        public virtual async Task SendEmailAsync(string[] emails, string subject, string htmlMessage, bool ignoreExceptions = false, string[]? bccEmails = null)
        {
            if (emails is null)
            {
                throw new ArgumentNullException(nameof(emails));
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or empty", nameof(subject));
            }

            if (string.IsNullOrEmpty(htmlMessage))
            {
                throw new ArgumentException($"'{nameof(htmlMessage)}' cannot be null or empty", nameof(htmlMessage));
            }

            this.logger.LogInformation("Attempt to send email to [{emails}] with subject '{subject}'. Parameter ignoreExceptions is '{ignoreExceptions}'.", string.Join(", ", emails), subject, ignoreExceptions);

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(string.Empty, this.EmailConfig.EmailFrom));

            foreach (string email in emails)
            {
                emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            }

            if (bccEmails != null)
            {
                foreach (string email in bccEmails)
                {
                    emailMessage.Bcc.Add(new MailboxAddress(string.Empty, email));
                }
            }

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage,
            };

            using var client = new SmtpClient();

            try
            {
#pragma warning disable CA5359 // Do Not Disable Certificate Validation. It's not a problem with emails. They are not secret.
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(this.EmailConfig.MailServer, 587, false);
                await client.AuthenticateAsync(this.EmailConfig.MailServerUsername, this.EmailConfig.MailServerPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Exception caught sending email.");

                if (!ignoreExceptions)
                {
                    throw;
                }
            }
        }
    }
}
