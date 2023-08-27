// <copyright file="UserService.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The user service.
    /// </summary>
    public class UserService : BaseService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="dbContext">
        /// Db context.
        /// </param>
        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Create user with customer role.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="userName">UserName of user.</param>
        /// <param name="phoneNumber">User phone number.</param>
        /// <param name="password">User password.</param>
        /// <param name="name">User name.</param>
        /// <returns>User with role customer.</returns>
        public async Task<ApplicationUser> CreateCompanyAsync(string email, string userName, string? phoneNumber, string password, string name)
        {
            var user = new ApplicationUser
            {
                Name = name,
                DateTimeCreated = DateTime.UtcNow,
                Email = email,
                UserName = userName,
                PhoneNumber = phoneNumber,
            };

            var identityOperationResult = await this.userManager.CreateAsync(user, password);
            this.EnsureIdentityOperationResult(identityOperationResult);

            identityOperationResult = await this.userManager.AddToRoleAsync(user, ApplicationRole.CompanyUser);
            this.EnsureIdentityOperationResult(identityOperationResult);

            var userRights = new UserRights
            {
                CanArchiveProject = true,
                CanCreateProject = true,
                CanDeleteProject = true,
                User = user,
            };

            this.dbContext.UsersRights.Add(userRights);
            this.dbContext.SaveChanges();

            return user;
        }

        /// <summary>
        /// Create user with customer role.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="userName">UserName of user.</param>
        /// <param name="phoneNumber">User phone number.</param>
        /// <param name="password">User password.</param>
        /// <param name="name">User name.</param>
        /// <returns>User with role customer.</returns>
        public async Task<ApplicationUser> CreateEmployeeAsync(string email, string userName, string? phoneNumber, string password, string name)
        {
            var user = new ApplicationUser
            {
                Name = name,
                DateTimeCreated = DateTime.UtcNow,
                Email = email,
                UserName = userName,
                PhoneNumber = phoneNumber,
            };

            var identityOperationResult = await this.userManager.CreateAsync(user, password);
            this.EnsureIdentityOperationResult(identityOperationResult);

            identityOperationResult = await this.userManager.AddToRoleAsync(user, ApplicationRole.EmployeeUser);
            this.EnsureIdentityOperationResult(identityOperationResult);

            var userRights = new UserRights
            {
                CanArchiveProject = false,
                CanCreateProject = false,
                CanDeleteProject = false,
                User = user,
            };

            this.dbContext.UsersRights.Add(userRights);
            this.dbContext.SaveChanges();

            return user;
        }

        /// <summary>
        /// Change user rights.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="create">Right for creating project.</param>
        /// <param name="delete">Right for deleting project.</param>
        /// <param name="archive">Right for archiving project.</param>
        /// <exception cref="Exception">Exception while user is null.</exception>
        public void ChangeUserRights(string userName, bool create, bool delete, bool archive)
        {
            var user = this.dbContext.Users
                .Include(u => u.UserRghts)
                .Single(u => u.UserName == userName) ?? throw new Exception("User cannot be null.");
            user.UserRghts!.CanCreateProject = create;
            user.UserRghts.CanDeleteProject = delete;
            user.UserRghts.CanArchiveProject = archive;

            this.dbContext.SaveChanges();
        }
    }
}
