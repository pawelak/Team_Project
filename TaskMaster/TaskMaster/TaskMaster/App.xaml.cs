using System.Collections.Generic;
using Xamarin.Forms;
using System;
using Plugin.LocalNotifications;

namespace TaskMaster
{
	public partial class App
	{
	    public static List<Stopwatches> Stopwatches = new List<Stopwatches>();
        private readonly UserService _userService = new UserService();
	    public App ()
		{
            InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {            
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await _userService.GetTaskById(activity.TaskId);
                var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                foreach (var part in parts)
                {
                    CrossLocalNotifications.Current.Show(task.Name, "Za 5 minut", part.PartId, DateTime.Parse(part.Start).AddMinutes(-5)); 
                }
            }            
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
       
    }
}
