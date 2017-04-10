using TaskMaster.ModelsDto;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultPage
	{
	    private PartsOfActivityDto _activity;
	    private TasksDto _task;
	    private ActivitiesDto _active;
		public ResultPage (PartsOfActivityDto part)
		{
			InitializeComponent ();
		    _activity = part;
		    _active = App.Database.GetActivity(_activity.ActivityId).Result;
            _task = App.Database.GetTaskById(_active.TaskId).Result;
		    DisplayAlert(_task.Name, _task.Description + "\n" + _activity.Start + "\n" + _activity.Stop + "\n" + _activity.Duration, "No", "Ok");
		}
	}
}
