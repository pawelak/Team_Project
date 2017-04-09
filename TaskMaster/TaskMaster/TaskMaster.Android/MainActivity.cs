using System;
using System.Diagnostics;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TaskMaster.Models;

namespace TaskMaster.Droid
{
	[Activity (Label = "TaskMaster", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication (new TaskMaster.App ());
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
	            part.Duration = App.Stopwatches[part.PartId-1].ElapsedMilliseconds.ToString();
                activity.Status = StatusType.Pause;
	            await App.Database.SaveActivity(activity);
	            await App.Database.SavePartOfTask(part);
	        }
	    }
	}
}

