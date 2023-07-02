// <copyright file="AuthPageModel.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Pages
{
    using System;

    using Diplom.Core.Controllers;
    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Base page model class for page where user should be authenticated.
    /// </summary>
    public abstract class AuthPageModel : BasePageModel
    {
        private UserManager<ApplicationUser>? userManager;
        private ApplicationUser? currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthPageModel"/> class.
        /// </summary>
        protected AuthPageModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthPageModel"/> class.
        /// </summary>
        /// <param name="controller">Main controller.</param>
        /// <param name="userManager">User manager.</param>
        /// <param name="dataContext">Data context.</param>
        protected AuthPageModel(MainController controller, UserManager<ApplicationUser> userManager, ApplicationDbContext dataContext)
            : base(controller, dataContext)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets user manager.
        /// </summary>
        public UserManager<ApplicationUser> UserManager => this.userManager ??= this.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>()!;

        /// <summary>
        /// Gets current user.
        /// </summary>
        public ApplicationUser CurrentUser => this.currentUser ??= this.UserManager.FindByNameAsync(this.HttpContext.User.Identity!.Name!).Result!;
    }
}
