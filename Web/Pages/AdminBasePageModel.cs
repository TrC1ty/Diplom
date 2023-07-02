// <copyright file="AdminBasePageModel.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Pages
{
    using System.Linq;
    using System.Security;
    using Diplom.Core.Controllers;
    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Base page model with main used services.
    /// </summary>
    public class AdminBasePageModel : PageModel
    {
        private MainController? controller;
        private ApplicationDbContext? dataContext;
        private ApplicationUser? currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBasePageModel"/> class.
        /// </summary>
        protected AdminBasePageModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBasePageModel"/> class.
        /// </summary>
        /// <param name="controller">Controller to be registerd and reused in <see cref="Controller"/> property.</param>
        /// <param name="dataContext">Data context to be registered and reused in <see cref="DataContext"/> property.</param>
        protected AdminBasePageModel(MainController controller, ApplicationDbContext dataContext)
        {
            this.controller = controller;
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Gets main controller.
        /// </summary>
        public MainController Controller => this.controller ??= (MainController)this.HttpContext.RequestServices.GetService(typeof(MainController))!;

        /// <summary>
        /// Gets application data context.
        /// </summary>
        public ApplicationDbContext DataContext => this.dataContext ??= (ApplicationDbContext)this.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext))!;

        /// <summary>
        /// Gets a value indicating whether user is authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                if (this.currentUser != null)
                {
                    return true;
                }

                return this.HttpContext.User.Identity == null || this.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// Gets a value indicating whether user has admin role.
        /// </summary>
        public bool IsAdministrator
        {
            get
            {
                return this.CurrentUser.IsInRole(ApplicationRole.SystemAdmin);
            }
        }

        /// <summary>
        /// Gets current user object.
        /// </summary>
        public ApplicationUser CurrentUser
        {
            get
            {
                if (this.currentUser == null)
                {
                    var normalizedCurrentUserName = this.HttpContext.User.Identity!.Name!.ToUpperInvariant();

                    this.currentUser = this.DataContext.Users
                        .Include(u => u.UserRoles!)
                            .ThenInclude(ur => ur.Role)
                        .Single(u => u.NormalizedUserName == normalizedCurrentUserName);

                    if (this.currentUser.DateTimeDeleted.HasValue)
                    {
                        throw new SecurityException("Attempt to login as deleted user.");
                    }
                }

                return this.currentUser;
            }
        }
    }
}
