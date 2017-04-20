using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TaskMaster.Droid
{
	[Activity (Label = "TaskMaster", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
	    private readonly UserServices _userServices = new UserServices();
        protected override void OnCreate (Bundle bundle)
		{
            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate (bundle);

            Xamarin.Forms.Forms.Init (this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication (new App ());
		}
	    protected override void OnDestroy()
	    {
	        PauseActivities();
	    }

	    private async void PauseActivities()
	    {
	        var activities = await App.Database.GetActivitiesByStatus(StatusType.Start);
	        foreach (var activity in activities)
	        {
	            var part = await App.Database.GetLastActivityPart(activity.ActivityId);
                DateTime now = new DateTime();
	            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
	            part.Stop = date;
                App.Stopwatches[part.PartId-1].Stop();
	            part.Duration = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == part.PartId).GetTime().ToString();
                activity.Status = StatusType.Pause;
	            await _userServices.SaveActivity(activity);
	            await _userServices.SavePartOfActivity(part);
	        }
	    }
	}
}

