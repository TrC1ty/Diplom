// <copyright file="LegalParticipant.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    /// <summary>
    /// EF entity that represents legal participant.
    /// </summary>
    public class LegalParticipant : Participant
    {
        /// <summary>
        /// Gets or sets legal name.
        /// </summary>
        public string? LegalName { get; set; }

        /// <summary>
        /// Gets or sets post address.
        /// </summary>
        public string? PostAddress { get; set; }

        /// <summary>
        /// Gets or sets mail.
        /// </summary>
        public string? Mail { get; set; }

        /// <summary>
        /// Gets or sets phone.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets ogrn.
        /// </summary>
        public string? Ogrn { get; set; }

        /// <summary>
        /// Gets or sets bic.
        /// </summary>
        public string? Bic { get; set; }

        /// <summary>
        /// Gets or sets correspondent account.
        /// </summary>
        public string? CorAccount { get; set; }

        /// <summary>
        /// Gets or sets okved.
        /// </summary>
        public string? Okved { get; set; }

        /// <summary>
        /// Gets or sets sro name.
        /// </summary>
        public string? SroName { get; set; }

        /// <summary>
        /// Gets or sets sro inn.
        /// </summary>
        public string? SroInn { get; set; }

        /// <summary>
        /// Gets or sets sro inn.
        /// </summary>
        public string? SroOgrn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sro is existed.
        /// </summary>
        public bool DoesItExistSro { get; set; }
    }
}
