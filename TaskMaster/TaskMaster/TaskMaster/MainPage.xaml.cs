using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;

namespace TaskMaster
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		    var user = new User()
		    {
		        Name = "Przykład",
		        Desc = "Jeszcze też przykład"
		    };
		    App.Database.SaveUser(user);
		}

	    private void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new StartTaskPage());
	    }

	    private void PlanTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new PlanTaskPage());
	    }

	    private void FastTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new FastTaskPage());
	    }

	    private void StatisticsButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new StatisticPage());
	    }
        private void CallendarButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CallendarPage());
        }
    }
}
