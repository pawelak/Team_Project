using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Models;

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
	        int newTaskId;
            if (App.Database.GetTask(taskon).Result == null)
	            newTaskId = App.Database.SaveTask(taskon).Result;
	        else
	        {
	            taskon = App.Database.GetTask(taskon).Result;
	            newTaskId = taskon.taskId;
	        }
	        var activity = new Activities {
                taskId = newTaskId,
                userId = 1
            };
            int newActivityId = App.Database.SaveActivity(activity).Result;
            DateTime now = DateTime.Now;
	        string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivity
            {
                activityID = newActivityId,
                start = date
            };
            App.Database.SavePartOfTask(part);
	    }
	}
}
