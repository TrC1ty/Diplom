// <copyright file="SectionRights.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Section rights.
    /// </summary>
    public class SectionRights
    {
        private ApplicationUser? user;

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
