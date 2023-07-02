// <copyright file="ProjectSection.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF entity that represents project section.
    /// </summary>
    public class ProjectSection
    {
        private Project? project;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSection"/> class.
        /// </summary>
        /// <param name="name">Section name.</param>
        public ProjectSection(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        /// Gets list of work.
        /// </summary>
        public List<Work> Works { get; } = new List<Work>();
    }
}
