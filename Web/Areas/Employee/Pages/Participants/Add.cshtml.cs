namespace Diplom.Web.Areas.Employee.Pages.Participants
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class AddModel : PageModel
    {
        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Имя")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [BindProperty]
        [Display(Name = "Фамилия")]
        public string? Surname { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
