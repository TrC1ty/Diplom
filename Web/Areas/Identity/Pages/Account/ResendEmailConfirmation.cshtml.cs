// <copyright file="ResendEmailConfirmation.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Diplom.Core.Data.Entities;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for ResendEmailConfirmation page.
    /// </summary>
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResendEmailConfirmationModel"/> class.
        /// </summary>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="emailSender">Email sending service.</param>
        public ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        /// <summary>
        /// Gets or sets page input model.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Automatically executed as a result of a POST request.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await this.userManager.FindByEmailAsync(this.Input.Email);
            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return this.Page();
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = this.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId, code },
                protocol: this.Request.Scheme);
            await this.emailSender.SendEmailAsync(
                this.Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            this.ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return this.Page();
        }

        /// <summary>
        /// Page input model class.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets user email address.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
