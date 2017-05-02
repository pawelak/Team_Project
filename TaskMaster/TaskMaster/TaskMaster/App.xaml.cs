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

        protected override void OnStart()
        {            
            
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
