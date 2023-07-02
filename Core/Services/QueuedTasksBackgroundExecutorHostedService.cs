// <copyright file="QueuedTasksBackgroundExecutorHostedService.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Executes tasks from DI-supplied <see cref="IBackgroundTaskQueue"/> tasks queue in background running as hosted service.
    /// </summary>
    public class QueuedTasksBackgroundExecutorHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue taskQueue;
        private readonly ILogger<QueuedTasksBackgroundExecutorHostedService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuedTasksBackgroundExecutorHostedService"/> class.
        /// </summary>
        /// <param name="taskQueue">Task queue service to get tasks from. Normally supplied by DI.</param>
        /// <param name="logger">Logging service.</param>
        public QueuedTasksBackgroundExecutorHostedService(IBackgroundTaskQueue taskQueue, ILogger<QueuedTasksBackgroundExecutorHostedService> logger)
        {
            this.taskQueue = taskQueue;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("QueuedTasksBackgroundExecutorHostedService is stopping now.");

            await base.StopAsync(stoppingToken);
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("QueuedTasksBackgroundExecutorHostedService is running now.");

            await this.PerformBackgroundProcessing(stoppingToken);
        }

        private async Task PerformBackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await this.taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "Error occurred executing work item (target is '{WorkItem}').", workItem?.Target);
                }
            }
        }
    }
}