using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }

        private async void ListInitiate()
        {
            await AddActivitiesByStatus(StatusType.Stop);
            await AddActivitiesByStatus(StatusType.Planned);
            await AddActivitiesByStatus(StatusType.Pause);
            Device.BeginInvokeOnMainThread(() =>
            {
                DayPlan.ItemsSource = _dayPlan;
            });
        }

        private async Task AddActivitiesByStatus(StatusType status)
        {
            var activities = await UserService.Instance.GetActivitiesByStatus(status);
            foreach (var activity in activities)
            {
                var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                long time = 0;
                if (status == StatusType.Planned)
                {
                    var part = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
                    if (DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy") != _calendarDay.ToString("dd/MM/yyyy"))
                    {
                        continue;
                    }
                }
                else
                {
                    time += parts.Where(part => DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy") == _calendarDay.ToString("dd/MM/yyyy"))
                        .Sum(part => long.Parse(part.Duration));
                    if (time == 0)
                    {
                        continue;
                    }
                }
                var task = await UserService.Instance.GetTaskById(activity.TaskId);
                if (task == null)
                {
                    continue;
                }
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

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {                
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            });
            return true;
        }

        private void DayPlan_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //Nothing
        }
    }
}