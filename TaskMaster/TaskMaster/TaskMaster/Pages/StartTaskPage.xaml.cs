using System;
using Xamarin.Forms.Xaml;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using TaskMaster.Services;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTaskPage
	{
		public StartTaskPage ()
		{
			InitializeComponent ();
		}

	    private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        if (StartTaskName.Text != null)
	        {
	            var newTask = new TasksDto
	            {
	                Name = StartTaskName.Text,
	                Description = StartTaskDescription.Text
	            };
	            if (await UserService.Instance.GetTask(newTask) == null)
	            {
	                newTask.TaskId = await UserService.Instance.SaveTask(newTask);
	            }
	            else
	            {
	                newTask = UserService.Instance.GetTask(newTask).Result;
	            }
	            var newActivity = new ActivitiesDto
	            {
	                TaskId = newTask.TaskId,
	                UserId = 1,
	                Status = StatusType.Start
	            };
	            newActivity.ActivityId = await UserService.Instance.SaveActivity(newActivity);
	            var date = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
	            var part = new PartsOfActivityDto
	            {
	                ActivityId = newActivity.ActivityId,
	                Start = date,
	                Duration = "0"
	            };
	            part.PartId = await UserService.Instance.SavePartOfActivity(part);
	            StopwatchesService.Instance.AddStopwatch(part.PartId);
	            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	        }
	        else
	        {
                await DisplayAlert("Error", "Nie podałeś nazwy aktywności", "Ok");
	        }
	    }
	}
}
