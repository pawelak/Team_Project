using Foundation;
using System;
using System.Diagnostics;
using System.Linq;
using UIKit;
namespace TaskMaster.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        private readonly UserService _userService = new UserService();
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Xamarin.Forms.Forms.Init ();
            XamForms.Controls.iOS.Calendar.Init();
            LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
		}
	    public override void DidEnterBackground(UIApplication application)
	    {
            PauseActivities();
        }
        private async void PauseActivities()
        {
            var now = DateTime.Now;
            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            var result = await _userService.GetActivitiesByStatus(StatusType.Start);
            foreach (var activity in result)
            {
                activity.Status = StatusType.Pause;
                var actual = await _userService.GetLastActivityPart(activity.ActivityId);
                actual.Stop = date;
                var sw = new Stopwatch();
                var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == actual.PartId);
                if (stopwatch != null)
                    sw = stopwatch.GetStopwatch();
                actual.Duration = sw.ElapsedMilliseconds.ToString();
                await _userService.SaveActivity(activity);
                await _userService.SavePartOfActivity(actual);
            }
        }
    }
}
