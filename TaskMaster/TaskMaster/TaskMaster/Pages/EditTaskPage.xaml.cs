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
        private readonly Stopwatch _stopwatch;
        private PartsOfActivity _actual;
        private Activities _activity;
        public EditTaskPage(PartsOfActivity part)
        {
            InitializeComponent();
            _stopwatch = App.Stopwatches.ElementAt(part.PartId - 1);
            TimeSpan t = TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
            _actual = part;
        }

        private async void StopButton_OnClicked(object sender, EventArgs e)
        {
            _activity = await App.Database.GetActivity(_actual.ActivityId);
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
            throw new NotImplementedException();
        }

        private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
