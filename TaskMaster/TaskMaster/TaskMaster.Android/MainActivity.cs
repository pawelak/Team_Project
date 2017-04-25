using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly UserService _userService = new UserService();

        protected override void OnCreate(Bundle bundle)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
        }
  
        protected override void OnStop()
        {
            //PauseActivities();
            //StartService(new Intent(this, typeof(BackgroundStopwatches)));
        }

        protected override void OnRestart()
        {
            //RestartActivities();
            //StopService(new Intent(this, typeof(BackgroundStopwatches)));
        }
        private static void PauseActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
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

