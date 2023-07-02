// <copyright file="EmployeeService.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using Diplom.Core.Controllers;
    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The customer service.
    /// </summary>
    public class EmployeeService : BaseService
    {
        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        public EmployeeService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Create user with customer role.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="userName">UserName of user.</param>
        /// <param name="phoneNumber">User phone number.</param>
        /// <param name="password">User password.</param>
        /// <returns>User with role customer.</returns>
        public async Task<ApplicationUser> CreateEmployeeAsync(string email, string userName, string? phoneNumber, string password)
        {
            var user = new ApplicationUser
            {
                DateTimeCreated = DateTime.UtcNow,
                Email = email,
                UserName = userName,
                PhoneNumber = phoneNumber,
            };

            var identityOperationResult = await this.userManager.CreateAsync(user, password);
            this.EnsureIdentityOperationResult(identityOperationResult);

            identityOperationResult = await this.userManager.AddToRoleAsync(user, ApplicationRole.EmployeeUser);
            this.EnsureIdentityOperationResult(identityOperationResult);

            return user;
        }
    }
}
