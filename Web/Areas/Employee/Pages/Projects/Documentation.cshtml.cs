// <copyright file="Documentation.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Projects
{
    using System.ComponentModel.DataAnnotations;
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Page model class for the Index page.
    /// </summary>
    public class Documentation : BasePageModel
    {
        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty(SupportsGet=true)]
        public int? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets project.
        /// </summary>
        public Project? Project { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
            this.Project = this.DataContext.Projects.Single(p => p.Id == this.ProjectId);
        }
    }
}