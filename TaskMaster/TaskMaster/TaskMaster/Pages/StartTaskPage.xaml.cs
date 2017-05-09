using System;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTaskPage
	{
        private readonly UserService _userService = new UserService();
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
	            if (await _userService.GetTask(newTask) == null)
	            {
	                newTask.TaskId = await _userService.SaveTask(newTask);
	            }
	            else
	            {
	                newTask = _userService.GetTask(newTask).Result;
	            }
	            var newActivity = new ActivitiesDto
	            {
	                TaskId = newTask.TaskId,
	                UserId = 1,
	                Status = StatusType.Start
	            };
	            newActivity.ActivityId = await _userService.SaveActivity(newActivity);
	            var date = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
	            var part = new PartsOfActivityDto
	            {
	                ActivityId = newActivity.ActivityId,
	                Start = date,
	                Duration = "0"
	            };
	            part.PartId = await _userService.SavePartOfActivity(part);
	            var sw = new Stopwatch();
	            var stopwatch = new Stopwatches(sw, part.PartId);
	            App.Stopwatches.Add(stopwatch);
	            App.Stopwatches[App.Stopwatches.Count - 1].Start();
	            await Navigation.PopModalAsync();
	        }
	        else
	        {
                await DisplayAlert("Error", "Nie podałeś nazwy aktywności", "Ok");
	        }
	    }
	}
}
