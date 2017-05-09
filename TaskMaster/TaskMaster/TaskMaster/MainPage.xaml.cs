using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskMaster.ModelsDto;
using TaskMaster.Pages;
using Xamarin.Forms;
namespace TaskMaster
{
    public partial class MainPage
    {
        public bool IsFirstOpen;
        private bool _isPageNotChanged = true;
        private bool _isVisible = true;
        private readonly List<MainPageList> _activeTasksList = new List<MainPageList>();
        private readonly UserService _userService = new UserService();

        public MainPage()
        {
            InitializeComponent();
            IsFirstOpen = false;
        }

        public MainPage(bool a)
        {
            InitializeComponent();
            IsFirstOpen = a;
            StartupResume();
        }

        protected override void OnAppearing()
        {
            _isPageNotChanged = true;
            Device.StartTimer(TimeSpan.FromSeconds(1),CheckIsFirstOpen);
        }

        private bool CheckIsFirstOpen()
        {
            if (IsFirstOpen)
            {
                return true;
            }
            ListInitiate();
            Device.StartTimer(TimeSpan.FromSeconds(1), CheckList);
            return false;
        }

        private bool CheckList()
        {
            if (_activeTasksList.Count <= 0)
            {
                return _isVisible;
            }
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
            return false;
        }

        private bool UpdateTime()
        {
            if (_activeTasksList.Count <= 0)
            {
                return false;
            }
            foreach (var item in _activeTasksList)
            {
                if (item.Status != StatusType.Start)
                {
                    continue;
                }
                item.Time += 1000;
                var t = TimeSpan.FromMilliseconds(item.Time);
                var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
                item.Duration = answer;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await UpdateList();
            });
            return _isPageNotChanged;
        }

