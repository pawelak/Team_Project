using System;
using System.Linq;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FillInformationPage
	{
        private readonly UserService _userService = new UserService();
        private TasksDto _task = new TasksDto();
	    private readonly ActivitiesDto _activity;
	    private long _duration;

        public FillInformationPage (ActivitiesDto item)
		{
		    _activity = item;
		    Init();
            InitializeComponent();
		}

	    private async void Init()
	    {
	        var parts = await _userService.GetPartsOfActivityByActivityId(_activity.ActivityId);
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
	            if (await _userService.GetTask(_task) == null)
	            {
	                var result = await _userService.SaveTask(_task);
	                _activity.TaskId = result;
	                await _userService.SaveActivity(_activity);
	                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	            }
	            else
	            {
	                _task = await _userService.GetTask(_task);
	                _activity.TaskId = _task.TaskId;
	                await _userService.SaveActivity(_activity);
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
	        DisplayAlert("Error", "Nie możesz opuścić tej strony bez wprowadzenia danych", "OK");
	        return true;
	    }
    }
}
