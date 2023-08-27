// <copyright file="Index.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Admin.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.EntityFrameworkCore;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.
    /// <summary>
    /// Page model class for Index page.
    /// </summary>
    public class IndexModel : BasePageModel
    {
        /// <summary>
        /// Gets or sets cutomers.
        /// </summary>
        public IList<ApplicationUser> Employees { get; set; } = default!;

        /// <summary>
        /// Get method.
        /// </summary>
        public void OnGet()
        {
            this.Employees = this.DataContext.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Any(ur => ur.Role.Name == ApplicationRole.CompanyUser))
                .ToList();
        }
    }
}
