using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TaskMaster.ModelsDto;
using TaskMaster.Pages;
using Xamarin.Forms;
using TaskMaster.Services;
namespace TaskMaster
{
    public partial class MainPage
    {
        private readonly Timer _listTimer = new Timer();
        private readonly List<MainPageList> _activeTasksList = new List<MainPageList>();

        public MainPage()
        {
            InitializeComponent();            
        }

        protected override async void OnAppearing()
        {
            await ListInitiate();
            _listTimer.Elapsed += UpdateTime;
            _listTimer.Interval = 1000;
            _listTimer.Start();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _listTimer.Stop();
            base.OnDisappearing();
        }

        private void UpdateTime(object source, ElapsedEventArgs e)
        {
            if (_activeTasksList.Count <= 0)
            {
                return;
            }
            foreach (var item in _activeTasksList)
            {
                if (item.Status != StatusType.Start)
                {
                    continue;
                }
                var time = item.Time + StopwatchesService.Instance.GetStopwatchTime(item.PartId);
                var t = TimeSpan.FromMilliseconds(time);
                var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
                item.Duration = answer;
            }
            UpdateList();
        }

        private void UpdateList()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ActiveTasks.ItemsSource = null;
                ActiveTasks.ItemsSource = _activeTasksList;
            });
        }

        private async Task GetStartedActivities()
        {
            var activitiesStarted = await UserService.Instance.GetActivitiesByStatus(StatusType.Start);
            foreach (var activity in activitiesStarted)
            {
                if (activity.TaskId == 0)
                {
                    var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
                    var stopwatchTime = StopwatchesService.Instance.GetStopwatchTime(lastPart.PartId);
                    if (stopwatchTime == -1)
                    {
                        await StartupResume(activity);
                    }
                    var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Sum(part => long.Parse(part.Duration));
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = "Unnamed Activity " + activity.ActivityId,
                        ActivityId = activity.ActivityId,
                        PartId = lastPart.PartId,
                        Duration = "0",
                        Time = time,
                        Status = StatusType.Start
                    };
                    _activeTasksList.Add(item);
                }
                else
                {
                    var task = await UserService.Instance.GetTaskById(activity.TaskId);
                    var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
                    var stopwatchTime = StopwatchesService.Instance.GetStopwatchTime(lastPart.PartId);
                    if (stopwatchTime == -1)
                    {
                        await StartupResume(activity);
                    }
                    var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Sum(part => long.Parse(part.Duration));
                    var t = TimeSpan.FromMilliseconds(time);
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = task.Name,
                        Description = task.Description,
                        ActivityId = activity.ActivityId,
                        TaskId = task.TaskId,
                        Duration = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                        Time = time,
                        Status = StatusType.Start
                    };
                    _activeTasksList.Add(item);
                }
            }
        }

        private async Task GetPausedActivities()
        {
            var result2 = await UserService.Instance.GetActivitiesByStatus(StatusType.Pause);
            foreach (var activity in result2)
            {
                if (activity.TaskId == 0)
                {
                    var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Sum(part => long.Parse(part.Duration));
                    var t = TimeSpan.FromMilliseconds(time);
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = "Unnamed Activity " + activity.ActivityId,
                        ActivityId = activity.ActivityId,
                        Duration = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                        Time = time,
                        Status = StatusType.Pause
                    };
                    _activeTasksList.Add(item);
                }
                else
                {
                    var task = await UserService.Instance.GetTaskById(activity.TaskId);
                    var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Sum(part => long.Parse(part.Duration));
                    var t = TimeSpan.FromMilliseconds(time);
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = task.Name,
                        Description = task.Description,
                        ActivityId = activity.ActivityId,
                        TaskId = task.TaskId,
                        Duration = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                        Time = time,
                        Status = StatusType.Pause
                    };
                    _activeTasksList.Add(item);
                }
            }
        }

        private async Task ListInitiate()
        {
            if (_activeTasksList.Count > 0)
            {
                _activeTasksList.Clear();
            }
            await GetStartedActivities();
            await GetPausedActivities();
        }

        private static string ImageChoice(StatusType status)
        {
            return status == StatusType.Start ? "playButton.png" : "pauseButton.png";
        }

        private async void StartTaskButton_OnClicked(object sender, EventArgs e)
        {
            _listTimer.Stop();
            await Navigation.PushModalAsync(new StartTaskPage());
        }

        private async void PlanTaskButton_OnClicked(object sender, EventArgs e)
        {
            _listTimer.Stop();
            await Navigation.PushModalAsync(new PlanTaskPage());
        }

        private async void FastTaskButton_OnClicked(object sender, EventArgs e)
        {

            var activity = new ActivitiesDto
            {
                Status = StatusType.Start,
                UserId = 1,
                GroupId = 1,
                TaskId = 0
            };
            activity.ActivityId = await UserService.Instance.SaveActivity(activity);
            var now = DateTime.Now;
            var part = new PartsOfActivityDto
            {
                ActivityId = activity.ActivityId,
                Start = now.ToString("HH:mm:ss dd/MM/yyyy"),
                Duration = "0"
            };
            part.PartId = await UserService.Instance.SavePartOfActivity(part);
            StopwatchesService.Instance.AddStopwatch(part.PartId);
            var t = TimeSpan.FromMilliseconds(0);
            var item = new MainPageList
            {
                MyImageSource = ImageChoice(activity.Status),
                Name = "Unnamed Activity " + activity.ActivityId,
                ActivityId = activity.ActivityId,
                PartId = part.PartId,
                Duration = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                Time = 0
            };
            _activeTasksList.Add(item);
            UpdateList();
        }

        private async void InitializeCalendarItem_OnClicked(object sender, EventArgs e)
        {
            _listTimer.Stop();
            await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));
        }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
        {
            _listTimer.Stop();
            await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
	    }

	    private async void ActiveTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        _listTimer.Stop();
            ActiveTasks.ItemsSource = null;
            var item = (MainPageList) e.Item;
	        await Navigation.PushModalAsync(new EditTaskPage(item));
	    }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async Task StartupResume(ActivitiesDto start)
        {
            var task = await UserService.Instance.GetTaskById(start.TaskId) ?? new TasksDto
            {
                Name = "Unnamed Activity " + start.ActivityId
            };
            var result = await DisplayAlert("Error", "Masz niezapauzowaną aktywność " + task.Name + ".\n " +
                                                     "Czy była ona aktywna od zamknięcia aplikacji? \n" +
                                                     "Jeżeli wybierzesz nie, czas aktywności może być niewłaściwy \n" +
                                                     "Jeżeli wybierzesz tak, czas końca aktywności będzie czasem zatwierdzenia tego komunikatu",
                "Tak", "Nie");
            if (!result)
            {
                var result4 = await DisplayAlert("Error",
                    "Czy chcesz żeby aktywność była kontynuowana?", "Tak", "Nie");
                if (!result4)
                {
                    start.Status = StatusType.Pause;
                    await UserService.Instance.SaveActivity(start);
                    return;
                }
                var part2 = new PartsOfActivityDto
                {
                    Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                    ActivityId = start.ActivityId,
                    Duration = "0"
                };
                part2.PartId = await UserService.Instance.SavePartOfActivity(part2);
                StopwatchesService.Instance.AddStopwatch(part2.PartId);
                await UserService.Instance.SaveActivity(start);
                return;
            }
            var part = await UserService.Instance.GetLastActivityPart(start.ActivityId);
            part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            var time = (long) (DateTime.Now - DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null))
                .TotalMilliseconds;
            part.Duration = time.ToString();
            await UserService.Instance.SaveActivity(start);
            await UserService.Instance.SavePartOfActivity(part);
            var result3 = await DisplayAlert("Error",
                "Czy chcesz żeby aktywność była kontynuowana?", "Tak", "Nie");
            if (!result3)
            {
                start.Status = StatusType.Pause;
                await UserService.Instance.SaveActivity(start);
                return;
            }
            var part3 = new PartsOfActivityDto
            {
                Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                ActivityId = start.ActivityId,
                Duration = "0"
            };
            part3.PartId = await UserService.Instance.SavePartOfActivity(part3);
            StopwatchesService.Instance.AddStopwatch(part3.PartId);          
        }
    }
}
