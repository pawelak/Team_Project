﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskMaster.ModelsDto;
using TaskMaster.Pages;
using Xamarin.Forms;
namespace TaskMaster
{
	public partial class MainPage
    {
        private readonly UserServices _userServices = new UserServices();
        public MainPage()
		{
            InitializeComponent ();
		    ListInitiate();
		    TasksRestart();
		}

        private async void TasksRestart()
        {
            DateTime now = new DateTime();
            var result = await _userServices.GetActivitiesByStatus(StatusType.Pause);
            foreach (var activity in result)
            {
                bool decision;
                if (activity.TaskId == 0)
                {
                    var task = await _userServices.GetTaskById(activity.TaskId);
                    decision = await DisplayAlert("Wznowienie", "Czy chcesz wznowić aktywność " + task.Name + "?", "Tak", "Nie");
                }
                else
                    decision = await DisplayAlert("Wznowienie", "Czy chcesz wznowić aktywność Unnamed Activity" + activity.ActivityId + "?", "Tak", "Nie");
                if (!decision) continue;
                activity.Status = StatusType.Start;
                var part = new PartsOfActivityDto
                {
                    ActivityId = activity.ActivityId,
                    Start = now.ToString("HH:mm:ss dd/MM/yyyy")
                };
                part.PartId = await _userServices.SavePartOfActivity(part);
                var sw = new Stopwatch();
                Stopwatches stopwatch = new Stopwatches(sw, part.PartId);
                App.Stopwatches.Add(stopwatch);
                App.Stopwatches[App.Stopwatches.Count - 1].Start();
                await _userServices.SaveActivity(activity);
            }
        }
	    protected override void OnAppearing()
	    {
	        ListInitiate();
	    }

	    private async void ListInitiate()
	    {
	        var activeTasksList = new List<MainPageList>();
            var result = await _userServices.GetActivitiesByStatus(StatusType.Start);
	        foreach (var activity in result)
	        {
	            if (activity.TaskId == 0)
	            {
	                var item = new MainPageList
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	            else
	            {
	                var task = await _userServices.GetTaskById(activity.TaskId);
	                var item = new MainPageList
	                {
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	        }
            var result2 = await _userServices.GetActivitiesByStatus(StatusType.Pause);
	        foreach (var activity in result2)
	        {
	            if (activity.TaskId == 0)
	            {
	                var item = new MainPageList
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	            else
	            {
	                var task = await _userServices.GetTaskById(activity.TaskId);
	                var item = new MainPageList
	                {
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId,
	                    Duration = "0",
                        Image = " "
	                };
	                activeTasksList.Add(item);
	            }
	        }
            ActiveTasks.ItemsSource = activeTasksList;
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
	        var activity = new ActivitiesDto
	        {
	            Status = StatusType.Start,
	            UserId = 1,
	            GroupId = 1,
                TaskId = 0
	        };
	        activity.ActivityId = await _userServices.SaveActivity(activity);
	        DateTime now = DateTime.Now;
	        var part = new PartsOfActivityDto
	        {
	            ActivityId = activity.ActivityId,
	            Start = now.ToString("HH:mm:ss dd/MM/yyyy"),
                Duration = "0"
	        };
	        var result = await _userServices.SavePartOfActivity(part);
	        Stopwatch sw = new Stopwatch();
            Stopwatches stopwatch = new Stopwatches(sw,result);
	        App.Stopwatches.Add(stopwatch);
	        App.Stopwatches[App.Stopwatches.Count - 1].Start();
	        ListInitiate();
	    }

	    private async void InitializeCalendarItem_OnClicked(object sender, EventArgs e)
	    {
            await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));

        }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
        { 
            await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
	    }

       

	    private async void ActiveTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var item = (MainPageList) e.Item;
	        await Navigation.PushModalAsync(new EditTaskPage(item));
	    }
	}
}
