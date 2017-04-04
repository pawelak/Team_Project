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
        private readonly Stopwatch _stopwatch = new Stopwatch();
	    private PartsOfActivity _actual; 
		public TimePage (PartsOfActivity part)
		{
			InitializeComponent ();
            _stopwatch.Start();
		    _actual = part;
		}

	    private void StopButton_OnClicked(object sender, EventArgs e)
	    {
	        _stopwatch.Stop();
	        long mili = _stopwatch.ElapsedMilliseconds;
            DateTime now = DateTime.Now;
            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
	        _actual.Stop = date;
	        _actual.Duration = mili.ToString();
            Navigation.PushAsync(new ResultPage(_actual));
        }
	}
}
