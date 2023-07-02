// <copyright file="IndividualEntrepreneur.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    /// <summary>
    /// EF entity that represents individual entrepreneur.
    /// </summary>
    public class IndividualEntrepreneur : LegalParticipant
    {
        /// <summary>
        /// Gets or sets short name.
        /// </summary>
        public string? ShortName { get; set; }

        /// <summary>
        /// Gets or sets bank name.
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Gets or sets taxation system.
        /// </summary>
        public string? TaxationSystem { get; set; }
    }
}
