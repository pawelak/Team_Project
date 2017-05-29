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
            AddToFavoritesList();
		}

	    private async void AddToFavoritesList()
	    {
	        var favorites = await UserService.Instance.GetUserFavorites(1);
	        if (favorites == null)
	        {
	            FavoritePicker.IsEnabled = false;
	        }
	        else
	        {
	            foreach (var item in favorites)
	            {
	                var task = await UserService.Instance.GetTaskById(item.TaskId);
	                FavoritePicker.Items.Add(task.Name);
	            }
	        }

	    }

        private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        if (StartTaskName.Text != null)
	        {
	            var newTask = new TasksDto
	            {
	                Name = StartTaskName.Text,
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
	                Guid = Guid.NewGuid().ToString(),
                    TaskId = newTask.TaskId,
	                //UserId = 1,
                    UserId = UserService.Instance.GetLoggedUser().UserId,
	                Status = StatusType.Start,
	                Comment = StartTaskDescription.Text
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

	    private async void FavoritePicker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        var select = FavoritePicker.Items[FavoritePicker.SelectedIndex];
	        var taskDto = new TasksDto
	        {
	            Name = select
	        };
	        var task = await UserService.Instance.GetTask(taskDto);
	        StartTaskName.Text = task.Name;
	    }
    }
}
