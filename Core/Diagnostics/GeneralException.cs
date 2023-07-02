// <copyright file="GeneralException.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Diagnostics
{
    using System;

    /// <summary>
    /// General exception.
    /// </summary>
    public class GeneralException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralException"/> class.
        /// </summary>
        public GeneralException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public GeneralException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public GeneralException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
