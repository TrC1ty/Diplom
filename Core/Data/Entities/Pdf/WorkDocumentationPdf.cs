// <copyright file="WorkDocumentationPdf.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities.Pdf
{
    /// <summary>
    /// EF entity that represents work documentation.
    /// </summary>
    public class WorkDocumentationPdf
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkDocumentationPdf"/> class.
        /// </summary>
        public WorkDocumentationPdf()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkDocumentationPdf"/> class.
        /// </summary>
        /// <param name="data">Pdf data.</param>
        /// <param name="mimeType">Pdf mime type.</param>
        /// <param name="workDocumentation">Work documentation.</param>
        public WorkDocumentationPdf(byte[] data, string mimeType, WorkDocumentation workDocumentation)
        {
            this.Data = data;
            this.MimeType = mimeType;
            this.WorkDocumentation = workDocumentation;
        }

        /// <summary>
        /// Gets or sets pdf id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets pdf data.
        /// </summary>
        public byte[]? Data { get; set; }

        /// <summary>
        /// Gets or sets pdf extenstion.
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets work documentation instance.
        /// </summary>
        public WorkDocumentation? WorkDocumentation { get; set; }
    }
}
