using System;
using System.Collections.Generic;
using TaskMaster.Pages;
using Xamarin.Forms;

namespace TaskMaster
{
	
	public partial class HistoryPage
	{
		public HistoryPage ()
		{
			InitializeComponent();
            ListInitiate();
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
            var result = await App.Database.GetActivitiesByStatus(StatusType.Stop);
            List<HistoryList> dayPlan = new List<HistoryList>();
            foreach (var activity in result)
            {
                long time = 0;
                var parts = await App.Database.GetPartsOfActivityByActivityId(activity.ActivityId);
                foreach (var part in parts)
                {
                    time += long.Parse(part.Duration);
                }
                var task = await App.Database.GetTaskById(activity.TaskId);
                TimeSpan t = TimeSpan.FromMilliseconds(time);
                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                    t.Hours,
                    t.Minutes,
                    t.Seconds,
                    t.Milliseconds);
                var element = new HistoryList
                {
                    Name = task.Name,
                    Description = task.Description,
                    Time = answer,
                    Date = ""
                };
                dayPlan.Add(element);
            }
            DayPlan.ItemsSource = dayPlan;
        }
	}
}

