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
	        var activitiesStoppedList = await _userService.GetActivitiesByStatus(StatusType.Stop);
	        var historyPlan = new List<HistoryList>();
	        foreach (var activity in activitiesStoppedList)
	        {
	            if (activity.TaskId == 0)
	            {
	                var choose = await DisplayAlert("Error", "Masz nienazwaną zakończoną aktywność " +
	                                                         activity.ActivityId + ".\n" +
	                                                         "Czy chcesz uzupełnić jej dane?\n" +
	                                                         "Aktywność z nieuzupełnionymi danymi nie zostanie wyświetlona", "Tak", "Nie");
	                if (choose)
	                {
	                    await Navigation.PushModalAsync(new FillInformationPage(activity));
	                }
	                else
	                {
	                    continue;
	                }
	            }
	            var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
	            var time = parts.Sum(part => long.Parse(part.Duration));
	            var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
	            var task = await _userService.GetTaskById(activity.TaskId);
	            var t = TimeSpan.FromMilliseconds(time);
	            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
	            var element = new HistoryList
	            {
	                Name = task.Name,
	                Description = task.Description,
	                Time = answer,
	                Date = lastPart.Start
	            };
	            historyPlan.Add(element);
	        }
            HistoryPlan.ItemsSource = historyPlan;
        }
	}
}

