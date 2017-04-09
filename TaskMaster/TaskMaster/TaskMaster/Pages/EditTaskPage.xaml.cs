using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage : ContentPage
    {
        private Stopwatch _stopwatch;
        private PartsOfActivity _actual;
        private Activities _activity;
        private Tasks _task;
        private DateTime _now = new DateTime();
        public EditTaskPage(ElemList item)
        {
            InitializeComponent();
            Initial(item);
            ActivityName.Text = item.Name;
            ActivityDescription.Text = item.Description;
            TaskName.Text = item.Name;
            TaskDescription.Text = item.Description;
        }

        private async void Initial(ElemList item)
        {
            _activity = await App.Database.GetActivity(item.ActivityId);
            _actual = await App.Database.GetLastActivityPart(_activity.ActivityId);
            _task = new Tasks()
            {
                Name = item.Name,
                Description = item.Description,
                TaskId = item.TaskId
            };
            TaskDates.Text = _actual.Start;
            _stopwatch = App.Stopwatches.ElementAt(_actual.PartId - 1);
            UpdateButtons();
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
            await App.Database.SaveActivity(_activity);
            await App.Database.SavePartOfTask(_actual);
            await Navigation.PushModalAsync(new MainPage());
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Pause;
            _now = DateTime.Now;
            string date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _actual.Stop = date;
            _stopwatch.Stop();
            _actual.Duration = _stopwatch.ElapsedMilliseconds.ToString(); 
            await App.Database.SaveActivity(_activity);
            await App.Database.SavePartOfTask(_actual);
            UpdateButtons();
        }

        private async void ResumeButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Start;
            _now = DateTime.Now;
            string date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivity()
            {
                ActivityId = _activity.ActivityId,
                Start = date
            };
            await App.Database.SavePartOfTask(part);
            Stopwatch sw = new Stopwatch();
            App.Stopwatches.Add(sw);
            App.Stopwatches[App.Stopwatches.Count - 1].Start();
            _actual = part;
            await App.Database.SaveActivity(_activity);
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
            if (_task.TaskId == 0)
            {
                if (await App.Database.GetTask(_task) == null)
                {
                    var result = await App.Database.SaveTask(_task);
                    _activity.TaskId = result;
                    await App.Database.SaveActivity(_activity);
                }

            }
        }
    }
}
