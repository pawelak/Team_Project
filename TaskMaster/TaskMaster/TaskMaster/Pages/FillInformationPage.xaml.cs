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
    }
}
