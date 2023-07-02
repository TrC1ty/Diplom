// <copyright file="List.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Admin.Pages.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The List model.
    /// </summary>
    public class List : AdminBasePageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        public List()
        {
        }

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
                .Where(u => u.UserRoles.Any(ur => ur.Role.Name == ApplicationRole.EmployeeUser))
                .ToList();
        }
    }
}
