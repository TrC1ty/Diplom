// <copyright file="Disable2fa.cshtml.cs" company="Test Company">
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
    /// Page model class for Disable2fa page.
    /// </summary>
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<Disable2faModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Disable2faModel"/> class.
        /// </summary>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="logger">Logging service.</param>
        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Gets or sets status message.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!await this.userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"Cannot disable 2FA for user as it's not currently enabled.");
            }

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

            var disable2faResult = await this.userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA.");
            }

            this.logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", this.userManager.GetUserId(this.User));
            this.StatusMessage = "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";
            return this.RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
