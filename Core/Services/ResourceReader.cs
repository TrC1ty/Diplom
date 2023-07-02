// <copyright file="ResourceReader.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Diplom.Core.Diagnostics;

    /// <summary>
    /// Static class that helps with reading embedded resources.
    /// </summary>
    internal static class ResourceReader
    {
        /// <summary>
        /// Reads embedded resource as binary data from assembly that contains this class.
        /// </summary>
        /// <param name="embeddedFileName">Name of the file to read. Folder part may be omitted or specified in dots notations (e.g. "HumanTouch.Reviews.Core.Data.Resources.Logo.png"). Name is case-sensitive.</param>
        /// <returns>Resource file data.</returns>
        public static byte[] ReadResourceBytes(string embeddedFileName)
        {
            if (string.IsNullOrWhiteSpace(embeddedFileName))
            {
                throw new ArgumentException($"'{nameof(embeddedFileName)}' cannot be null or whitespace.", nameof(embeddedFileName));
            }

            using var resourceStream = GetResourceStream(embeddedFileName);
            using var memoryStream = new MemoryStream();
            resourceStream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Reads embedded resource as string from assembly that contains this class.
        /// </summary>
        /// <param name="embeddedFileName">Name of the file to read. Folder part may be omitted or specified in dots notations (e.g. "HumanTouch.Reviews.Core.Data.Resources.Logo.png"). Name is case-sensitive.</param>
        /// <returns>Resource file as string.</returns>
        public static string ReadResourceString(string embeddedFileName)
        {
            if (string.IsNullOrWhiteSpace(embeddedFileName))
            {
                throw new ArgumentException($"'{nameof(embeddedFileName)}' cannot be null or whitespace.", nameof(embeddedFileName));
            }

            using var resourceStream = GetResourceStream(embeddedFileName);
            using var streamReader = new StreamReader(resourceStream);

            return streamReader.ReadToEnd();
        }

        private static Stream GetResourceStream(string embeddedFileName)
        {
            var assembly = typeof(ResourceReader).GetTypeInfo().Assembly;

            var resourceName = assembly.GetManifestResourceNames().Single(s => s.EndsWith(embeddedFileName, StringComparison.Ordinal));

            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                throw new GeneralException("Could not load manifest resource stream. No resources were specified during compilation or the resource is not visible to the caller.");
            }

            return resourceStream;
        }
    }
}