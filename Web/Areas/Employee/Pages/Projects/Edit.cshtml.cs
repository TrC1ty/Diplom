// <copyright file="add.cshtml.cs" company="Test Company">
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
    public class Edit : BasePageModel
    {
        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets project documentation.
        /// </summary>
        [BindProperty]
        [Display(Name = "Проектная документация")]
        public string? ProjectDocumentation { get; set; }

        /// <summary>
        /// Gets or sets building address.
        /// </summary>
        [BindProperty]
        [Display(Name = "Адрес строительства")]
        public string? BuildingAddress { get; set; }

        /// <summary>
        /// Gets or sets project code.
        /// </summary>
        [BindProperty]
        [Display(Name = "Код проекта")]
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string? ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty(SupportsGet = true)]
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
            this.Name = this.Project.Name;
            this.ProjectDocumentation = this.Project.ProjectDocumentation;
            this.BuildingAddress = this.Project.BuldingAddress;
            this.ProjectCode = this.Project.ProjectCode;
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <returns>Redirect to page.</returns>
        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var project = this.DataContext.Projects.Single(project => project.Id == this.ProjectId);

            project.Name = this.Name!;
            project.BuldingAddress = this.BuildingAddress;
            project.ProjectDocumentation = this.ProjectDocumentation;
            project.ProjectCode = this.ProjectCode!;

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Projects/View", new { this.ProjectId });
        }
    }
}