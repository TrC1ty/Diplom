// <copyright file="LogWriterJob.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.SchedulerJobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Quartz;

    /// <summary>
    /// Job that runs in Quartz scheduler and writes to log.
    /// This is used for Quartz testing.
    /// </summary>
    public class LogWriterJob : IJob
    {
        private readonly ILogger<LogWriterJob> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogWriterJob"/> class.
        /// </summary>
        /// <param name="logger">Logging service.</param>
        public LogWriterJob(ILogger<LogWriterJob> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task Execute(IJobExecutionContext context)
        {
            // Write to log.
            this.logger.LogDebug("Execute called.");

            // Avoid warning by introducing await.
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
