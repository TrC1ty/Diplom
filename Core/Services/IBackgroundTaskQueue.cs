// <copyright file="IBackgroundTaskQueue.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for services that may execute background tasks. Based on https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services.
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Queues supplied work item for background execution.
        /// </summary>
        /// <param name="workItem">Work item to execute in background.</param>
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        /// <summary>
        /// Dequeues next task. This method will wait asynchroneously until task is available.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel waiting if needed.</param>
        /// <returns>Next work item from the queue.</returns>
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
