using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskMaster.Models;
using TaskMaster.Pages;
using Xamarin.Forms;
using XamForms.Controls;


namespace TaskMaster
{
	
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent();
            ListInitiate();
        }

	    private async void MainPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new MainPage());
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new CalendarPage());
	    }
        protected override void OnAppearing()
        {
            ListInitiate();
        }

        private async void ListInitiate()
        {
            var result = await App.Database.GetActivitiesByStatus(StatusType.Stop);
            List<CustomList> dayPlan = new List<CustomList>();
            foreach (var activity in result)
            {
                long time = 0;
                var parts = await App.Database.GetPartsOfActivityByActivityId(activity.ActivityId);
                foreach (var part in parts)
                {
                    time += long.Parse(part.Duration);
                }
                var task = await App.Database.GetTaskById(activity.TaskId);
                var element = new CustomList()
                {
                    Name = task.Name,
                    Description = task.Description,
                    Time = time.ToString()
                };
                dayPlan.Add(element);
            }
           
            DayPlan.ItemsSource = dayPlan;
        }
        public struct CustomList
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Time { get; set; }
        }
	}
}

