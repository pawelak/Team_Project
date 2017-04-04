using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultPage : ContentPage
	{
	    private PartsOfActivity _activity;
	    private Tasks _task;
	    private Activities _active;
		public ResultPage (PartsOfActivity part)
		{
			InitializeComponent ();
		    _activity = part;
		    _active = App.Database.GetActivity(_activity.ActivityID).Result;
            _task = App.Database.GetTaskById(_active.TaskId).Result;
		    DisplayAlert(_task.Name, _task.Description + "\n" + _activity.Start + "\n" + _activity.Stop + "\n" + _activity.Duration, "No", "Ok");
		}
	}
}
