using System;
using System.Diagnostics;

namespace Common.Logging
{
    public class UnitOfWorkTimer : IDisposable
    {
        private readonly Action<Stopwatch> _action;

        public UnitOfWorkTimer(Action<Stopwatch> action = null)
        {
            _action = action ?? (s => Console.WriteLine(s.ElapsedMilliseconds));
            Watch = new Stopwatch();
            Watch.Start();
        }

        public Stopwatch Watch { get; set; }

        public void Dispose()
        {
            Watch.Stop();
            _action(Watch);
        }
    }
}