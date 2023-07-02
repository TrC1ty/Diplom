// <copyright file="ApplicationRole.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Identity Core Role.
    /// </summary>
    public class ApplicationRole : IdentityRole<int>
    {
        /// <summary>
        /// System role name. This is role for SYSTEM user. Has all permissions.
        /// </summary>
        public static readonly string System = nameof(System).ToUpperInvariant();

        /// <summary>
        /// SystemAdmin role name. Has all permissions.
        /// </summary>
        public static readonly string SystemAdmin = nameof(SystemAdmin);

        /// <summary>
        /// Users role name. This is role for normal employee user.
        /// </summary>
        public static readonly string EmployeeUser = nameof(EmployeeUser);

        /// <summary>
        /// Gets collection of ApplicationUserRole objects.
        /// </summary>
        public List<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();

        /// <summary>
        /// Gets display name for system role name.
        /// </summary>
        /// <param name="role">System (english) role name.</param>
        /// <returns>Display name in russian.</returns>
        public static string DisplayName(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException(nameof(role));
            }

            return role switch
            {
                nameof(System) => "Система",
                nameof(SystemAdmin) => "Администратор",
                nameof(EmployeeUser) => "Сотрудник",
                _ => throw new NotImplementedException("Unknown role."),
            };
        }
    }
}