        private async Task<bool> UpdateList()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ActiveTasks.ItemsSource = null;
                    ActiveTasks.ItemsSource = _activeTasksList;
                });
            });
            return true;
        }

        private async void GetStartedActivities()
        {
            var activitiesStarted = await _userService.GetActivitiesByStatus(StatusType.Start);
            foreach (var activity in activitiesStarted)
            {
                if (activity.TaskId == 0)
                {
                    var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
                    var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
                    var time = long.Parse(lastPart.Duration);
                    if (stopwatch != null)
                    {
                        time += stopwatch.GetStopwatch().ElapsedMilliseconds;
                    }
                    else
                    {
                        var sw = new Stopwatch();
                        var stopwatch2 = new Stopwatches(sw, lastPart.PartId);
                        App.Stopwatches.Add(stopwatch2);
                        App.Stopwatches[App.Stopwatches.Count - 1].Start();
                    }
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = "Unnamed Activity " + activity.ActivityId,
                        ActivityId = activity.ActivityId,
                        Duration = "0",
                        Time = time,
                        Status = StatusType.Start
                    };
                    _activeTasksList.Add(item);
                }
                else
                {
                    var task = await _userService.GetTaskById(activity.TaskId);
                    var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Sum(part => long.Parse(part.Duration));
                    var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
                    var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
                    if (stopwatch != null)
                    {
                        time += stopwatch.GetStopwatch().ElapsedMilliseconds;
                    }
                    else
                    {
                        var sw = new Stopwatch();
                        var stopwatch2 = new Stopwatches(sw, lastPart.PartId);
                        App.Stopwatches.Add(stopwatch2);
                        App.Stopwatches[App.Stopwatches.Count - 1].Start();
                    }
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

        private async void GetPausedActivities()
        {
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Pause);
            foreach (var activity in result2)
            {
                if (activity.TaskId == 0)
                {
                    var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
                    var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
                    var time = long.Parse(lastPart.Duration);
                    if (stopwatch != null)
                    {
                        time += stopwatch.GetStopwatch().ElapsedMilliseconds;
                    }
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
                    var task = await _userService.GetTaskById(activity.TaskId);
                    var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Sum(part => long.Parse(part.Duration));
                    var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
                    var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
                    if (stopwatch != null)
                    {
                        time += stopwatch.GetStopwatch().ElapsedMilliseconds;
                    }
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
        private void ListInitiate()
        {
            if (_activeTasksList.Count > 0)
            {
                _activeTasksList.Clear();
            }
            GetStartedActivities();
            GetPausedActivities();
            ActiveTasks.ItemsSource = _activeTasksList;
        }

        private static string ImageChoice(StatusType status)
        {
            return status == StatusType.Start ? "playButton.png" : "pauseButton.png";
        }

        private async void StartTaskButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void PlanTaskButton_OnClicked(object sender, EventArgs e)
        {
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
            activity.ActivityId = await _userService.SaveActivity(activity);
            var now = DateTime.Now;
            var part = new PartsOfActivityDto
            {
                ActivityId = activity.ActivityId,
                Start = now.ToString("HH:mm:ss dd/MM/yyyy"),
                Duration = "0"
            };
            var result = await _userService.SavePartOfActivity(part);
            var sw = new Stopwatch();
            var stopwatch = new Stopwatches(sw, result);
            App.Stopwatches.Add(stopwatch);
            App.Stopwatches[App.Stopwatches.Count - 1].Start();
            var t = TimeSpan.FromMilliseconds(0);
            var item = new MainPageList
            {
                MyImageSource = ImageChoice(activity.Status),
                Name = "Unnamed Activity " + activity.ActivityId,
                ActivityId = activity.ActivityId,
                Duration = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                Time = 0
            };
            _activeTasksList.Add(item);
            await UpdateList();
        }
          
        private async void InitializeCalendarItem_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            _isVisible = false;
            await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));
        }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            _isVisible = false;
            await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
	    }

	    private async void ActiveTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        _isPageNotChanged = false;
	        _isVisible = false;
            var item = (MainPageList) e.Item;
	        await Navigation.PushModalAsync(new EditTaskPage(item));
	    }

        protected override bool OnBackButtonPressed()
        {
            _isPageNotChanged = false;
            _isVisible = false;
            return base.OnBackButtonPressed();
        }

        private async void StartupResume()
        {
            var started = await _userService.GetActivitiesByStatus(StatusType.Start);
            foreach (var start in started)
            {
                var task = await _userService.GetTaskById(start.TaskId);
                if (await _userService.GetTaskById(start.TaskId) == null)
                {
                    task = new TasksDto
                    {
                        Name = "Unnamed Activity " + start.ActivityId
                    };
                }
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
                        await _userService.SaveActivity(start);
                        continue;
                    }
                    var part2 = new PartsOfActivityDto
                    {
                        Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                        ActivityId = start.ActivityId,
                        Duration = "0"
                    };
                    part2.PartId = await _userService.SavePartOfActivity(part2);
                    var sw = new Stopwatch();
                    var stopwatch = new Stopwatches(sw, part2.PartId);
                    App.Stopwatches.Add(stopwatch);
                    App.Stopwatches[App.Stopwatches.Count - 1].Start();
                    await _userService.SaveActivity(start);
                    continue;
                }
                var part = await _userService.GetLastActivityPart(start.ActivityId);
                part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                var time = (long)(DateTime.Now - DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null)).TotalMilliseconds;
                part.Duration = time.ToString();
                await _userService.SaveActivity(start);
                await _userService.SavePartOfActivity(part);
                var result3 = await DisplayAlert("Error",
                    "Czy chcesz żeby aktywność była kontynuowana?", "Tak", "Nie");
                if (!result3)
                {
                    start.Status = StatusType.Pause;
                    await _userService.SaveActivity(start);
                    continue;
                }
                var part3 = new PartsOfActivityDto
                {
                    Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                    ActivityId = start.ActivityId,
                    Duration = "0"
                };
                part3.PartId = await _userService.SavePartOfActivity(part3);
                var sw2 = new Stopwatch();
                var stopwatch2 = new Stopwatches(sw2, part3.PartId);
                App.Stopwatches.Add(stopwatch2);
                App.Stopwatches[App.Stopwatches.Count - 1].Start();
            }
            IsFirstOpen = false;
        }
    }
}
