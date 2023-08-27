// <copyright file="ApplicationUser.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// EF entity that represents application user. Part of ASP.NET Core Identity Framework.
    /// </summary>
    public class ApplicationUser : IdentityUser<int>
    {
        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets date and time when this record was created.
        /// </summary>
        public DateTime DateTimeCreated { get; set; }

        /// <summary>
        /// Gets or sets date and time when this record was deleted.
        /// This value is null if record has not been deleted.
        /// </summary>
        public DateTime? DateTimeDeleted { get; set; }

        /// <summary>
        /// Gets or sets user rights.
        /// </summary>
        public UserRights? UserRghts { get; set; }

        /// <summary>
        /// Gets collection of ApplicationUserRole objects.
        /// </summary>
        public List<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();

        /// <summary>
        /// Gets list of project.
        /// </summary>
        public List<Project> Projects { get; } = new List<Project>();

        /// <summary>
        /// Checks if user belongs to specified role.
        /// </summary>
        /// <param name="roleName">Role name to check.</param>
        /// <returns>True if user belongs to role.</returns>
        public bool IsInRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException($"'{nameof(roleName)}' cannot be null or whitespace.", nameof(roleName));
            }

            return this.UserRoles.Any(ur => ur.Role.Name == roleName);
        }
    }
}
