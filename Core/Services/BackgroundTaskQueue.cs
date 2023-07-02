// <copyright file="BackgroundTaskQueue.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Background task execution service.
    /// </summary>
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<CancellationToken, Task>> workItems = new();
        private readonly SemaphoreSlim newItemIsAvailableSignalSemaphore = new(0);

        /// <summary>
        /// Queues supplied work item for background execution.
        /// </summary>
        /// <param name="workItem">Work item to execute in background.</param>
        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            this.workItems.Enqueue(workItem);
            this.newItemIsAvailableSignalSemaphore.Release();
        }

        /// <summary>
        /// Dequeues next task. This method will wait asynchroneously until task is available.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel waiting if needed.</param>
        /// <returns>Next work item from the queue.</returns>
        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                // If there are items in the queue already -- return.
                if (this.workItems.TryDequeue(out var workItem))
                {
                    return workItem!;
                }

                // Otherwise wait to new item to be added.
                await this.newItemIsAvailableSignalSemaphore.WaitAsync(cancellationToken);
            }
        }
    }
}