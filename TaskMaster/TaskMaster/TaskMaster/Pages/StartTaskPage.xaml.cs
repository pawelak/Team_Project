using System;
using System.Collections.Generic;
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
            int newTaskId = App.Database.SaveTask(taskon).Result;
            var activity = new Activities {
                taskId = newTaskId,
                userId = 1
            };
            int newActivityId = App.Database.SaveActivity(activity).Result;
            DateTime now = DateTime.Now;
            var part = new PartsOfActivity
            {
                activityID = newActivityId,
                start = now
            };
            App.Database.SavePartOfTask(part);
        }
	}
}
