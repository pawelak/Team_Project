using System;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanTaskPage
	{
        private readonly UserServices _userServices = new UserServices();
		public PlanTaskPage ()
		{
			InitializeComponent ();
		}

	    private async void PlanTaskStartButton_OnClicked(object sender, EventArgs e)
	    {
	        if (ActivityName != null)
	        {
	            var newTask = new TasksDto()
	            {
	                Name = ActivityName.Text,
	                Description = ActivityDescription.Text
	            };
	            if (await _userServices.GetTask(newTask) == null)
	                newTask.TaskId = await _userServices.SaveTask(newTask);
	            else
	                newTask = await _userServices.GetTask(newTask);
	            var newActivity = new ActivitiesDto()
	            {
	                UserId = 1,
	                TaskId = newTask.TaskId,
	                GroupId = 1,
	                Status = StatusType.Planned
	            };
	            newActivity.ActivityId = await _userServices.SaveActivity(newActivity);
	            var start = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToShortDateString();
	            var end = PlanTaskStopTime.Time + " " + PlanTaskStopDate.Date.ToShortDateString();
	            var part = new PartsOfActivityDto()
	            {
	                ActivityId = newActivity.ActivityId,
	                Start = start,
	                Stop = end,
	                Duration = "0"
	            };
	            await _userServices.SavePartOfActivity(part);
	            await Navigation.PopAsync();
	        }
	        else
	            await DisplayAlert("Error", "Nie podałeś nazwy aktywności", "Ok");

	    }

	    private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskName.Text = ActivityName.Text;
        }

	    private void PlanTaskStartDate_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        
	        TaskDate.Text = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToShortDateString();
        }

	    private void PlanTaskStartTime_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskDate.Text = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToShortDateString();
        }

	    private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskDescription.Text = ActivityDescription.Text;
        }
	}
}
