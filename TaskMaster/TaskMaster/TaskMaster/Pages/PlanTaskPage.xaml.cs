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
            var newTask = new Tasks()
            {
                Name = ActivityName.Text,
                Description = ActivityDecription.Text
            };
            if (App.Database.GetTask(newTask).Result == null)
                newTask.TaskId = App.Database.SaveTask(newTask).Result;
            else
                newTask = App.Database.GetTask(newTask).Result;
            var newActivity = new Activities()
            {
                UserId = 1,
                TaskId = newTask.TaskId,
                GroupId = 1
            };
            newActivity.ActivityId = App.Database.SaveActivity(newActivity).Result;
            string start = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToShortDateString();
            string end = PlanTaskStopTime.Time + " " + PlanTaskStopDate.Date.ToShortDateString();
            var part = new PartsOfActivity()
            {
                ActivityID = newActivity.ActivityId,
                Start = start,
                Stop = end
            };
            App.Database.SavePartOfTask(part);
            Navigation.PopAsync();
        }
	}
}
