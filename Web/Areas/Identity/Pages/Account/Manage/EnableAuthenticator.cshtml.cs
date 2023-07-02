// <copyright file="EnableAuthenticator.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Diplom.Core.Data.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for EnableAuthenticator page.
    /// </summary>
    public class EnableAuthenticatorModel : PageModel
    {
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<EnableAuthenticatorModel> logger;
        private readonly UrlEncoder urlEncoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnableAuthenticatorModel"/> class.
        /// </summary>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="logger">Logging service.</param>
        /// <param name="urlEncoder">URL encoding service.</param>
        public EnableAuthenticatorModel(
            UserManager<ApplicationUser> userManager,
            ILogger<EnableAuthenticatorModel> logger,
            UrlEncoder urlEncoder)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.urlEncoder = urlEncoder;
        }

        /// <summary>
        /// Gets or sets shared key.
        /// </summary>
        public string SharedKey { get; set; }

        /// <summary>
        /// Gets or sets authenticator URI.
        /// </summary>
        public string AuthenticatorUri { get; set; }

        /// <summary>
        /// Gets or sets recovery codes.
        /// </summary>
        [TempData]
        public string[] RecoveryCodes { get; set; }

        /// <summary>
        /// Gets or sets status message.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets page input model.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadSharedKeyAndQrCodeUriAsync(user);

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

            if (!this.ModelState.IsValid)
            {
                await this.LoadSharedKeyAndQrCodeUriAsync(user);
                return this.Page();
            }

            // Strip spaces and hyphens
            var verificationCode = this.Input.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await this.userManager.VerifyTwoFactorTokenAsync(
                user, this.userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                this.ModelState.AddModelError("Input.Code", "Verification code is invalid.");
                await this.LoadSharedKeyAndQrCodeUriAsync(user);
                return this.Page();
            }

            await this.userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await this.userManager.GetUserIdAsync(user);
            this.logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);

            this.StatusMessage = "Your authenticator app has been verified.";

            if (await this.userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var recoveryCodes = await this.userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                this.RecoveryCodes = recoveryCodes.ToArray();
                return this.RedirectToPage("./ShowRecoveryCodes");
            }
            else
            {
                return this.RedirectToPage("./TwoFactorAuthentication");
            }
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user)
        {
            // Load the authenticator key & QR code URI to display on the form
            var unformattedKey = await this.userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await this.userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await this.userManager.GetAuthenticatorKeyAsync(user);
            }

            this.SharedKey = this.FormatKey(unformattedKey);

            var email = await this.userManager.GetEmailAsync(user);
            this.AuthenticatorUri = this.GenerateQrCodeUri(email, unformattedKey);
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                AuthenticatorUriFormat,
                this.urlEncoder.Encode("Microsoft.AspNetCore.Identity.UI"),
                this.urlEncoder.Encode(email),
                unformattedKey);
        }

        /// <summary>
        /// Page input model class.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets code.
            /// </summary>
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Verification Code")]
            public string Code { get; set; }
        }
    }
}
