using System;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FillInformationPage
	{
        private readonly UserServices _userServices = new UserServices();
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
	        var parts = await _userServices.GetPartsOfActivityByActivityId(_activity.ActivityId);
	        foreach (var part in parts)
	        {
	            _duration += long.Parse(part.Duration);
	        }
	        TimeSpan t = TimeSpan.FromMilliseconds(_duration);
	        string answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
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
	            if (await _userServices.GetTask(_task) == null)
	            {
	                var result = await _userServices.SaveTask(_task);
	                _activity.ActivityId = result;
	                await _userServices.SaveActivity(_activity);
	                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	            }
	            else
	            {
	                _task = await _userServices.GetTask(_task);
	                _activity.TaskId = _task.TaskId;
	                await _userServices.SaveActivity(_activity);
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
