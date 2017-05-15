using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskMaster.Services
{
    public class StopwatchesService
    {
        private static StopwatchesService _instance;
        private readonly List<StopwatchWrapper> _stopwatches = new List<StopwatchWrapper>();

        public void AddStopwatch(int id)
        {
            var stopwatch = new StopwatchWrapper(id);
            _stopwatches.Add(stopwatch);
            _stopwatches[_stopwatches.Count - 1].Start();
        }

        private StopwatchesService()
        {
            
        }

        public List<int> GetActiveStopwatchesPartsId()
        {
            return (from stopwatch in _stopwatches where stopwatch.IsRunning select stopwatch.GetPartId()).ToList();
        }
        public void StopStopwatch(int id)
        {
            _stopwatches.FirstOrDefault(s => s.GetPartId() == id).Stop();
        }

        public void RestartStopwatch(int id)
        {
            _stopwatches.FirstOrDefault(s => s.GetPartId() == id).Restart();
        }

        public long GetStopwatchTime(int id)
        {
            long time;
            var stopwatch = _stopwatches.FirstOrDefault(s => s.GetPartId() == id);
            if (stopwatch == null)
            {
                time = -1;
            }
            else
            {
                time = stopwatch.ElapsedMilliseconds;
            }
            return time;
        }

        public int CountStopwatches()
        {
            return _stopwatches.Count;
        }

        public static StopwatchesService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StopwatchesService();
                }
                return _instance;
            }
        }
    }
}
