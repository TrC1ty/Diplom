// <copyright file="NaturalPerson.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Entities
{
    /// <summary>
    /// EF entity that represents natural person.
    /// </summary>
    public class NaturalPerson : Participant
    {
        /// <summary>
        /// Gets or sets surname.
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets patronymic.
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Gets or sets passport data.
        /// </summary>
        public string? PassportData { get; set; }

        /// <summary>
        /// Gets or sets post.
        /// </summary>
        public string? Post { get; set; }

        /// <summary>
        /// Gets or sets register of specialists.
        /// </summary>
        public string? RegisterOfSpecialists { get; set; }

        /// <summary>
        /// Gets or sets details of admin documentation.
        /// </summary>
        public string? DetailsAdminDoc { get; set; }
    }
}
