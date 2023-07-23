// <copyright file="Add.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Participants
{
    using System.ComponentModel.DataAnnotations;
    using Diplom.Core.Data.Entities;
    using Diplom.Core.Data.Enums;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Add page.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class AddModel : BasePageModel
    {
        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets participant type.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? Type { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets surname.
        /// </summary>
        [BindProperty]
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string? Surname { get; set; }

        /// <summary>
        /// Gets or sets patronomic.
        /// </summary>
        [BindProperty]
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }

        /// <summary>
        /// Gets or sets legal name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Поле обязательно для заполнения.")]
        public string? LegalName { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// Add natural person.
        /// </summary>
        /// <returns>Redirect to page.</returns>
        public IActionResult OnPostAddIE()
        {
            if (string.IsNullOrEmpty(this.LegalName))
            {
                return this.Page();
            }

            var ie = new IndividualEntrepreneur()
            {
                LegalName = this.LegalName,
            };

            ie = (IndividualEntrepreneur)this.ChooseType(ie);
            var project = this.DataContext.Projects
                .Include(p => p.Participants)
                .Single(p => p.Id == this.ProjectId);
            project.Participants.Add(ie);

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Projects/Participants", new { this.ProjectId });
        }

        /// <summary>
        /// Add individual enterpreneur.
        /// </summary>
        /// <returns>Redirect to page.</returns>
        public IActionResult OnPostAddNP()
        {
            if (string.IsNullOrEmpty(this.Surname) || string.IsNullOrEmpty(this.Name))
            {
                return this.Page();
            }

            var np = new NaturalPerson()
            {
                Surname = this.Surname,
                Name = this.Name,
                Patronymic = this.Patronymic,
            };

            np = (NaturalPerson)this.ChooseType(np);
            var project = this.DataContext.Projects
                .Include(p => p.Participants)
                .Single(p => p.Id == this.ProjectId);
            project.Participants.Add(np);

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Projects/Participants", new { this.ProjectId });
        }

        /// <summary>
        /// Add legal entity.
        /// </summary>
        /// <returns>Redirect to page.</returns>
        public IActionResult OnPostAddLE()
        {
            if (string.IsNullOrEmpty(this.LegalName))
            {
                return this.Page();
            }

            var le = new LegalEntity()
            {
                LegalName = this.LegalName,
            };

            le = (LegalEntity)this.ChooseType(le);
            var project = this.DataContext.Projects
                .Include(p => p.Participants)
                .Single(p => p.Id == this.ProjectId);
            project.Participants.Add(le);

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Projects/Participants", new { this.ProjectId });
        }

        private Participant ChooseType(Participant participant)
        {
            participant.ParticipantType = this.Type switch
            {
                1 => ParticipantType.Type1,
                2 => ParticipantType.Type2,
                3 => ParticipantType.Type3,
                4 => ParticipantType.Type4,
                5 => ParticipantType.Type5,
                6 => ParticipantType.Type6,
                7 => ParticipantType.Type7,
                8 => ParticipantType.Type8,
                9 => ParticipantType.Type9,
                _ => ParticipantType.Type10,
            };
            return participant;
        }
    }
}
