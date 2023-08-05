// <copyright file="TestInitializer.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data.Initialization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Diplom.Core.Controllers;
    using Diplom.Core.Data.Entities;

    /// <summary>
    /// Database test initializer. Loads test data.
    /// </summary>
    public static class TestInitializer
    {
        /// <summary>
        /// Seed data.
        /// </summary>
        /// <param name="controller">Main controller.</param>
        /// <param name="dataContext">Application data context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task SeedWithDataAsync(MainController controller, ApplicationDbContext dataContext)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            ////// This is how to read embedded resource file if needed.
            ////ResourceReader.ReadResourceBytes("Image.jpg");
            ////цResourceReader.ReadResourceString("Text.txt");

            await dataContext.SaveChangesAsync();
        }
    }
}