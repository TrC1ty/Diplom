// <copyright file="ShowRecoveryCodes.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account.Manage
{
    using Diplom.Core.Data.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for ShowRecoveryCodes page.
    /// </summary>
    public class ShowRecoveryCodesModel : PageModel
    {
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
        /// Automatically executed as a result of a GET request.
        /// </summary>
        /// <returns>Page action result.</returns>
        public IActionResult OnGet()
        {
            if (this.RecoveryCodes == null || this.RecoveryCodes.Length == 0)
            {
                return this.RedirectToPage("./TwoFactorAuthentication");
            }

            return this.Page();
        }
    }
}
