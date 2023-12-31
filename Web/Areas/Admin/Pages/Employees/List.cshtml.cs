// <copyright file="List.cshtml.cs" company="Test Company">
// Copyright � 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Admin.Pages.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The List model.
    /// </summary>
    public class List : AdminBasePageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="List"/> class.
        /// </summary>
        /// <param name="signInManager">Sign in manager.</param>
        public List(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
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
                .Where(u => u.UserRoles.Any(ur => ur.Role.Name == ApplicationRole.CompanyUser))
                .ToList();
        }

        /// <summary>
        /// On post, login customer.
        /// </summary>
        /// <param name="customerId">Customer id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostLogin(int customerId)
        {
            var customer = this.DataContext.Users.Single(u => u.Id == customerId);
            await this.signInManager.SignInAsync(customer, true);

            return this.LocalRedirect("/Employee/Index");
        }
    }
}
