using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		    ListInitiate();
		}

	    protected override void OnAppearing()
	    {
	        ListInitiate();
	    }

        private async void ListInitiate()
        {
            var result = await App.Database.GetActivitiesByStatus(StatusType.Start);
            List<Tasks> activeTasks = new List<Tasks>();
            foreach (var activity in result)
            {
                var task = await App.Database.GetTaskById(activity.TaskId);
                activeTasks.Add(task);
            }
            ActiveTasks.ItemsSource = activeTasks;
        }
        
	    private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new StartTaskPage());
	    }

	    private async void PlanTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new PlanTaskPage());
	    }

	    private async void FastTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        var activity = new Activities()
	        {
	            Status = StatusType.Start
	        };
	        activity.ActivityId = await App.Database.SaveActivity(activity);
            DateTime now = DateTime.Now;
            var part = new PartsOfActivity()
            {
                ActivityId = activity.ActivityId,
                Start = now.ToString("HH:mm:ss dd/MM/yyyy")
            };
	        await App.Database.SavePartOfTask(part);
            Stopwatch sw = new Stopwatch();
            App.Stopwatches.Add(sw);
            App.Stopwatches[App.Stopwatches.Count-1].Start();
        }
	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new CalendarPage());
	    }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new HistoryPage());
	    }

	    private void ActiveTasks_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}
