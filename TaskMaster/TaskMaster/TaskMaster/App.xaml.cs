using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Plugin.LocalNotifications;
using TaskMaster.Pages;

namespace TaskMaster
{
	public partial class App
	{
	    private static UserDatabase _database;
	    public static List<Stopwatches> Stopwatches = new List<Stopwatches>();
        private readonly UserServices _userServices = new UserServices();
        public static UserDatabase Database => _database ?? (_database = new UserDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("UserSQLite.db3")));
	    public App ()
		{
            InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
            var result2 = await _userServices.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await _userServices.GetTaskById(activity.TaskId);
                var parts = await _userServices.GetPartsOfActivityByActivityId(activity.ActivityId);
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
