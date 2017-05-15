using System;
using System.Linq;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Services;

namespace TaskMaster.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage
    {
        private bool _isPageNotChanged = true;
        private PartsOfActivityDto _part;
        private ActivitiesDto _activity;
        private TasksDto _task;
        private DateTime _now;
        private long _duration;
        public EditTaskPage(MainPageList item)
        {
            InitializeComponent();
            Initial(item);
            ActivityName.Text = item.Name;
            ActivityDescription.Text = item.Description;
            TaskName.Text = item.Name;
            TaskDescription.Text = item.Description;
        }

        private async void Initial(MainPageList item)
        {
            _activity = await UserService.Instance.GetActivity(item.ActivityId);
            _part = await UserService.Instance.GetLastActivityPart(_activity.ActivityId);
            _task = new TasksDto
            {
                Name = item.Name,
                Description = item.Description,
                TaskId = item.TaskId
            };
            TaskDates.Text = _part.Start;
            TaskDate.Text = _part.Start;
            var parts = await UserService.Instance.GetPartsOfActivityByActivityId(_activity.ActivityId);
            _duration = parts.Sum(part => long.Parse(part.Duration));
            if (item.Status == StatusType.Start)
            {
                var stopwatchTime = StopwatchesService.Instance.GetStopwatchTime(_part.PartId);
                if (stopwatchTime != -1)
                {
                    _duration += stopwatchTime;
                }
            }
            var t = TimeSpan.FromMilliseconds(_duration);
            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            TaskDuration.Text = answer;
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
            UpdateButtons();
        }

        private bool UpdateTime()
        {
            if (_activity.Status != StatusType.Start)
            {
                return false;
            }
            _duration += 1000;
            var t = TimeSpan.FromMilliseconds(_duration);
            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            TaskDuration.Text = answer;
            return _isPageNotChanged;
        }

        private void UpdateButtons()
        {
            PauseButton.IsEnabled = _activity.Status == StatusType.Start;
            ResumeButton.IsEnabled = _activity.Status == StatusType.Pause;
            StopButton.IsEnabled = _activity.Status != StatusType.Planned;
        }
        private async void StopButton_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            StopwatchesService.Instance.StopStopwatch(_part.PartId);
            _now = DateTime.Now;
            _part.Stop = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Duration = _duration.ToString();
            _activity.Status = StatusType.Stop;
            await UserService.Instance.SaveActivity(_activity);
            await UserService.Instance.SavePartOfActivity(_part);
            if (_task.TaskId == 0)
            {
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            }
            else
            {
                _task.TaskId = await UserService.Instance.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await UserService.Instance.SaveActivity(_activity);
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Pause;
            _now = DateTime.Now;
            var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Stop = date;
            StopwatchesService.Instance.StopStopwatch(_part.PartId);
            _part.Duration = StopwatchesService.Instance.GetStopwatchTime(_part.PartId).ToString();
            await UserService.Instance.SaveActivity(_activity);
            await UserService.Instance.SavePartOfActivity(_part);
            UpdateButtons();
        }

        private async void ResumeButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Start;
            _now = DateTime.Now;
            var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivityDto
            {
                ActivityId = _activity.ActivityId,
                Start = date,
                Duration = "0"
            };
            part.PartId = await UserService.Instance.SavePartOfActivity(part);
            StopwatchesService.Instance.AddStopwatch(part.PartId);
            _part = part;
            await UserService.Instance.SaveActivity(_activity);
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
            UpdateButtons();
        }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskDescription.Text = ActivityDescription.Text;
            _task.Description = ActivityDescription.Text;
        }

        private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskName.Text = ActivityName.Text;
            _task.Name = ActivityName.Text;
        }

        private async void AcceptButton_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            if (_task.TaskId == 0)
            {
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            }
            else
            {
                _task.TaskId = await UserService.Instance.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await UserService.Instance.SaveActivity(_activity);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_task.Name == ActivityName.Text)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _isPageNotChanged = false;
                    await Navigation.PopModalAsync();
                });
                return true;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Error", "Niezapisane dane zostaną utracone. Czy kontynuować",
                    "Tak", "Nie");
                if (!result) return;
                _isPageNotChanged = false;
                await Navigation.PopModalAsync();
            });
            return true;
        }
    }
}
