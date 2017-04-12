using System.Collections.Generic;
using System.Diagnostics;
using TaskMaster.Pages;
using Xamarin.Forms;
using Xamarin.Forms.OAuth;

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
		    OAuthAuthenticator.AddPRovider(OAuthProviders.Facebook("647866935403018"));
		    OAuthAuthenticator.AddPRovider(OAuthProviders.Google("723494873981-qsnnp5vsa72f4d74bo8m8kqfsrbo25cq.apps.googleusercontent.com", "urn:ietf:wg:oauth:2.0:oob"));
            InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
            /*var user = new User()
            {
                Name = "Patryk"
            };
            Database.SaveUser(user);*/
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
