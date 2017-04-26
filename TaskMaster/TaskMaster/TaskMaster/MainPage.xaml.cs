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
        private bool _isPageNotChanged = true;
        private bool _isVisible = true;
        private readonly List<MainPageList> _activeTasksList = new List<MainPageList>();
        private readonly UserService _userService = new UserService();

        public MainPage()
        {
            InitializeComponent();
            ListInitiate();
            Device.StartTimer(TimeSpan.FromSeconds(1), CheckList);
        }

        protected override void OnAppearing()
        {
            ListInitiate();
            _isPageNotChanged = true;
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

        private async void ListInitiate()
        {
            if (_activeTasksList.Count > 0)
            {
                _activeTasksList.Clear();
            }
            var activitiesStarted = await _userService.GetActivitiesByStatus(StatusType.Start);
            foreach (var activity in activitiesStarted)
            {
                if (activity.TaskId == 0)
                {
                    long time = 0;
                    var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
                    var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
                    if (stopwatch != null)
                    {
                        time = stopwatch.GetStopwatch().ElapsedMilliseconds;
                    }
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = "Unnamed Activity " + activity.ActivityId,
                        ActivityId = activity.ActivityId,
                        Duration = "0",
                        Time = time
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
                        Time = time
                    };
                    _activeTasksList.Add(item);
                }
            }
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Pause);
            foreach (var activity in result2)
            {
                if (activity.TaskId == 0)
                {
                    long time = 0;
                    var lastPart = await _userService.GetLastActivityPart(activity.ActivityId);
                    var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == lastPart.PartId);
                    if (stopwatch != null)
                    {
                        time = stopwatch.GetStopwatch().ElapsedMilliseconds;
                    }
                    var t = TimeSpan.FromMilliseconds(time);
                    var item = new MainPageList
                    {
                        MyImageSource = ImageChoice(activity.Status),
                        Name = "Unnamed Activity " + activity.ActivityId,
                        ActivityId = activity.ActivityId,
                        Duration = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s",
                        Time = time
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
                        Time = time
                    };
                    _activeTasksList.Add(item);
                }
            }
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
            await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));
        }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
	    }

	    private async void ActiveTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        _isPageNotChanged = false;
            var item = (MainPageList) e.Item;
	        await Navigation.PushModalAsync(new EditTaskPage(item));
	    }

        protected override bool OnBackButtonPressed()
        {
            _isPageNotChanged = false;
            _isVisible = false;
            return base.OnBackButtonPressed();
        }
    }
}
