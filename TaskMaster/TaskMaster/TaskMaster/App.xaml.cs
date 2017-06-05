using System;
using TaskMaster.Interfaces;
using TaskMaster.Services;
using Xamarin.Forms;

namespace TaskMaster
{
	public partial class App
	{
	    public App ()
		{
            InitializeComponent();
		    MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
            //await SynchronizationService.Instance.GetActivities();
            //await SynchronizationService.Instance.GetFavorites();
            //await SynchronizationService.Instance.GetPlanned();
            var result2 = await UserService.Instance.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await UserService.Instance.GetTaskById(activity.TaskId);
                var part = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
                if (DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy",null) >= DateTime.Now)
                {
                    DependencyService.Get<INotificationService>().LoadNotifications(task.Name, "Naciśnij aby rozpocząć aktywność", part.ActivityId,
                                                                                    DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null));
                }
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
