// <copyright file="EmailConfiguration.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services.Email
{
    /// <summary>
    /// Email configuration.
    /// </summary>
    public class EmailConfiguration
    {
        /// <summary>
        /// Gets or sets mail server.
        /// </summary>
        public string? MailServer { get; set; }

        /// <summary>
        /// Gets or sets mail server username.
        /// </summary>
        public string? MailServerUsername { get; set; }

        /// <summary>
        /// Gets or sets mail server password.
        /// </summary>
        public string? MailServerPassword { get; set; }

        /// <summary>
        /// Gets or sets email from.
        /// </summary>
        public string? EmailFrom { get; set; }

        /// <summary>
        /// Gets or sets email to.
        /// </summary>
        public string? EmailTo { get; set; }
    }
}
