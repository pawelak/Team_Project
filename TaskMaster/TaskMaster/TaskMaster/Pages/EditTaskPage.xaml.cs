using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            TaskDates.Text = _actual.Start;
            /*_stopwatch = App.Stopwatches.ElementAt(_actual.PartId - 1);
            TimeSpan t = TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);*/
        }
        private async void StopButton_OnClicked(object sender, EventArgs e)
        {
            _stopwatch.Stop();
            await DisplayAlert("Tytul", _stopwatch.ElapsedMilliseconds.ToString(), "E", "F");
            long mili = _stopwatch.ElapsedMilliseconds;
            DateTime now = DateTime.Now;
            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            _actual.Stop = date;
            _actual.Duration = mili.ToString();
            _activity.Status = StatusType.Stop;
            await App.Database.SaveActivity(_activity);
            await App.Database.SavePartOfTask(_actual);
            await Navigation.PushModalAsync(new MainPage());
        }

        private void PauseButton_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ResumeButton_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskDescription.Text = ActivityDescription.Text;
        }

        private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskName.Text = ActivityName.Text;
        }

        private void AcceptButton_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
