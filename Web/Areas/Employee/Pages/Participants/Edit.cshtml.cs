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

        /// <summary>
        /// The post.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            this.InitFieldsWithout();

            if (this.Type == 1)
            {
                var np = (NaturalPerson)this.Participant!;
                np.Inn = this.Inn;
                np.Address = this.Address;
                np.PaymentAccount = this.PaymentAccount;
                np.Surname = this.Surname;
                np.Name = this.Name;
                np.Patronymic = this.Patronymic;
                np.PassportData = this.PassportData;
                np.Post = this.Post;
                np.RegisterOfSpecialists = this.RegisterOfSpecialists;
                np.DetailsAdminDoc = this.DetailsAdminDoc;
            }
            else if (this.Type == 2)
            {
                var le = (LegalEntity)this.Participant!;
                le.Inn = this.Inn;
                le.Address = this.Address;
                le.PaymentAccount = this.PaymentAccount;
                le.LegalName = this.LegalName;
                le.PostAddress = this.PostAddress;
                le.Mail = this.Mail;
                le.Phone = this.Phone;
                le.Ogrn = this.Ogrn;
                le.Bic = this.Bic;
                le.CorAccount = this.CorAccount;
                le.Okved = this.Okved;
                le.SroName = this.SroName;
                le.SroInn = this.SroInn;
                le.SroOgrn = this.SroOgrn;
                le.Kpp = this.Kpp;
                le.Okpo = this.Okpo;
                le.Okato = this.Okato;
                le.GeneralManager = this.GeneralManager;
                le.Site = this.Site;
            }
            else
            {
                var ie = (IndividualEntrepreneur)this.Participant!;
                ie.Inn = this.Inn;
                ie.Address = this.Address;
                ie.PaymentAccount = this.PaymentAccount;
                ie.LegalName = this.LegalName;
                ie.PostAddress = this.PostAddress;
                ie.Mail = this.Mail;
                ie.Phone = this.Phone;
                ie.Ogrn = this.Ogrn;
                ie.Bic = this.Bic;
                ie.CorAccount = this.CorAccount;
                ie.Okved = this.Okved;
                ie.SroName = this.SroName;
                ie.SroInn = this.SroInn;
                ie.SroOgrn = this.SroOgrn;
                ie.ShortName = this.ShortName;
                ie.BankName = this.BankName;
                ie.TaxationSystem = this.TaxationSystem;
            }

            this.DataContext.SaveChanges();

            return this.RedirectToPage("/Participants/View", new { this.ParticipantId, this.ProjectId });
        }

        private void InitFieldsWithout()
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
                this.Inn = ie.Inn;
                this.Address = ie.Address;
                this.PaymentAccount = ie.PaymentAccount;
                this.LegalName = ie.LegalName;
                this.PostAddress = ie.PostAddress;
                this.Mail = ie.Mail;
                this.Phone = ie.Phone;
                this.Ogrn = ie.Ogrn;
                this.Bic = ie.Bic;
                this.CorAccount = ie.CorAccount;
                this.Okved = ie.Okved;
                this.SroName = ie.SroName;
                this.SroInn = ie.SroInn;
                this.SroOgrn = ie.SroOgrn;
                this.ShortName = ie.ShortName;
                this.BankName = ie.BankName;
                this.TaxationSystem = ie.TaxationSystem;
            }
            else if (le != null)
            {
                this.Participant = le;
                this.Type = 2;
                this.Inn = le.Inn;
                this.Address = le.Address;
                this.PaymentAccount = le.PaymentAccount;
                this.LegalName = le.LegalName;
                this.PostAddress = le.PostAddress;
                this.Mail = le.Mail;
                this.Phone = le.Phone;
                this.Ogrn = le.Ogrn;
                this.Bic = le.Bic;
                this.CorAccount = le.CorAccount;
                this.Okved = le.Okved;
                this.SroName = le.SroName;
                this.SroInn = le.SroInn;
                this.SroOgrn = le.SroOgrn;
                this.Kpp = le.Kpp;
                this.Okpo = le.Okpo;
                this.Okato = le.Okato;
                this.GeneralManager = le.GeneralManager;
                this.Site = le.Site;
            }
            else
            {
                this.Participant = np;
                this.Type = 1;
                this.Inn = np.Inn;
                this.Address = np.Address;
                this.PaymentAccount = np.PaymentAccount;
                this.Surname = np.Surname;
                this.Name = np.Name;
                this.Patronymic = np.Patronymic;
                this.PassportData = np.PassportData;
                this.Post = np.Post;
                this.RegisterOfSpecialists = np.RegisterOfSpecialists;
                this.DetailsAdminDoc = np.DetailsAdminDoc;
            }
        }
    }
}
