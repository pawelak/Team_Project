using System;
using System.Collections.Generic;

namespace TaskMaster.Pages
{

    public partial class CalendarDayListPage
    {
        private DateTime _calendarDay;

        public CalendarDayListPage(DateTime dateTime)
        {
            _calendarDay = dateTime;
            InitializeComponent();
            ListInitiate();
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
            var result2 = await App.Database.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                long time = 0;
                var parts = await App.Database.GetPartsOfActivityByActivityId(activity.ActivityId);
                foreach (var part in parts)
                {
                    if (DateTime.Parse(part.Start).ToString("dd/MM/yyyy").Equals(_calendarDay.ToString("dd/MM/yyyy")))
                    {
                        time += long.Parse(part.Duration);
                    }
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
    }
}