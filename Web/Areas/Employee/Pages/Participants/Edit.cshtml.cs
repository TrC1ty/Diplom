// <copyright file="Edit.cshtml.cs" company="Test Company">
// Copyright � 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Participants
{
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Edit model.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class EditModel : BasePageModel
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
        /// Gets or sets prticipant inn.
        /// </summary>
        [BindProperty]
        [Display(Name = "���")]
        public string? Inn { get; set; }

        /// <summary>
        /// Gets or sets participant address.
        /// </summary>
        [BindProperty]
        [Display(Name = "����������� �����")]
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets payment account.
        /// </summary>
        [BindProperty]
        [Display(Name = "�/�")]
        public string? PaymentAccount { get; set; }

        // Natural person

        /// <summary>
        /// Gets or sets surname.
        /// </summary>
        [BindProperty]
        [Display(Name = "�������")]
        [Required(ErrorMessage = "���� ����������� ��� ����������.")]
        public string? Surname { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [BindProperty]
        [Display(Name = "���")]
        [Required(ErrorMessage = "���� ����������� ��� ����������.")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets patronymic.
        /// </summary>
        [BindProperty]
        [Display(Name = "��������")]
        public string? Patronymic { get; set; }

        /// <summary>
        /// Gets or sets passport data.
        /// </summary>
        [BindProperty]
        [Display(Name = "���������� ������")]
        public string? PassportData { get; set; }

        /// <summary>
        /// Gets or sets post.
        /// </summary>
        [BindProperty]
        [Display(Name = "���������")]
        public string? Post { get; set; }

        /// <summary>
        /// Gets or sets register of specialists.
        /// </summary>
        [BindProperty]
        [Display(Name = "������ ������������")]
        public string? RegisterOfSpecialists { get; set; }

        /// <summary>
        /// Gets or sets details of admin documentation.
        /// </summary>
        [BindProperty]
        [Display(Name = "��������� ����������������� ���������")]
        public string? DetailsAdminDoc { get; set; }

        // Natural person

        /// <summary>
        /// Gets or sets legal name.
        /// </summary>
        [BindProperty]
        [Display(Name = "������������")]
        [Required(ErrorMessage = "���� ����������� ��� ����������.")]
        public string? LegalName { get; set; }

        /// <summary>
        /// Gets or sets post address.
        /// </summary>
        [BindProperty]
        [Display(Name = "����������� �����")]
        public string? PostAddress { get; set; }

        /// <summary>
        /// Gets or sets mail.
        /// </summary>
        [BindProperty]
        [Display(Name = "�����")]
        public string? Mail { get; set; }

        /// <summary>
        /// Gets or sets phone.
        /// </summary>
        [BindProperty]
        [Display(Name = "�������")]
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets ogrn.
        /// </summary>
        [BindProperty]
        [Display(Name = "����")]
        public string? Ogrn { get; set; }

        /// <summary>
        /// Gets or sets bic.
        /// </summary>
        [BindProperty]
        [Display(Name = "���")]
        public string? Bic { get; set; }

        /// <summary>
        /// Gets or sets correspondent account.
        /// </summary>
        [BindProperty]
        [Display(Name = "�/�")]
        public string? CorAccount { get; set; }

        /// <summary>
        /// Gets or sets okved.
        /// </summary>
        [BindProperty]
        [Display(Name = "�����")]
        public string? Okved { get; set; }

        /// <summary>
        /// Gets or sets sro name.
        /// </summary>
        [BindProperty]
        [Display(Name = "������������ ���")]
        public string? SroName { get; set; }

        /// <summary>
        /// Gets or sets sro inn.
        /// </summary>
        [BindProperty]
        [Display(Name = "��� ���")]
        public string? SroInn { get; set; }

        /// <summary>
        /// Gets or sets sro inn.
        /// </summary>
        [BindProperty]
        [Display(Name = "���� ���")]
        public string? SroOgrn { get; set; }

        // Legal entity

        /// <summary>
        /// Gets or sets kpp.
        /// </summary>
        [BindProperty]
        [Display(Name = "���")]
        public string? Kpp { get; set; }

        /// <summary>
        /// Gets or sets okpo.
        /// </summary>
        [BindProperty]
        [Display(Name = "����")]
        public string? Okpo { get; set; }

        /// <summary>
        /// Gets or sets okato.
        /// </summary>
        [BindProperty]
        [Display(Name = "�����")]
        public string? Okato { get; set; }

        /// <summary>
        /// Gets or sets general manager.
        /// </summary>
        [BindProperty]
        [Display(Name = "����������� ��������")]
        public string? GeneralManager { get; set; }

        /// <summary>
        /// Gets or sets site.
        /// </summary>
        [BindProperty]
        [Display(Name = "����")]
        public string? Site { get; set; }

        // Legal entity

        // Individual entepreneur

        /// <summary>
        /// Gets or sets short name.
        /// </summary>
        [BindProperty]
        [Display(Name = "������� ������������")]
        public string? ShortName { get; set; }

        /// <summary>
        /// Gets or sets bank name.
        /// </summary>
        [BindProperty]
        [Display(Name = "������������ �����")]
        public string? BankName { get; set; }

        /// <summary>
        /// Gets or sets taxation system.
        /// </summary>
        [BindProperty]
        [Display(Name = "������� ���������������")]
        public string? TaxationSystem { get; set; }

        // Individual entepreneur

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
