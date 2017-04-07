using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskMaster.Models;
using TaskMaster.Pages;
using Xamarin.Forms;
using XamForms.Controls;
namespace TaskMaster.Pages
{

    public partial class CalendarDayListPage : ContentPage
    {
        private DateTime calendarDay;

        public CalendarDayListPage(DateTime dateTime)
        {
            this.calendarDay = dateTime;
            InitializeComponent();
             ListInitiate(); 
            //DisplayAlert("Task", calendarDay.ToString(), "Task", "Task");
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
                var parts = await App.Database.GetPartsOfActivityByStatus(calendarDay, activity.ActivityId);
                foreach (var part in parts)
                {
                    time += long.Parse(part.Duration);
                }
                var task = await App.Database.GetTaskById(activity.TaskId);
                var element = new CustomList()
                {
                    Name = task.Name,
                    Description = task.Description,
                    Time = DateTime.Parse(time.ToString("HH:mm")).ToShortTimeString()
                };
                dayPlan.Add(element);
            }
            var result2 = await App.Database.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                long time = 0;
                var parts = await App.Database.GetPartsOfActivityByStatus(calendarDay, activity.ActivityId);
                foreach (var part in parts)
                {
                    time += long.Parse(part.Duration);
                }
                var task = await App.Database.GetTaskById(activity.TaskId);
                var element = new CustomList()
                {
                    Name = task.Name,
                    Description = task.Description,
                    Time = DateTime.Parse(time.ToString("HH:mm")).ToShortTimeString()
                };
                dayPlan.Add(element);
            }
            DayPlan.ItemsSource = dayPlan;
        }
        public struct CustomList
        {
            public string Name;
            public string Description;
            public string Time;
        }
    }
}