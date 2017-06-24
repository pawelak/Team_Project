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
        private readonly List<CustomListItem> _dayPlan = new List<CustomListItem>();
        private readonly string _calendarDayString;
        public CalendarDayListPage(DateTime dateTime)
        {
            _calendarDayString = dateTime.ToString("dd/MM/yyyy");
            InitializeComponent();
            DayListDate.Text = _calendarDayString;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ListInitiateAsync();
        }

        private async Task ListInitiateAsync()
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
                var task = await UserService.Instance.GetTaskById(activity.TaskId);
                if (task == null)
                {
                    continue;
                }
                long time = 0;
                if (status == StatusType.Planned)
                {
                    var part = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
                    if (DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy") != _calendarDayString)
                    {
                        continue;
                    }
                }
                else
                {
                    var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                    time += parts.Where(part => DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy") == _calendarDayString)
                        .Sum(part => long.Parse(part.Duration));
                    if (time == 0)
                    {
                        continue;
                    }
                }
                var t = TimeSpan.FromMilliseconds(time);
                var element = new CustomListItem
                {
                    Name = task.Name,
                    Description = activity.Status.ToString(),
                    Time = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}",
                    Image = ImagesService.Instance.SelectImage(task.Typ)
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
    }
}