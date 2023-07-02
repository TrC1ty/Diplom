// <copyright file="InitDatabase.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Diplom.Core.Controllers;
    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;
    using Diplom.Core.Data.Initialization;

    using Microsoft.AspNetCore.Identity;

#pragma warning disable SA1649 // File name should match first type name. This is a convention for Razor pages models.

    /// <summary>
    /// Page model class for InitDatabase page.
    /// </summary>
    public class InitDatabaseModel : BasePageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitDatabaseModel"/> class.
        /// </summary>
        /// <param name="controller">Main controller.</param>
        /// <param name="dataContext">Application data context.</param>
        /// <param name="userManager">Identity Core user manager.</param>
        /// <param name="roleManager">Identity Core role manager.</param>
        public InitDatabaseModel(MainController controller, ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
            : base(controller, dataContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Gets or sets a value indicating whether database has been created.
        /// </summary>
        public bool RecreateDatabaseSuccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether database data load operation just succeeded.
        /// </summary>
        public bool LoadDataSuccess { get; set; }

        /// <summary>
        /// Automatically executed as a result of a POST request when RecreateDatabase page handler is specified.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnPostRecreateDatabase()
        {
            // (Re-)create database.
            await this.DataContext.Database.EnsureDeletedAsync();
            await this.DataContext.Database.EnsureCreatedAsync();

            // Seed base data.
            await AlwaysRequiredFirstInitializer.SeedWithDataAsync(this.Controller, this.DataContext, this.userManager, this.roleManager);

            this.RecreateDatabaseSuccess = true;
        }

        /// <summary>
        /// Automatically executed as a result of a POST request when LoadTestData page handler is specified.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnPostLoadTestData()
        {
            // Seed test data.
            await TestInitializer.SeedWithDataAsync(this.Controller, this.DataContext);

            this.LoadDataSuccess = true;
        }
    }
}