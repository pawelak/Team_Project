using System;
using System.Diagnostics;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly UserServices _userServices = new UserServices();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
        }

        protected override void OnDestroy()
        {
            PauseActivities();
        }

        private async void PauseActivities()
        {
            var now = DateTime.Now;
            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            var result = await _userServices.GetActivitiesByStatus(StatusType.Start);
            foreach (var activity in result)
            {
                activity.Status = StatusType.Pause;
                var actual = await _userServices.GetLastActivityPart(activity.ActivityId);
                actual.Stop = date;
                var sw = new Stopwatch();
                var firstOrDefault = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == actual.PartId);
                if (firstOrDefault != null)
                    sw = firstOrDefault.GetStopwatch();
                actual.Duration = sw.ElapsedMilliseconds.ToString();
                await _userServices.SaveActivity(activity);
                await _userServices.SavePartOfActivity(actual);
            }
        }
    }
}

