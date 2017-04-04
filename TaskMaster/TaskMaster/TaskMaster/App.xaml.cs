using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using TaskMaster.Models;

namespace TaskMaster
{
	public partial class App : Application
	{
	    private static UserDatabase _database;
	    public static List<Stopwatch> Stopwatches = new List<Stopwatch>();
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
            var user = new User()
            {
                Name = "Patryk"
            };
            Database.SaveUser(user);
		}

		protected override void OnStart ()
		{
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
