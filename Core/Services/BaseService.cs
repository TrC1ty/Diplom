// <copyright file="BaseService.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The base service.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// The ensure identity result.
        /// </summary>
        /// <param name="identityResult">
        /// The identity result.
        /// </param>
#pragma warning disable CA1822 // Пометьте члены как статические
        protected void EnsureIdentityOperationResult(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new Exception(string.Join(", ", identityResult.Errors.Select(e => e.Description)));
            }
        }
    }
}
