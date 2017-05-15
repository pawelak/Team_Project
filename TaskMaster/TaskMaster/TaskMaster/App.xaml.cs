using System;
using TaskMaster.Interfaces;
using Xamarin.Forms;

namespace TaskMaster
{
	public partial class App
	{
        private readonly UserService _userService = new UserService();
	    public App ()
		{
            InitializeComponent();
		    MainPage = new NavigationPage(new MainPage());
		}

        protected override async void OnStart()
        {            
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await _userService.GetTaskById(activity.TaskId);
                var part = await _userService.GetLastActivityPart(activity.ActivityId);
                DependencyService.Get<INotificationService>().LoadNotifications(task.Name, "Naciśnij aby rozpocząć aktywność", part.PartId,
                    DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null));
            }
        }

        protected override void OnSleep ()
		{

		}

		protected override void OnResume ()
		{

		}
       
    }
}
