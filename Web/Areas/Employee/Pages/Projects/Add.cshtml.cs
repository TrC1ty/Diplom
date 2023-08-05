// <copyright file="add.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Projects
{
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Page model class for the Index page.
    /// </summary>
    public class Add : BasePageModel
    {
        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Наименование")]
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
        /// The get.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// The post.
        /// </summary>
        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var project = new Project(this.Name!, this.ProjectCode!);
            project.ProjectDocumentation = this.ProjectDocumentation;
            project.BuldingAddress = this.BuildingAddress;
            project.User = this.CurrentUser;

            this.DataContext.Projects.Add(project);

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Index");
        }
    }
}