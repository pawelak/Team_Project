﻿using System;
using System.Diagnostics;
using System.Linq;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage
    {
        private bool _isPageNotChanged = true;
        private readonly UserService _userService = new UserService();
        private Stopwatch _stopwatch;
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
            _activity = await _userService.GetActivity(item.ActivityId);
            _part = await _userService.GetLastActivityPart(_activity.ActivityId);
            _task = new TasksDto
            {
                Name = item.Name,
                Description = item.Description,
                TaskId = item.TaskId
            };
            TaskDates.Text = _part.Start;
            TaskDate.Text = _part.Start;
            var parts = await _userService.GetPartsOfActivityByActivityId(_activity.ActivityId);
            _duration = parts.Sum(part => long.Parse(part.Duration));
            if (item.Status == StatusType.Start)
            {
                var stopwatch = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == _part.PartId);
                if (stopwatch != null)
                {
                    _stopwatch = stopwatch.GetStopwatch();
                    _duration += _stopwatch.ElapsedMilliseconds;
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
            _stopwatch.Stop();
            _now = DateTime.Now;
            _part.Stop = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Duration = _duration.ToString();
            _activity.Status = StatusType.Stop;
            await _userService.SaveActivity(_activity);
            await _userService.SavePartOfActivity(_part);
            if (_task.TaskId == 0)
            {
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            }
            else
            {
                _task.TaskId = await _userService.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await _userService.SaveActivity(_activity);
                await Navigation.PushModalAsync(new MainPage());
            }
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Pause;
            _now = DateTime.Now;
            var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Stop = date;
            _stopwatch.Stop();
            _part.Duration = _stopwatch.ElapsedMilliseconds.ToString();
            await _userService.SaveActivity(_activity);
            await _userService.SavePartOfActivity(_part);
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
            part.PartId = await _userService.SavePartOfActivity(part);
            var sw = new Stopwatch();
            var stopwatch = new Stopwatches(sw,part.PartId);
            App.Stopwatches.Add(stopwatch);
            App.Stopwatches[App.Stopwatches.Count - 1].Start();
            _part = part;
            _stopwatch = App.Stopwatches[App.Stopwatches.Count - 1].GetStopwatch();
            await _userService.SaveActivity(_activity);
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
                _task.TaskId = await _userService.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await _userService.SaveActivity(_activity);
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
