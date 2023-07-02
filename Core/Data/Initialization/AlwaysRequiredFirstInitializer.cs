// <copyright file="AlwaysRequiredFirstInitializer.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Initialization
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Diplom.Core.Controllers;
    using Diplom.Core.Data.Entities;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Database initializer. Loads initial data that should be in any database wether it is a test or production.
    /// </summary>
    public static class AlwaysRequiredFirstInitializer
    {
        /// <summary>
        /// Seeds with initial data. Note, that admin's password should be changed immidiately.
        /// </summary>
        /// <param name="controller">Main controller.</param>
        /// <param name="dataContext">Application data context.</param>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="roleManager">Identity framework role manager.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task SeedWithDataAsync(MainController controller, ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (roleManager is null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            if (roleManager.Roles.Any() || userManager.Users.Any())
            {
                throw new InvalidOperationException("Seeding should be done into empty database but some roles or users already exist.");
            }

            IdentityResult identityResult;

            // Add roles.
            identityResult = await roleManager.CreateAsync(new ApplicationRole() { Id = 0, Name = ApplicationRole.System });
            MainController.EnsureIdentityResultIsSucceeded(identityResult);

            identityResult = await roleManager.CreateAsync(new ApplicationRole() { Id = 10, Name = ApplicationRole.SystemAdmin });
            MainController.EnsureIdentityResultIsSucceeded(identityResult);

            identityResult = await roleManager.CreateAsync(new ApplicationRole() { Id = 20, Name = ApplicationRole.EmployeeUser });
            MainController.EnsureIdentityResultIsSucceeded(identityResult);

            // Add SYSTEM and Admin user. Add lower-case, upper-case, digit and punctuation to pass password validation.
            await controller.CreateSystemUserAsync();
            var adminUser = await controller.AddNewUserAsync("admin@test.com", "admin@test.com", "Pass@word1T", ApplicationRole.SystemAdmin);
            adminUser.EmailConfirmed = true;

            dataContext.SaveChanges();
        }
    }
}