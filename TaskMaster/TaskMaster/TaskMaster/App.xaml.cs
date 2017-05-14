using System.Collections.Generic;
using Xamarin.Forms;

namespace TaskMaster
{
	public partial class App
	{
	    public static List<Stopwatches> Stopwatches = new List<Stopwatches>();
	    public App ()
		{
            InitializeComponent();
		    MainPage = new NavigationPage(new MainPage(true));
		}

        protected override void OnStart()
        {            
            
        }

        protected override void OnSleep ()
		{

		}

		protected override void OnResume ()
		{

		}
       
    }
}
