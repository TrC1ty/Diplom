// <copyright file="Participant.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using Diplom.Core.Data.Enums;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF entity that represents participant.
    /// </summary>
    public class Participant
    {
        private Project? project;

        /// <summary>
        /// Gets or sets participant id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets prticipant inn.
        /// </summary>
        public string? Inn { get; set; }

        /// <summary>
        /// Gets or sets participant address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets participant type.
        /// </summary>
        public ParticipantType ParticipantType { get; set; }

        /// <summary>
        /// Gets or sets project id.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <seealso cref="ProjectId"/>
        [BackingField(nameof(project))]
        public Project Project
        {
            get => this.project ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.Project)}.");
            set => this.project = value;
        }
    }
}
