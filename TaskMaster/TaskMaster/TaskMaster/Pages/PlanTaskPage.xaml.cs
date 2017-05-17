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
       private String Typ_Selected= "Inne";
		public PlanTaskPage ()
		{
			InitializeComponent ();
            TypePickerImage.Source = "OK.png";
            AddItemsToPicker();
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
                    Description = ActivityDescription.Text,
                    Typ = Typ_Selected
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
	                TaskId = newTask.TaskId,
	                GroupId = 1,
	                Status = StatusType.Planned
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
            Typ_Selected = TypePicker.Items[TypePicker.SelectedIndex];
            switch (Typ_Selected)
            {
                case "Sztuka":
                    TypePickerImage.Source = "art.png";
                    break;
                case "Inne":
                    TypePickerImage.Source = "OK.png";
                    break;
                case "Programowanie":
                    TypePickerImage.Source = "programming.png";
                    break;
                case "Sport":
                    TypePickerImage.Source = "sport.png";
                    break;
                case "Muzyka":
                    TypePickerImage.Source = "music.png";
                    break;
                case "Języki":
                    TypePickerImage.Source = "language.png";
                    break;
                case "Jedzenie":
                    TypePickerImage.Source = "eat.png";
                    break;
                case "Rozrywka":
                    TypePickerImage.Source = "instrument.png";
                    break;
                case "Podróż":
                    TypePickerImage.Source = "car.png";
                    break;
                case "Przerwa":
                    TypePickerImage.Source = "Cafe.png";
                    break;
                default:
                    TypePickerImage.Source = "OK.png";
                    break;
                  
            }
        }
    }
}
