using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Plugin.LocalNotifications;

namespace TaskMaster
{
	public partial class App
	{
	    private static UserDatabase _database;
	    public static List<Stopwatches> Stopwatches = new List<Stopwatches>();
        private readonly UserServices _userServices = new UserServices();

        public static UserDatabase Database
	    {
	        get
	        {
	            if (_database == null)
	            {
	                _database = new UserDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("UserSQlite.db3"));
	            }
                return _database;
            }
	    } 
		public App ()
		{
            InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
            /*var user = new User()
            {
                Name = "Patryk"
            };
            Database.SaveUser(user);*/
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

            // Handle when your app starts
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
