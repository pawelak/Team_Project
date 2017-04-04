using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimePage : ContentPage
	{
        private readonly Stopwatch _stopwatch;
	    private PartsOfActivity _actual;
	    private Activities _activity;
		public TimePage (PartsOfActivity part)
		{
			InitializeComponent ();
		    _stopwatch = App.Stopwatches.ElementAt(part.PartId-1);
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
	        //await Navigation.PushAsync(new ResultPage(_actual));
	    }

	    private void PauseButton_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }

	    private void ResumeButton_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}
