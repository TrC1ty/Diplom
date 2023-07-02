// <copyright file="EmailTemplatingService.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;

    using RazorLight;

    /// <summary>
    /// Service that generates HTML code from templates.
    /// </summary>
    public class EmailTemplatingService
    {
        private const string BaseEmailTemplatesFolderPath = @"\Pages\EmailTemplates";

        private readonly RazorLightEngine engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplatingService"/> class.
        /// </summary>
        /// <param name="webHostEnvironment">Web host environment.</param>
        public EmailTemplatingService(IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment is null)
            {
                throw new ArgumentNullException(nameof(webHostEnvironment));
            }

            this.engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(webHostEnvironment.ContentRootPath)
                .UseMemoryCachingProvider()
                .Build();
        }

        /// <summary>
        /// Finds specified page and executes it.
        /// </summary>
        /// <param name="pageFileNameWithoutExtension">File name wihtout .cshtml extension.</param>
        /// <param name="model">Model to pass to the page.</param>
        /// <returns>Resulting HTML.</returns>
        public async Task<string> GenerateHtml(string pageFileNameWithoutExtension, object model)
        {
            var templateFileName = Path.Combine(BaseEmailTemplatesFolderPath, pageFileNameWithoutExtension) + ".cshtml";

            var result = await this.engine.CompileRenderAsync(templateFileName, model);

            return result;
        }
    }
}
