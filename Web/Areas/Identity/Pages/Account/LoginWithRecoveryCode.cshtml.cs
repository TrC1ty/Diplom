// <copyright file="LoginWithRecoveryCode.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Diplom.Core.Data.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for  LoginWithRecoveryCode page.
    /// </summary>
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWithRecoveryCodeModel"/> class.
        /// </summary>
        /// <param name="signInManager">Identity sign in manager.</param>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="logger">Logging service.</param>
        public LoginWithRecoveryCodeModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginWithRecoveryCodeModel> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Gets or sets page input model.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Gets or sets return URL.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            this.ReturnUrl = returnUrl;

            return this.Page();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = this.Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await this.signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            var userId = await this.userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                this.logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", userId);
                return this.LocalRedirect(returnUrl ?? this.Url.Content("~/"));
            }

            if (result.IsLockedOut)
            {
                this.logger.LogWarning("User account locked out.");
                return this.RedirectToPage("./Lockout");
            }
            else
            {
                this.logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", userId);
                this.ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return this.Page();
            }
        }

        /// <summary>
        /// Page input model class.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets recovery code.
            /// </summary>
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
        }
    }
}
