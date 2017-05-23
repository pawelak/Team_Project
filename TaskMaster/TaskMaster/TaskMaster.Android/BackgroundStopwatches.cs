using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using TaskMaster.ModelsDto;
using TaskMaster.Services;
using System.Timers;

namespace TaskMaster.Droid
{
    [Service]
    public class BackgroundStopwatches : Service
    {
        private readonly Timer _timer = new Timer();
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (StopwatchesService.Instance.CountStopwatches() == 0)
            {
                return StartCommandResult.Sticky;
            }
            _timer.Elapsed += UpdateTimes;
            _timer.Interval = 60000;
            _timer.Start();
            return StartCommandResult.Sticky;
        }

        private static async void UpdateTimes(object source, ElapsedEventArgs e)
        {
            var running = StopwatchesService.Instance.GetActiveStopwatchesPartsId();
            foreach (var item in running)
            {
                var part = await Services.UserService.Instance.GetPartsOfActivityById(item);
                var t = StopwatchesService.Instance.GetStopwatchTime(item);
                var time = long.Parse(part.Duration) + t;
                part.Duration = time.ToString();
                await Services.UserService.Instance.SavePartOfActivity(part);
                StopwatchesService.Instance.RestartStopwatch(item);
            }
        }
        
        public override void OnDestroy()
        {
            _timer.Stop();
            base.OnDestroy();
        }
    }
}