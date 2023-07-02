// <copyright file="WorkDocumentation.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    using Diplom.Core.Data.Entities.Pdf;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF entity that represents work documentation.
    /// </summary>
    public class WorkDocumentation
    {
        private Work? work;

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets number.
        /// </summary>
        public string? Number { get; set; }

        /// <summary>
        /// Gets or sets datetime when documentation was created.
        /// </summary>
        public DateTime? DateTimeStarted { get; set; }

        /// <summary>
        /// Gets or sets datetime when documentation was ended.
        /// </summary>
        public DateTime? DateTimeEnded { get; set; }

        /// <summary>
        /// Gets or sets number of pages.
        /// </summary>
        public int? PagesTotal { get; set; }

        /// <summary>
        /// Gets or sets documentation pdf.
        /// </summary>
        public WorkDocumentationPdf? Pdf { get; set; }

        /// <summary>
        /// Gets or sets project id.
        /// </summary>
        public int WorkId { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <seealso cref="WorkId"/>
        [BackingField(nameof(work))]
        public Work Work
        {
            get => this.work ?? throw new InvalidOperationException($"Access to uninitialized property: {nameof(this.Work)}.");
            set => this.work = value;
        }
    }
}
