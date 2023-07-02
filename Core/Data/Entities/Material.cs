// <copyright file="Material.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    /// <summary>
    /// EF entity that represents material.
    /// </summary>
    public class Material : WorkDocumentation
    {
        /// <summary>
        /// Gets or sets count.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Gets or sets units of measurement.
        /// </summary>
        public string? MeasurementUnits { get; set; }

        /// <summary>
        /// Gets or sets provider.
        /// </summary>
        public string? Provider { get; set; }

        /// <summary>
        /// Gets or sets certificate name.
        /// </summary>
        public string? CertificateName { get; set; }

        /// <summary>
        /// Gets or sets certificate number.
        /// </summary>
        public string? CertificateNumber { get; set; }
    }
}
