using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Models;
using TaskMaster.Pages;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTaskPage : ContentPage
	{
		public StartTaskPage ()
		{
			InitializeComponent ();
		}

	    private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
            var newTask = new Tasks()
            {
                Name = StartTaskName.Text,
                Description = StartTaskDescription.Text
            };
            if (await App.Database.GetTask(newTask) == null)
	            newTask.TaskId = await App.Database.SaveTask(newTask);
	        else
	            newTask = App.Database.GetTask(newTask).Result;
	        var newActivity = new Activities {
                TaskId = newTask.TaskId,
                UserId = 1
            };
            newActivity.ActivityId = await App.Database.SaveActivity(newActivity);
            DateTime now = DateTime.Now;
	        string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivity
            {
                ActivityID = newActivity.ActivityId,
                Start = date
            };
            await App.Database.SavePartOfTask(part);
	        await Navigation.PushAsync(new TimePage(part));
	    }
	}
}
