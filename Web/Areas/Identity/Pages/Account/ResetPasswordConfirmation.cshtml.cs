// <copyright file="ResetPasswordConfirmation.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for ResetPasswordConfirmation page.
    /// </summary>
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
    }
}
