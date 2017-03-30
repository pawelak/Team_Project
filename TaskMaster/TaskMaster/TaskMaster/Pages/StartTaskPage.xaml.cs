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
            App.Database.SaveTask(taskon);
            var activity = new Activities {
                taskId = taskon.taskId,
                userId = 1
            };
            App.Database.SaveActivity(activity);
            DateTime now = DateTime.Now;
            var part = new PartsOfActivity
            {
                activityID = activity.activityId,
                start = now
            };
            App.Database.SavePartOfTask(part);
        }
	}
}
