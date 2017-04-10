using System;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using TaskMaster.ModelsDto;

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
	            var newTask = new TasksDto()
	            {
	                Name = StartTaskName.Text,
	                Description = StartTaskDescription.Text
	            };
	            /*if (await App.Database.GetTask(newTask) == null)
	                newTask.TaskId = await App.Database.SaveTask(newTask);
	            else
	                newTask = App.Database.GetTask(newTask).Result;*/
	            var newActivity = new ActivitiesDto
	            {
	                TaskId = newTask.TaskId,
	                UserId = 1,
	                Status = StatusType.Start
	            };
	            //newActivity.ActivityId = await App.Database.SaveActivity(newActivity);
	            DateTime now = DateTime.Now;
	            string date = now.ToString("HH:mm:ss dd/MM/yyyy");
	            var part = new PartsOfActivityDto
	            {
	                ActivityId = newActivity.ActivityId,
	                Start = date
	            };
	            //await App.Database.SavePartOfTask(part);
	            Stopwatch sw = new Stopwatch();
	            App.Stopwatches.Add(sw);
	            App.Stopwatches[App.Stopwatches.Count - 1].Start();
	            await Navigation.PopModalAsync();
	        }
	        else
	            await DisplayAlert("Error", "Nie podałeś nazwy aktywności", "Ok");
	    }
	}
}
