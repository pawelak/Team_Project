using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Models;
using TaskMaster.Pages;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTaskPage : ContentPage
	{
		public StartTaskPage ()
		{
			InitializeComponent ();
		}

	    private void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
            var taskon = new Tasks()
            {
                name = StartTaskName.Text,
                description = StartTaskDescription.Text
            };
            if (App.Database.GetTask(taskon).Result == null)
	            taskon.taskId = App.Database.SaveTask(taskon).Result;
	        else
	            taskon = App.Database.GetTask(taskon).Result;
	        var activity = new Activities {
                taskId = taskon.taskId,
                userId = 1
            };
            activity.activityId = App.Database.SaveActivity(activity).Result;
            DateTime now = DateTime.Now;
	        string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivity
            {
                activityID = activity.activityId,
                start = date
            };
            App.Database.SavePartOfTask(part);
	        Navigation.PushAsync(new TimePage(part));
	    }
	}
}
