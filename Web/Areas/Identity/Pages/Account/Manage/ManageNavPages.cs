// <copyright file="ManageNavPages.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

#nullable disable

namespace Diplom.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class ManageNavPages
    {
        /// <summary>
        /// Gets Index page name.
        /// </summary>
        public static string Index => "Index";

        /// <summary>
        /// Gets Email page name.
        /// </summary>
        public static string Email => "Email";

        /// <summary>
        /// Gets ChangePassword page name.
        /// </summary>
        public static string ChangePassword => "ChangePassword";

        /// <summary>
        /// Gets DownloadPersonalData page name.
        /// </summary>
        public static string DownloadPersonalData => "DownloadPersonalData";

        /// <summary>
        /// Gets DeletePersonalData page name.
        /// </summary>
        public static string DeletePersonalData => "DeletePersonalData";

        /// <summary>
        /// Gets ExternalLogins page name.
        /// </summary>
        public static string ExternalLogins => "ExternalLogins";

        /// <summary>
        /// Gets PersonalData page name.
        /// </summary>
        public static string PersonalData => "PersonalData";

        /// <summary>
        /// Gets TwoFactorAuthentication page name.
        /// </summary>
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        /// <summary>
        /// Returns Index page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        /// <summary>
        /// Returns Email page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        /// <summary>
        /// Returns ChangePassword page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        /// <summary>
        /// Returns DownloadPersonalData page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

        /// <summary>
        /// Returns DeletePersonalData page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);

        /// <summary>
        /// Returns ExternalLogins page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        /// <summary>
        /// Returns PersonalData page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        /// <summary>
        /// Returns TwoFactorAuthentication page navigation link class.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <returns>Class name.</returns>
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        /// <summary>
        /// Returns page navigation link class for specified view context and page.
        /// </summary>
        /// <param name="viewContext">Context for view execution.</param>
        /// <param name="page">Page name.</param>
        /// <returns>Class name.</returns>
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
