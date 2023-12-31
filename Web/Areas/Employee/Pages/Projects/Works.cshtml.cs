﻿// <copyright file="View.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Projects
{
    using System.ComponentModel.DataAnnotations;
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Page model class for the Index page.
    /// </summary>
    public class Works : BasePageModel
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
        /// Gets or sets list of work.
        /// </summary>
        public List<Work> ProjectWorks { get; set; } = default!;

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
            this.InitFields();
        }

        private void InitFields()
        {
            this.Project = this.DataContext.Projects.Single(p => p.Id == this.ProjectId);
            this.ProjectWorks = this.DataContext.Works
                .Include(w => w.Section)
                    .ThenInclude(s => s.Project)
                .Where(w => w.Section.ProjectId == this.ProjectId).ToList();
        }
    }
}