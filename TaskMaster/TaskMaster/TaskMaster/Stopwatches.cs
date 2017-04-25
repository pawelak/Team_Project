using System.Diagnostics;

namespace TaskMaster
{
    public class Stopwatches
    {
        private readonly Stopwatch _stopwatch;
        private readonly int _partId;
        public Stopwatches(Stopwatch sw, int id)
        {
            _stopwatch = sw;
            _partId = id;
        }

        public Stopwatch GetStopwatch()
        {
            return _stopwatch;
        }

        public int GetPartId()
        {
            return _partId;
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public long GetTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }
    }
}
