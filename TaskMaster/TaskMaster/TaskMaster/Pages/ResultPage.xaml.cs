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
		    _active = App.Database.GetActivity(_activity.activityID).Result;
            _task = App.Database.GetTaskById(_active.taskId).Result;
		    DisplayAlert(_task.name, _task.description + "\n" + _activity.start + "\n" + _activity.stop + "\n" + _activity.duration, "No", "Ok");
		}
	}
}
