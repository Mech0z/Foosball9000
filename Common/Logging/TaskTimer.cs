using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Logging
{
    public class TaskTimer : ITaskTimer
    {
        private readonly object _syncRoot = new object();
        private readonly List<ITaskTimer> _taskTimers = new List<ITaskTimer>();

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
                lock (_syncRoot)
                {
                    return _taskTimers.ToList();
                }
            }
        }

        public void AddChild(ITaskTimer taskTimer)
        {
            lock (_syncRoot)
            {
                _taskTimers.Add(taskTimer);
            }
        }

        public void Dispose()
        {
            Disposer(this);
        }


    }
}