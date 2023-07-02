// <copyright file="Work.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF entity that represents work.
    /// </summary>
    public class Work
    {
        private ProjectSection? section;

        /// <summary>
        /// Gets or sets work id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets name of project documentation.
        /// </summary>
        public string? NameProjectDoc { get; set; }

        /// <summary>
        /// Gets or sets number of project documentation.
        /// </summary>
        public string? NumberProjectDoc { get; set; }

        /// <summary>
        /// Gets or sets project drawing details.
        /// </summary>
        public string? ProjectDrawingDetails { get; set; }

        /// <summary>
        /// Gets or sets name of work documentation.
        /// </summary>
        public string? NameWorktDoc { get; set; }

        /// <summary>
        /// Gets or sets number of work documentation.
        /// </summary>
        public string? NumberWorkDoc { get; set; }

        /// <summary>
        /// Gets or sets project drawing details.
        /// </summary>
        public string? ProjectWorkDetails { get; set; }

        /// <summary>
        /// Gets or sets information about person who prepared documentation.
        /// </summary>
        public string? PersonInformation { get; set; }

        /// <summary>
        /// Gets or sets datetime when work was started.
        /// </summary>
        public DateTime? DateTimeWorkStarted { get; set; }

        /// <summary>
        /// Gets or sets datetime when work was ended.
        /// </summary>
        public DateTime? DateTimeWorkEnded { get; set; }

        /// <summary>
        /// Gets or sets additional information.
        /// </summary>
        public string? AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets next work.
        /// </summary>
        public string? NextWork { get; set; }

        /// <summary>
        /// Gets or sets number of instances.
        /// </summary>
        public int? InstancesTotal { get; set; }

        /// <summary>
        /// Gets or sets project id.
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <seealso cref="SectionId"/>
        [BackingField(nameof(section))]
        public ProjectSection Section
        {
            get => this.section ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.Section)}.");
            set => this.section = value;
        }

        /// <summary>
        /// Gets list of regulatory acts.
        /// </summary>
        public List<string> RegulatoryActs { get; } = new List<string>();

        /// <summary>
        /// Gets list of materials.
        /// </summary>
        public List<Material> Materials { get; } = new List<Material>();

        /// <summary>
        /// Gets list of executive schemes.
        /// </summary>
        public List<ExecutiveScheme> ExecutiveSchemes { get; } = new List<ExecutiveScheme>();
    }
}
