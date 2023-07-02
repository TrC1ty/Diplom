// <copyright file="TwoFactorAuthentication.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Threading.Tasks;
    using Diplom.Core.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for TwoFactorAuthentication page.
    /// </summary>
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoFactorAuthenticationModel"/> class.
        /// </summary>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="signInManager">Identity sign in manager.</param>
        public TwoFactorAuthenticationModel(
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Gets or sets a value indicating whether user has authenticator.
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        /// Gets or sets number of recovery codes left.
        /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 2FA is enabled.
        /// </summary>
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the machine is remembered.
        /// </summary>
        public bool IsMachineRemembered { get; set; }

        /// <summary>
        /// Gets or sets status message.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.HasAuthenticator = await this.userManager.GetAuthenticatorKeyAsync(user) != null;
            this.Is2faEnabled = await this.userManager.GetTwoFactorEnabledAsync(user);
            this.IsMachineRemembered = await this.signInManager.IsTwoFactorClientRememberedAsync(user);
            this.RecoveryCodesLeft = await this.userManager.CountRecoveryCodesAsync(user);

            return this.Page();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.signInManager.ForgetTwoFactorClientAsync();
            this.StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
            return this.RedirectToPage();
        }
    }
}
