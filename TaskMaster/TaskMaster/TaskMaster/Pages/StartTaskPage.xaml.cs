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
	    private string _typSelected;
		public StartTaskPage ()
		{
			InitializeComponent ();
            AddItemsToPicker();
            AddToFavoritesList();
		}

	    private void AddItemsToPicker()
	    {
	        string[] types = {"Sztuka","Programowanie","Sport","Muzyka","Języki","Jedzenie","Rozrywka","Podróż","Przerwa","Inne" };
	        foreach (var type in types)
	        {
	            TypePicker.Items.Add(type);
	        }
        }

        private async void AddToFavoritesList()
	    {
	        var user = UserService.Instance.GetLoggedUser();
	        var favorites = await UserService.Instance.GetUserFavorites(user.UserId);
            if (favorites.Count == 0)
	        {
	            Device.BeginInvokeOnMainThread(() =>
	            {
	                FavoritePicker.IsVisible = false;
	                FavText.IsVisible = false;
	            });
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

	    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
	    {
	        _typSelected = TypePicker.Items[TypePicker.SelectedIndex];
	    }
	  
        private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        if (StartTaskName.Text != null)
	        {
	            var newTask = new TasksDto
	            {
	                Name = StartTaskName.Text,
                    Typ = _typSelected
	            };
	            var task = await UserService.Instance.GetTask(newTask);
                if ( task == null)
	            {
	                newTask.TaskId = await UserService.Instance.SaveTask(newTask);
	            }
	            else
	            {
	                newTask = await UserService.Instance.GetTask(newTask);
	            }
	            var newActivity = new ActivitiesDto
	            {
	                Guid = Guid.NewGuid().ToString(),
                    TaskId = newTask.TaskId,
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
                    Stop = "",
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
