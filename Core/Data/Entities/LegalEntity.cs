// <copyright file="LegalEntity.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    /// <summary>
    /// EF entity that represents legal entity.
    /// </summary>
    public class LegalEntity : LegalParticipant
    {
        /// <summary>
        /// Gets or sets kpp.
        /// </summary>
        public string? Kpp { get; set; }

        /// <summary>
        /// Gets or sets okpo.
        /// </summary>
        public string? Okpo { get; set; }

        /// <summary>
        /// Gets or sets okato.
        /// </summary>
        public string? Okato { get; set; }

        /// <summary>
        /// Gets or sets general manager.
        /// </summary>
        public string? GeneralManager { get; set; }

        /// <summary>
        /// Gets or sets site.
        /// </summary>
        public string? Site { get; set; }
    }
}
