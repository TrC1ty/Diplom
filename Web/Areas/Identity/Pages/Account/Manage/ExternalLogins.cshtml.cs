// <copyright file="ExternalLogins.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Diplom.Core.Data.Entities;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for ExternalLogins page.
    /// </summary>
    public class ExternalLoginsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserStore<ApplicationUser> userStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLoginsModel"/> class.
        /// </summary>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="signInManager">Identity sign in manager.</param>
        /// <param name="userStore">Identity framework user store.</param>
        public ExternalLoginsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserStore<ApplicationUser> userStore)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userStore = userStore;
        }

        /// <summary>
        /// Gets or sets current logins.
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// Gets or sets other logins.
        /// </summary>
        public IList<AuthenticationScheme> OtherLogins { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remove button should be displayed.
        /// </summary>
        public bool ShowRemoveButton { get; set; }

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

            this.CurrentLogins = await this.userManager.GetLoginsAsync(user);
            this.OtherLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => this.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();

            string passwordHash = null;
            if (this.userStore is IUserPasswordStore<ApplicationUser> userPasswordStore)
            {
                passwordHash = await userPasswordStore.GetPasswordHashAsync(user, this.HttpContext.RequestAborted);
            }

            this.ShowRemoveButton = passwordHash != null || this.CurrentLogins.Count > 1;
            return this.Page();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when RemoveLogin page handler is specified.
        /// </summary>
        /// <param name="loginProvider">Provider name.</param>
        /// <param name="providerKey">Provider key.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var result = await this.userManager.RemoveLoginAsync(user, loginProvider, providerKey);
            if (!result.Succeeded)
            {
                this.StatusMessage = "The external login was not removed.";
                return this.RedirectToPage();
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "The external login was removed.";
            return this.RedirectToPage();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when LinkLogin page handler is specified.
        /// </summary>
        /// <param name="provider">Provider name.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostLinkLoginAsync(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = this.Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, this.userManager.GetUserId(this.User));
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// Automatically executed as a result of a GET request when Callback page handler is specified.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var info = await this.signInManager.GetExternalLoginInfoAsync(userId);
            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info.");
            }

            var result = await this.userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                this.StatusMessage = "The external login was not added. External logins can only be associated with one account.";
                return this.RedirectToPage();
            }

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.StatusMessage = "The external login was added.";
            return this.RedirectToPage();
        }
    }
}
