using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskMaster.ModelsDto;
using TaskMaster.Pages;
using Xamarin.Forms;
namespace TaskMaster
{
	public partial class MainPage
    {
        private List<MainPageList> _activeTasksList = new List<MainPageList>();
        private readonly UserService _userService = new UserService();
        public MainPage()
		{
            InitializeComponent ();
		    ListInitiate();
        }

        protected override void OnAppearing()
	    {
	        ListInitiate();
	    }

        private bool UpdateTime()
        {
            if (_activeTasksList.Count <= 0)
            {
                return false;
            }
            foreach (var item in _activeTasksList)
            {
                item.Time += 1000;
                var t = TimeSpan.FromMilliseconds(item.Time);
                var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
                item.Duration = answer;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                ActiveTasks.ItemsSource = _activeTasksList;
            });
            return true;
        }

        private async void ListInitiate()
	    {
            var activitiesStarted = await _userService.GetActivitiesByStatus(StatusType.Start);
	        foreach (var activity in activitiesStarted)
	        {
	            if (activity.TaskId == 0)
	            {
	                long time = 0;
	                var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
	                var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
	                if (stopwatch != null)
	                {
	                    time = stopwatch.GetStopwatch().ElapsedMilliseconds;
	                }
	                var item = new MainPageList
	                {
	                    MyImageSource = ImageChoice(activity.Status),
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId,
	                    Duration = "0",
	                    Time = time
	                };
	                _activeTasksList.Add(item);
	            }
	            else
	            {
	                var task = await _userService.GetTaskById(activity.TaskId);
	                var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
	                var time = parts.Sum(part => long.Parse(part.Duration));
	                var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
	                var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
	                if (stopwatch != null)
	                {
	                    time += stopwatch.GetStopwatch().ElapsedMilliseconds;
	                }
	                var item = new MainPageList
	                {
	                    MyImageSource = ImageChoice(activity.Status),
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId,
	                    Duration = "0",
	                    Time = time
	                };
	                _activeTasksList.Add(item);
	            }
            }
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Pause);
	        foreach (var activity in result2)
	        {
	            if (activity.TaskId == 0)
	            {
	                long time = 0;
	                var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
	                var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
	                if (stopwatch != null)
	                {
	                    time = stopwatch.GetStopwatch().ElapsedMilliseconds;
	                }
                    var item = new MainPageList
	                {
                        MyImageSource =  ImageChoice(activity.Status),
                        Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId,
	                    Duration = "0",
                        Time = time
	                };
	                _activeTasksList.Add(item);
	            }
	            else
	            {
	                var task = await _userService.GetTaskById(activity.TaskId);
	                var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
	                var time = parts.Sum(part => long.Parse(part.Duration));
	                var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
	                var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() ==lastPart.PartId);
	                if (stopwatch != null)
	                {
	                    time += stopwatch.GetStopwatch().ElapsedMilliseconds;
	                }
                    var item = new MainPageList
	                {
                        MyImageSource = ImageChoice(activity.Status),
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId,
	                    Duration = "0",
                        Time = time
	                };
	                _activeTasksList.Add(item);
	            }
	        }

	        Device.BeginInvokeOnMainThread(() =>
	        {
	            ActiveTasks.ItemsSource = _activeTasksList;
	        });
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
        }

        private static string ImageChoice(StatusType status)
        {
            return status==StatusType.Start ? "playButton.png" : "pauseButton.png";
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
	        activity.ActivityId = await _userService.SaveActivity(activity);
	        DateTime now = DateTime.Now;
	        var part = new PartsOfActivityDto
	        {
	            ActivityId = activity.ActivityId,
	            Start = now.ToString("HH:mm:ss dd/MM/yyyy"),
                Duration = "0"
	        };
	        var result = await _userService.SavePartOfActivity(part);
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
