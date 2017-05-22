using System.Diagnostics;

namespace TaskMaster
{
    public class StopwatchWrapper : Stopwatch
    {
        private readonly int _partId;
        public StopwatchWrapper(int id)
        {
            _partId = id;
        }

        public int GetPartId()
        {
            return _partId;
        }        
    }
}
