// <copyright file="Reestr.cshtml.cs" company="Test Company">
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
    public class Reestr : BasePageModel
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
        /// Gets or sets sections.
        /// </summary>
        public List<ProjectSection>? Sections { get; set; }

        /// <summary>
        /// Gets or sets section name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string? Name { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
            this.InitFields();
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

            var section = new ProjectSection(this.Name!);
            section.Project = this.Project!;
            this.DataContext.ProjectSections.Add(section);
            this.DataContext.SaveChanges();

            this.InitFields();

            return this.Page();
        }

        private void InitFields()
        {
            this.Project = this.DataContext.Projects.Single(p => p.Id == this.ProjectId);
            this.Sections = this.DataContext.ProjectSections.Where(s => s.ProjectId == this.ProjectId).ToList();
        }
    }
}