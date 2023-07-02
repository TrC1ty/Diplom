// <copyright file="LoginWith2fa.cshtml.cs" company="Test Company">
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
    /// Page model class for LoginWith2fa page.
    /// </summary>
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<LoginWith2faModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWith2faModel"/> class.
        /// </summary>
        /// <param name="signInManager">Identity sign in manager.</param>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="logger">Logging service.</param>
        public LoginWith2faModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginWith2faModel> logger)
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
        /// Gets or sets a value indicating whether 'remember me' options is set.
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets return URL.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <param name="rememberMe">'Remember me' check box value.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            this.ReturnUrl = returnUrl;
            this.RememberMe = rememberMe;

            return this.Page();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request.
        /// </summary>
        /// <param name="rememberMe">'Remember me' check box value.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            returnUrl ??= this.Url.Content("~/");

            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = this.Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await this.signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, this.Input.RememberMachine);

            var userId = await this.userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                this.logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", userId);
                return this.LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                this.logger.LogWarning("User with ID '{UserId}' account locked out.", userId);
                return this.RedirectToPage("./Lockout");
            }
            else
            {
                this.logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", userId);
                this.ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return this.Page();
            }
        }

        /// <summary>
        /// Page input model class.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets 2 factor authentication code.
            /// </summary>
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether 'Remember this machine' option is set.
            /// </summary>
            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }
    }
}
