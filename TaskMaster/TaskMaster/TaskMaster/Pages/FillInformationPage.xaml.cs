using System;
using System.Linq;
using TaskMaster.ModelsDto;
using TaskMaster.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FillInformationPage
	{
        private TasksDto _task = new TasksDto();
	    private readonly ActivitiesDto _activity;
	    private long _duration;

        public FillInformationPage (ActivitiesDto item)
		{
		    _activity = item;
            InitializeComponent();
		    Init();
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

        private async void Init()
	    {
	        var parts = await UserService.Instance.GetPartsOfActivityByActivityId(_activity.ActivityId);
            _duration = parts.Sum(part => long.Parse(part.Duration));
	        var t = TimeSpan.FromMilliseconds(_duration);
	        var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
	        FillTaskDuration.Text = answer;
	    }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        FillTaskDescription.Text = ActivityDescription.Text;
	        _task.Description = ActivityDescription.Text;
	    }

	    private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        FillTaskName.Text = ActivityName.Text;
	        _task.Name = ActivityName.Text;
	    }

	    private async void AcceptButton_OnClicked(object sender, EventArgs e)
	    {
	        if (_task.Name != null)
	        {
	            if (await UserService.Instance.GetTask(_task) == null)
	            {
	                var result = await UserService.Instance.SaveTask(_task);
	                _activity.TaskId = result;
                    _activity.Status = StatusType.Stop;
	                await UserService.Instance.SaveActivity(_activity);
	                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	            }
	            else
	            {
	                _task = await UserService.Instance.GetTask(_task);
	                _activity.TaskId = _task.TaskId;
	                _activity.Status = StatusType.Stop;
                    await UserService.Instance.SaveActivity(_activity);
	                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	            }
            }
	        else
	        {
	            await DisplayAlert("Error", "Nie wprowadziłeś danych", "Ok");
	        }
	    }

	    protected override bool OnBackButtonPressed()
	    {
	        DisplayAlert("Error", "Nie możesz opuścić tej strony bez wprowadzenia danych", "Ok");
	        return true;
	    }

	    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
	    {
	        var typ = TypePicker.Items[TypePicker.SelectedIndex];
	        TypePickerImage.Source = SelectImage(typ);
	        _task.Typ = typ;
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
	        _task = await UserService.Instance.GetTask(taskDto);
	        FillTaskName.Text = _task.Name;
	        FillTaskDescription.Text = _task.Description;
	        TypePickerImage.Source = SelectImage(_task.Typ);
	    }
    }
}
