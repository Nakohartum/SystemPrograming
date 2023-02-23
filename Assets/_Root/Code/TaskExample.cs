using System.Threading;
using System.Threading.Tasks;

namespace _Root.Code
{
    public class TaskExample
    {
        public static async Task<bool> WhatTaskFasterAsync(CancellationToken ct, Task task1, Task task2)
        {
            var any = await Task.WhenAny(task1, task2);
            return await Task.FromResult(any == task1 && !ct.IsCancellationRequested);
        }
    }
}