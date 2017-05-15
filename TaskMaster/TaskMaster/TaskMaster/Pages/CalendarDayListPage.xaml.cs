using System;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.Services;
using Xamarin.Forms;

namespace TaskMaster.Pages
{

    public partial class CalendarDayListPage
    {
        private readonly List<CustomList> _dayPlan = new List<CustomList>();
        private DateTime _calendarDay;
        public CalendarDayListPage(DateTime dateTime)
        {
            _calendarDay = dateTime;
            InitializeComponent();
            ListInitiate();
            Device.BeginInvokeOnMainThread(() =>
            {
                DayPlan.ItemsSource = _dayPlan;
            });
        }
        protected override void OnAppearing()
        {
            ListInitiate();
        }

        private void ListInitiate()
        {
            AddActivitiesByStatus(StatusType.Stop);
            AddActivitiesByStatus(StatusType.Planned);
            AddActivitiesByStatus(StatusType.Pause);
        }

        private async void AddActivitiesByStatus(StatusType status)
        {
            var activities = await UserService.Instance.GetActivitiesByStatus(status);
            foreach (var activity in activities)
            {
                var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                var time = parts.Where(part => DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null)
                        .ToString("dd/MM/yyyy")
                        .Equals(_calendarDay.ToString("dd/MM/yyyy")))
                    .Sum(part => long.Parse(part.Duration));
                var task = await UserService.Instance.GetTaskById(activity.TaskId);
                var t = TimeSpan.FromMilliseconds(time);
                var element = new CustomList
                {
                    Name = task.Name,
                    Description = activity.Status.ToString(),
                    Time = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s"
                };
                _dayPlan.Add(element);
            }
        }

        private void DayPlan_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {                
                await Navigation.PushModalAsync(new InitializeCalendar());
            });
            return true;
        }
    }
}