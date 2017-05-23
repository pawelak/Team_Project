using System;
using TaskMaster.Interfaces;
using TaskMaster.ModelsDto;
using TaskMaster.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanTaskPage
	{
       private string _typSelected= "Inne";
		public PlanTaskPage ()
		{
			InitializeComponent ();
            TypePickerImage.Source = "OK.png";
            AddItemsToPicker();
            AddToFavoritesList();
		}

        private void AddItemsToPicker()
        {
            TypePicker.Items.Add("Sztuka");
            TypePicker.Items.Add("Inne");
            TypePicker.Items.Add("Programowanie");
            TypePicker.Items.Add("Sport");
            TypePicker.Items.Add("Muzyka");
            TypePicker.Items.Add("Języki");
            TypePicker.Items.Add("Jedzenie");
            TypePicker.Items.Add("Rozrywka");
            TypePicker.Items.Add("Podróż");
            TypePicker.Items.Add("Przerwa");
        }

	    private async void AddToFavoritesList()
	    {
	        var favorites = await UserService.Instance.GetUserFavorites(1);
	        if (favorites == null)
	        {
	            FavoritePicker.IsVisible = false;
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

        private async void PlanTaskStartButton_OnClicked(object sender, EventArgs e)
	    {
	        if (ActivityName != null)
	        {
	            var start = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToString("dd/MM/yyyy");
	            var time = DateTime.ParseExact(start, "HH:mm:ss dd/MM/yyyy", null);
	            if (time < DateTime.Now)
	            {
	                await DisplayAlert("Error", "Wprowadzona data jest wcześniejsza niż obecna", "Ok");
	                return;
	            }
                var newTask = new TasksDto
                {
                    Name = ActivityName.Text,
                    Typ = _typSelected
                };
	            if (await UserService.Instance.GetTask(newTask) == null)
	            {
	                newTask.TaskId = await UserService.Instance.SaveTask(newTask);
	            }
	            else
	            {
	                newTask = await UserService.Instance.GetTask(newTask);
	            }
	            var newActivity = new ActivitiesDto
	            {
	                UserId = 1,
	                //UserId = await UserService.Instance.GetLoggedUser(),
                    TaskId = newTask.TaskId,
	                GroupId = 1,
	                Status = StatusType.Planned,
	                Comment = ActivityDescription.Text,
                };
	            newActivity.ActivityId = await UserService.Instance.SaveActivity(newActivity);
	            var part = new PartsOfActivityDto
	            {
	                ActivityId = newActivity.ActivityId,
	                Start = start,
	                Duration = "0"
	            };
	            part.PartId = await UserService.Instance.SavePartOfActivity(part);
	            DependencyService.Get<INotificationService>().LoadNotifications(newTask.Name, "Naciśnij aby rozpocząć aktywność", part.ActivityId, 
	                DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null));
	            //await SynchronizationService.Instance.SendPlanned(newActivity);
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	        }
	        else
	        {
	            await DisplayAlert("Error", "Nie podałeś nazwy aktywności", "Ok");
	        }

	    }

	    private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskName.Text = ActivityName.Text;
        }

	    private void PlanTaskStartDate_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskDate.Text = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToString("dd/MM/yyyy");
        }

	    private void PlanTaskStartTime_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskDate.Text = PlanTaskStartTime.Time + " " + PlanTaskStartDate.Date.ToString("dd/MM/yyyy");
        }

	    private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        TaskDescription.Text = ActivityDescription.Text;
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _typSelected = TypePicker.Items[TypePicker.SelectedIndex];
            TypePickerImage.Source = SelectImage(_typSelected);
        }

	    private static string SelectImage(string item)
	    {
	        string type;
	        switch (item)
	        {
	            case "Sztuka":
	                type = "art.png";
	                break;
	            case "Inne":
	                type = "OK.png";
	                break;
	            case "Programowanie":
	                type = "programming.png";
	                break;
	            case "Sport":
	                type = "sport.png";
	                break;
	            case "Muzyka":
	                type = "music.png";
	                break;
	            case "Języki":
	                type = "language.png";
	                break;
	            case "Jedzenie":
	                type = "eat.png";
	                break;
	            case "Rozrywka":
	                type = "instrument.png";
	                break;
	            case "Podróż":
	                type = "car.png";
	                break;
	            case "Przerwa":
	                type = "Cafe.png";
	                break;
	            default:
	                type = "OK.png";
	                break;
	        }
	        return type;
	    }
	    private async void FavoritePicker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        var select = FavoritePicker.Items[FavoritePicker.SelectedIndex];
            var taskDto = new TasksDto
            {
                Name = select
            };
	        var task = await UserService.Instance.GetTask(taskDto);
	        TaskName.Text = task.Name;
	        TypePickerImage.Source = SelectImage(task.Typ);
	    }
	}
}
