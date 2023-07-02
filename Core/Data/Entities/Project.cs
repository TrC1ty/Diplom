// <copyright file="Project.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF entity that represents project.
    /// </summary>
    public class Project
    {
        private ApplicationUser? ownerUser;

        /// <summary>
        /// Gets or sets project id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets project documentation.
        /// </summary>
        public string? ProjectDocumentation { get; set; }

        /// <summary>
        /// Gets or sets building address.
        /// </summary>
        public string? BuldingAddress { get; set; }

        /// <summary>
        /// Gets or sets datetime created.
        /// </summary>
        public DateTime? DateTimeCreated { get; set; }

        /// <summary>
        /// Gets or sets project code.
        /// </summary>
        public string? ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets user instance.
        /// </summary>
        [BackingField(nameof(this.ownerUser))]
        public ApplicationUser OwnerUser
        {
            get => this.ownerUser ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.ownerUser)}.");
            set => this.ownerUser = value;
        }

        /// <summary>
        /// Gets list of participants.
        /// </summary>
        public List<Participant> Participants { get; } = new List<Participant>();

        /// <summary>
        /// Gets list of project sections.
        /// </summary>
        public List<ProjectSection> Sections { get; } = new List<ProjectSection>();
    }
}
