using System;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.Pages;
using Xamarin.Forms;

namespace TaskMaster
{
	
	public partial class HistoryPage
	{
        private readonly UserService _userService = new UserService();
		public HistoryPage ()
		{
			InitializeComponent();
        }

	    private async void MainPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
            await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));
        }
        protected override void OnAppearing()
        {
            ListInitiate();
        }

	    private async void ListInitiate()
	    {
	        var result = await _userService.GetActivitiesByStatus(StatusType.Stop);
	        var historyPlan = new List<HistoryList>();
	        foreach (var activity in result)
	        {
	            var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
	            var time = parts.Sum(part => long.Parse(part.Duration));
	            var last = await _userService.GetLastActivityPart(activity.ActivityId);
	            var task = await _userService.GetTaskById(activity.TaskId);
	            var t = TimeSpan.FromMilliseconds(time);
	            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
	            var element = new HistoryList
	            {
	                Name = task.Name,
	                Description = task.Description,
	                Time = answer,
	                Date = last.Start
	            };
	            historyPlan.Add(element);
	        }
            HistoryPlan.ItemsSource = historyPlan;
        }
	}
}

