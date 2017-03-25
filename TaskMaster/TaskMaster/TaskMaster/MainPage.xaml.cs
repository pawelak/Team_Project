using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskMaster
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
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
	}
}
