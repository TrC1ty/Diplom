// <copyright file="BasePageModel.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Pages
{
    using System;

    using Diplom.Core.Controllers;
    using Diplom.Core.Data;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Base page model class.
    /// </summary>
    public abstract class BasePageModel : PageModel
    {
        private MainController? controller;
        private ApplicationDbContext? dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePageModel"/> class.
        /// </summary>
        protected BasePageModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePageModel"/> class.
        /// </summary>
        /// <param name="controller">Controller to be registerd and reused in <see cref="Controller"/> property.</param>
        /// <param name="dataContext">Data context to be registered and reused in <see cref="DataContext"/> property.</param>
        protected BasePageModel(MainController controller, ApplicationDbContext dataContext)
        {
            this.controller = controller;
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Gets main controller.
        /// </summary>
        public MainController Controller => this.controller ??= this.HttpContext.RequestServices.GetService<MainController>()!;

        /// <summary>
        /// Gets application data context.
        /// </summary>
        public ApplicationDbContext DataContext => this.dataContext ??= this.HttpContext.RequestServices.GetService<ApplicationDbContext>()!;
    }
}
