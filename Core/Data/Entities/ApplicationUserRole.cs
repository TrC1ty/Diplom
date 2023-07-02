// <copyright file="ApplicationUserRole.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Identity core class that is cross-reference between user and role and enables M:M mapping.
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        private ApplicationUser? user;
        private ApplicationRole? role;

        /// <summary>
        /// Gets or sets User part of relashionship.
        /// </summary>
        [BackingField(nameof(user))]
        public virtual ApplicationUser User
        {
            get => this.user ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.User)}.");
            set => this.user = value;
        }

        /// <summary>
        /// Gets or sets Role part of relashionship.
        /// </summary>
        [BackingField(nameof(role))]
        public virtual ApplicationRole Role
        {
            get => this.role ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.Role)}.");
            set => this.role = value;
        }
    }
}
