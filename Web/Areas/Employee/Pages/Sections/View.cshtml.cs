// <copyright file="View.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Sections
{
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Org.BouncyCastle.Tls.Crypto;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class ViewModel : BasePageModel
    {
        /// <summary>
        /// Gets or sets section id.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? SectionId { get; set; }

        /// <summary>
        /// Gets or sets project id.
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets project.
        /// </summary>
        public ProjectSection? ProjectSection { get; set; }

        /// <summary>
        /// Gets or sets section name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Наименование")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets section name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Наименование")]
        public string? WorkName { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
            this.InitFields();
            this.Name = this.ProjectSection!.Name;
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <returns>Page.</returns>
        public IActionResult OnPost()
        {
            this.InitFields();

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.ProjectSection!.Name = this.Name!;
            this.DataContext.SaveChanges();
            this.Name = this.ProjectSection!.Name;

            return this.Page();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when Delete page handler is specified.
        /// </summary>
        /// <returns>Page action result.</returns>
        public IActionResult OnPostDeleteSection()
        {
            this.InitFields();

            this.DataContext.ProjectSections.Remove(this.ProjectSection!);

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Projects/Reestr", new { this.ProjectId });
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when Delete page handler is specified.
        /// </summary>
        /// <returns>Page action result.</returns>
        public IActionResult OnPostAddWork()
        {
            this.InitFields();

            var work = new Work() { Name = this.WorkName, Section = this.ProjectSection!, };
            this.DataContext.Works.Add(work);

            this.DataContext.SaveChanges();
            this.InitFields();
            this.Name = this.ProjectSection!.Name;

            return this.Page();
        }

        private void InitFields()
        {
            this.ProjectSection = this.DataContext.ProjectSections
                .Include(s => s.Works)
                .Single(s => s.Id == this.SectionId);

            this.ProjectId = this.ProjectSection.ProjectId;
        }
    }
}
