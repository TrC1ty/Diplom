// <copyright file="UserRIghts.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User rights.
    /// </summary>
    public class UserRights
    {
        private ApplicationUser? user;

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user can create a project.
        /// </summary>
        public bool CanCreateProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user can delete a project.
        /// </summary>
        public bool CanDeleteProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user can archive a project.
        /// </summary>
        public bool CanArchiveProject { get; set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets user instance.
        /// </summary>
        [BackingField(nameof(this.user))]
        public ApplicationUser User
        {
            get => this.user ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.user)}.");
            set => this.user = value;
        }
    }
}
