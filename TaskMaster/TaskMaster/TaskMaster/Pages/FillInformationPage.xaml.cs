using System;
using System.Linq;
using TaskMaster.Enums;
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
	        string[] types = { "Sztuka", "Inne", "Programowanie", "Sport", "Muzyka", "Języki", "Jedzenie", "Rozrywka", "Podróż", "Przerwa", "Inne" };
	        foreach (var type in types)
	        {
	            TypePicker.Items.Add(type);
	        }
        }
	    private async void AddToFavoritesList()
	    {
	        var favorites = await UserService.Instance.GetUserFavorites(1);
	        if (favorites.Count == 0)
	        {
	            FavoritePicker.IsVisible = false;
	            FavText.IsVisible = false;
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
	        var answer = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}";
	        FillTaskDuration.Text = answer;
	    }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
	    {
	        FillTaskDescription.Text = ActivityDescription.Text;
	        _activity.Comment = ActivityDescription.Text;
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
	                _task.TaskId = await UserService.Instance.SaveTask(_task);
	                _activity.TaskId = _task.TaskId;
                    _activity.Status = StatusType.Stop;
	                await UserService.Instance.SaveActivity(_activity);
	                await SynchronizationService.Instance.SendTask(_task);
	                await SynchronizationService.Instance.SendActivity(_activity,_task);
	                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	            }
	            else
	            {
	                _task = await UserService.Instance.GetTask(_task);
	                _activity.TaskId = _task.TaskId;
	                _activity.Status = StatusType.Stop;
                    await UserService.Instance.SaveActivity(_activity);
	                if (_task.SyncStatus == SyncStatus.ToUpload)
	                {
	                    await SynchronizationService.Instance.SendTask(_task);
	                }
	                await SynchronizationService.Instance.SendActivity(_activity, _task);
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
	        TypePickerImage.Source = ImagesService.Instance.SelectImage(typ);
	        _task.Typ = typ;
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
	        TypePickerImage.Source = ImagesService.Instance.SelectImage(_task.Typ);
	    }
    }
}
