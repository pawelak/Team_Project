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
                var element = new CustomListItem
                {
                    Name = task.Name,
                    Description = activity.Status.ToString(),
                    Time = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                    Image = SelectImage(task.Typ)
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

        private static string SelectImage(string item)
        {
            string type;
            switch (item)
            {
                case "Sztuka":
                    type = "art.png";
                    break;
                case "Inne":
                    type = "OK.png";
                    break;
                case "Programowanie":
                    type = "programming.png";
                    break;
                case "Sport":
                    type = "sport.png";
                    break;
                case "Muzyka":
                    type = "music.png";
                    break;
                case "Języki":
                    type = "language.png";
                    break;
                case "Jedzenie":
                    type = "eat.png";
                    break;
                case "Rozrywka":
                    type = "instrument.png";
                    break;
                case "Podróż":
                    type = "car.png";
                    break;
                case "Przerwa":
                    type = "Cafe.png";
                    break;
                default:
                    type = "OK.png";
                    break;
            }
            return type;
        }
    }
}