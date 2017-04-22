using System;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTaskPage
	{
        private readonly UserServices _userServices = new UserServices();
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
	            if (await _userServices.GetTask(newTask) == null)
	                newTask.TaskId = await _userServices.SaveTask(newTask);
	            else
	                newTask = _userServices.GetTask(newTask).Result;
	            var newActivity = new ActivitiesDto
	            {
	                TaskId = newTask.TaskId,
	                UserId = 1,
	                Status = StatusType.Start
	            };
	            newActivity.ActivityId = await _userServices.SaveActivity(newActivity);
	            DateTime now = DateTime.Now;
	            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
	            var part = new PartsOfActivityDto
	            {
	                ActivityId = newActivity.ActivityId,
	                Start = date,
                    Duration = "0"
	            };
	            var result = await _userServices.SavePartOfActivity(part);
	            Stopwatch sw = new Stopwatch();
                Stopwatches stopwatch = new Stopwatches(sw,result);
	            App.Stopwatches.Add(stopwatch);
	            App.Stopwatches[App.Stopwatches.Count - 1].Start();
	            await Navigation.PopModalAsync();
	        }
	        else
	            await DisplayAlert("Error", "Nie podałeś nazwy aktywności", "Ok");
	    }
	}
}
