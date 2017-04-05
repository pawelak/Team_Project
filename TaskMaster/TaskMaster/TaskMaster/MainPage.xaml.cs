using System;
using TaskMaster.Pages;
using Xamarin.Forms;

namespace TaskMaster
{

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();
        }

	    private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new StartTaskPage());
	    }

	    private async void PlanTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new PlanTaskPage());
	    }

	    private async void FastTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new FastTaskPage());
	    }

	    private async void StatisticsButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new StatisticPage());
	    }
        private async void CalendarButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

	    private async void ActiveButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new ActiveTasksPage());
	    }

	    private void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }

	    private void HistoryPageItem_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}
