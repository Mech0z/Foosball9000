using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Logging
{
    public class TaskTimer : ITaskTimer
    {
        private readonly object syncRoot = new object();
        private readonly List<ITaskTimer> taskTimers = new List<ITaskTimer>();

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StopedTime { get; set; }
        public ITaskTimer Parent { get; set; }
        public DateTime StartedTime { get; set; }

        public bool IsDisposed
        {
            get { return StopedTime.HasValue; }
        }

        public Action<TaskTimer> Disposer { private get; set; }

        public IEnumerable<ITaskTimer> Children
        {
            get
            {
                lock (syncRoot)
                {
                    return taskTimers.ToList();
                }
            }
        }

        public void AddChild(ITaskTimer taskTimer)
        {
            lock (syncRoot)
            {
                taskTimers.Add(taskTimer);
            }
        }

        public void Dispose()
        {
            Disposer(this);
        }


    }
}