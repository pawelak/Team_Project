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

	    private async void PlanTaskStartButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (ActivityName != null)
                {
                    var newTask = new Tasks()
                    {
                        Name = ActivityName.Text,
                        Description = ActivityDescription.Text
                    };
                    if (await App.Database.GetTask(newTask) == null)
                        newTask.TaskId = await App.Database.SaveTask(newTask);
                    else
                        newTask = await App.Database.GetTask(newTask);
                    var newActivity = new Activities()
                    {
                        UserId = 1,
                        TaskId = newTask.TaskId,
                        GroupId = 1
                    };
                    newActivity.ActivityId = await App.Database.SaveActivity(newActivity);
                    var start = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToShortDateString();
                    var end = PlanTaskStopTime.Time + " " + PlanTaskStopDate.Date.ToShortDateString();
                    var part = new PartsOfActivity()
                    {
                        ActivityId = newActivity.ActivityId,
                        Start = start,
                        Stop = end
                    };
                    await App.Database.SavePartOfTask(part);
                }
                //await Navigation.PopAsync();
            }
            catch (SystemException o)
            {
                await DisplayAlert("Test", o.ToString(), "e", "f");
            }

        }
	}
}
