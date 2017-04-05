using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.Models;
using TaskMaster.Pages;
using Xamarin.Forms;

namespace TaskMaster
{

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent ();
		    //var taskList = ListInitiate().Result;
		    //ActiveTasks.ItemsSource = taskList;
		}

        private async Task<List<Tasks>> ListInitiate()
        {
            var result = await App.Database.GetActivitiesByStatus(StatusType.Start);
            List<Tasks> activeTasks = new List<Tasks>();
            foreach (var activity in result)
            {
                var task = await App.Database.GetTaskById(activity.TaskId);
                activeTasks.Add(task);
            }
            return activeTasks;
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
	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CalendarPage());
	    }
	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new HistoryPage());
	    }
	    /*private void ActiveTasks_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }*/
	}
}
