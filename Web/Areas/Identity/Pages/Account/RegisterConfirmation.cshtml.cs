// <copyright file="RegisterConfirmation.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Text;
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
    /// Page model class for RegisterConfirmation page.
    /// </summary>
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterConfirmationModel"/> class.
        /// </summary>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="sender">Email sending service.</param>
        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender)
        {
            this.userManager = userManager;
            this.sender = sender;
        }

        /// <summary>
        /// Gets or sets user email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether confirm account link should be displayed.
        /// </summary>
        public bool DisplayConfirmAccountLink { get; set; }

        /// <summary>
        /// Gets or sets email confirmation URL.
        /// </summary>
        public string EmailConfirmationUrl { get; set; }

        /// <summary>
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return this.RedirectToPage("/Index");
            }

            returnUrl ??= this.Url.Content("~/");

            var user = await this.userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with email '{email}'.");
            }

            this.Email = email;

            // Once you add a real email sender, you should remove this code that lets you confirm the account
            this.DisplayConfirmAccountLink = true;
            if (this.DisplayConfirmAccountLink)
            {
                var userId = await this.userManager.GetUserIdAsync(user);
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                this.EmailConfirmationUrl = this.Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId, code, returnUrl },
                    protocol: this.Request.Scheme);
            }

            return this.Page();
        }
    }
}
