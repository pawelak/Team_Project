using System;
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
        private readonly UserServices _userServices = new UserServices();
        private Stopwatch _stopwatch;
        private PartsOfActivityDto _actual;
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
            _activity = await _userServices.GetActivity(item.ActivityId);
            _actual = await _userServices.GetLastActivityPart(_activity.ActivityId);
            _task = new TasksDto
            {
                Name = item.Name,
                Description = item.Description,
                TaskId = item.TaskId
            };
            TaskDates.Text = _actual.Start;
            var parts = await _userServices.GetPartsOfActivityByActivityId(_activity.ActivityId);
            foreach (var part in parts)
            {
                _duration +=long.Parse(part.Duration);
            }
            var firstOrDefault = App.Stopwatches.FirstOrDefault(s => s.GetPartId() == _actual.PartId);
            if (firstOrDefault != null)
                _stopwatch = firstOrDefault.GetStopwatch();
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
            UpdateButtons();
        }

        private bool UpdateTime()
        {
            if (_activity.Status == StatusType.Stop || _activity.Status == StatusType.Planned)
                return false;
            TimeSpan t = TimeSpan.FromMilliseconds(_duration + _stopwatch.ElapsedMilliseconds);
            string answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            TaskDuration.Text = answer;
            return true;
        }
        private void UpdateButtons()
        {
            PauseButton.IsEnabled = _activity.Status == StatusType.Start;
            ResumeButton.IsEnabled = _activity.Status == StatusType.Pause;
            StopButton.IsEnabled = _activity.Status != StatusType.Planned;
        }
        private async void StopButton_OnClicked(object sender, EventArgs e)
        {
            _stopwatch.Stop();
            _now = DateTime.Now;
            string date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _actual.Stop = date;
            _actual.Duration = _stopwatch.ElapsedMilliseconds.ToString();
            _activity.Status = StatusType.Stop;
            await _userServices.SaveActivity(_activity);
            await _userServices.SavePartOfActivity(_actual);
            if (_task.TaskId == 0)
            {
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            }
            else
            {
                _task.TaskId = await _userServices.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await _userServices.SaveActivity(_activity);
                await Navigation.PushModalAsync(new MainPage());
            }
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Pause;
            _now = DateTime.Now;
            string date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _actual.Stop = date;
            _stopwatch.Stop();
            _actual.Duration = _stopwatch.ElapsedMilliseconds.ToString();
            await _userServices.SaveActivity(_activity);
            await _userServices.SavePartOfActivity(_actual);
            UpdateButtons();
        }

        private async void ResumeButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Start;
            _now = DateTime.Now;
            string date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivityDto
            {
                ActivityId = _activity.ActivityId,
                Start = date,
                Duration = "0"
            };
            var result = await _userServices.SavePartOfActivity(part);
            Stopwatch sw = new Stopwatch();
            Stopwatches stopwatch = new Stopwatches(sw,result);
            App.Stopwatches.Add(stopwatch);
            App.Stopwatches[App.Stopwatches.Count - 1].Start();
            _actual = part;
            await _userServices.SaveActivity(_activity);
            UpdateButtons();
        }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskDescription.Text = ActivityDescription.Text;
        }

        private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskName.Text = ActivityName.Text;
        }

        private async void AcceptButton_OnClicked(object sender, EventArgs e)
        {
            if (_task.TaskId == 0)
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            else
            {
                _task.Description = ActivityDescription.Text;
                _task.Name = ActivityName.Text;
                _task.TaskId = await _userServices.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await _userServices.SaveActivity(_activity);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_task.Name == ActivityName.Text)
                return false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Error", "Niezapisane dane zostaną utracone. Czy kontynuować",
                    "Tak", "Nie");
                if (result) await Navigation.PopModalAsync();
            });
            return true;
        }
    }
}
