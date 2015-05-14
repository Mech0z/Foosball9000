using System;
using System.Collections.Generic;

namespace Common.Logging
{
    public interface ITaskTimer : IDisposable
    {
        Guid Id { get; }
        string Name { get; }
        DateTime StartedTime { get; }
        DateTime? StopedTime { get; }
        ITaskTimer Parent { get; }
        IEnumerable<ITaskTimer> Children { get; }

        void AddChild(ITaskTimer taskTimer);
    }
}