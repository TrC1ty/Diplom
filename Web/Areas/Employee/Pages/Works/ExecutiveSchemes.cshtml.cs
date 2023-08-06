// <copyright file="ExecutiveSchemes.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Works
{
    using Diplom.Core.Data.Entities;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Executive schemes model.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class ExecutiveSchemesModel : BasePageModel
    {
        /// <summary>
        /// Gets or sets section id.
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// Gets or sets work id.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int? WorkId { get; set; }

        /// <summary>
        /// Gets or sets work.
        /// </summary>
        public Work? Work { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
            this.InitFields();
        }

        private void InitFields()
        {
            this.Work = this.DataContext.Works.Single(w => w.Id == this.WorkId);
            this.SectionId = this.Work.SectionId;
        }
    }
}
