// <copyright file="View.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Participants
{
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// View model.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class ViewModel : BasePageModel
    {
        /// <summary>
        /// Gets or sets participant id.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int ParticipantId { get; set; }

        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets participant.
        /// </summary>
        public Participant? Participant { get; set; }

        /// <summary>
        /// Gets or sets participant type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
            this.InitFields();
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when Delete page handler is specified.
        /// </summary>
        /// <returns>Page action result.</returns>
        public IActionResult OnPostDeleteParticipant()
        {
            this.InitFields();

            if (this.Type == 1)
            {
                this.DataContext.NaturalPersons.Remove((NaturalPerson)this.Participant!);
            }
            else if (this.Type == 2)
            {
                this.DataContext.LegalEntities.Remove((LegalEntity)this.Participant!);
            }
            else
            {
                this.DataContext.IndividualEntrepreneurs.Remove((IndividualEntrepreneur)this.Participant!);
            }

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Projects/Participants", new { this.ProjectId });
        }

        private void InitFields()
        {
            var le = this.DataContext.LegalEntities
                        .Include(le => le.Project)
                        .SingleOrDefault(le => le.Id == this.ParticipantId);

            var np = this.DataContext.NaturalPersons
                        .Include(le => le.Project)
                        .SingleOrDefault(le => le.Id == this.ParticipantId);

            var ie = this.DataContext.IndividualEntrepreneurs
                        .Include(le => le.Project)
                        .SingleOrDefault(le => le.Id == this.ParticipantId);

            if (ie != null)
            {
                this.Participant = ie;
                this.Type = 3;
            }
            else if (le != null)
            {
                this.Participant = le;
                this.Type = 2;
            }
            else
            {
                this.Participant = np;
                this.Type = 1;
            }
        }
    }
}
