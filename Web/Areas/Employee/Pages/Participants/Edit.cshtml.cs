// <copyright file="Edit.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Employee.Pages.Participants
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Edit model.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class EditModel : PageModel
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
        /// The get.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
