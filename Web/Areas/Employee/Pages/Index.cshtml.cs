// <copyright file="Index.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages
{
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Page model class for the Index page.
    /// </summary>
    public class Index : BasePageModel
    {
        /// <summary>
        /// Gets or sets list of project.
        /// </summary>
        public List<Project> UserProjects { get; set; } = default!;

        public void OnGet()
        {
            this.UserProjects = this.DataContext.Projects.Where(p => p.UserId == this.CurrentUser.Id).ToList();
        }
    }
}