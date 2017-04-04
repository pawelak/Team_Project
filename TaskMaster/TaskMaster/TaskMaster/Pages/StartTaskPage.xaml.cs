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

	    private void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
            var newTask = new Tasks()
            {
                Name = StartTaskName.Text,
                Description = StartTaskDescription.Text
            };
            if (App.Database.GetTask(newTask).Result == null)
	            newTask.TaskId = App.Database.SaveTask(newTask).Result;
	        else
	            newTask = App.Database.GetTask(newTask).Result;
	        var newActivity = new Activities {
                TaskId = newTask.TaskId,
                UserId = 1
            };
            newActivity.ActivityId = App.Database.SaveActivity(newActivity).Result;
            DateTime now = DateTime.Now;
	        string date = now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivity
            {
                ActivityID = newActivity.ActivityId,
                Start = date
            };
            App.Database.SavePartOfTask(part);
	        Navigation.PushAsync(new TimePage(part));
	    }
	}
}
