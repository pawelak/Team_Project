using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotifications;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly UserService _userService = new UserService();

        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await _userService.GetTaskById(activity.TaskId);
                var part = await _userService.GetLastActivityPart(activity.ActivityId);
                if (DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null) < DateTime.Now)
                {
                    part.Stop = part.Start;
                    activity.Status = StatusType.Stop;
                    await _userService.SaveActivity(activity);
                    await _userService.SavePartOfActivity(part);
                    continue;
                }
                CrossLocalNotifications.Current.Show(task.Name, "Za 5 minut", part.PartId, DateTime.Parse(part.Start).AddMinutes(-5));
            }
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            PauseActivities();
            StartService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnPause();
        }

        protected override void OnResume()
        {
            RestartActivities();
            StopService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnResume();
        }

        private async void PauseActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
                var part = await _userService.GetPartsOfActivityById(item.GetPartId());
                part.Duration = item.GetTime().ToString();
                await _userService.SavePartOfActivity(part);
                item.Stop();
            }
        }

        private static void RestartActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
                item.Restart();
            }
        }
    }
}

