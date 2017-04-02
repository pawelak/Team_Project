using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanTaskPage : ContentPage
	{
		public PlanTaskPage ()
		{
			InitializeComponent ();
		}

	    private void PlanTaskStartButton_OnClicked(object sender, EventArgs e)
        {
            var task = new Tasks()
            {
                name = ActivityName.Text,
                description = ActivityDecription.Text
            };
            if (App.Database.GetTask(task).Result == null)
                task.taskId = App.Database.SaveTask(task).Result;
            else
                task = App.Database.GetTask(task).Result;
            var activity = new Activities()
            {
                userId = 1,
                taskId = task.taskId,
                groupId = 1
            };
            activity.activityId = App.Database.SaveActivity(activity).Result;
            string start = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToShortDateString();
            string end = PlanTaskStopTime.Time + " " + PlanTaskStopDate.Date.ToShortDateString();
            var part = new PartsOfActivity()
            {
                activityID = activity.activityId,
                start = start,
                stop = end
            };
            App.Database.SavePartOfTask(part);
        }
	}
}
