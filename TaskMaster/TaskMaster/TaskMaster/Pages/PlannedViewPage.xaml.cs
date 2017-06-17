using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskMaster.Enums;
using TaskMaster.Interfaces;
using TaskMaster.Lists;
using TaskMaster.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlannedViewPage
	{
        private readonly List<PlannedListItem> _plannedList = new List<PlannedListItem>();
		public PlannedViewPage ()
		{
			InitializeComponent ();
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        await ListInitiateAsync();
	    }

	    private async void MainPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));
	    }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
	    }

	    private async void LogoutItem_OnClicked(object sender, EventArgs e)
	    {
	        var result = await DisplayAlert("Uwaga",
	            "Wszystkie aktywności zostaną automatycznie zakończone. Kontynuować?", "Tak", "Nie");
	        if (!result)
	        {
	            return;
	        }
	        var activities = await UserService.Instance.GetActivitiesByStatus(StatusType.Start);
	        if (activities.Any(activity => activity.TaskId == 0))
	        {
	            await DisplayAlert("Error", "Nie można wylogować gdyż są nienazwane aktywności", "Ok");
	            return;
	        }
	        var activitiesPause = await UserService.Instance.GetActivitiesByStatus(StatusType.Pause);
	        if (activitiesPause.Any(activity => activity.TaskId == 0))
	        {
	            await DisplayAlert("Error", "Nie można wylogować gdyż są nienazwane aktywności", "Ok");
	            return;
	        }
	        foreach (var activity in activities)
	        {
	            var part = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
	            StopwatchesService.Instance.StopStopwatch(part.PartId);
	            activity.Status = StatusType.Stop;
	            part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
	            part.Duration = StopwatchesService.Instance.GetStopwatchTime(part.PartId).ToString();
	            await UserService.Instance.SaveActivity(activity);
	            await UserService.Instance.SavePartOfActivity(part);
	        }
	        foreach (var activity in activitiesPause)
	        {
	            activity.Status = StatusType.Stop;
	            await UserService.Instance.SaveActivity(activity);
	        }
	        await UserService.Instance.LogoutUser();
	        DependencyService.Get<ILogOutService>().LogOut();
        }

	    private async Task ListInitiateAsync()
	    {
	        var activitiesPlannedList = await UserService.Instance.GetActivitiesByStatus(StatusType.Planned);
	        foreach (var activity in activitiesPlannedList)
	        {
	            var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
	            var task = await UserService.Instance.GetTaskById(activity.TaskId);
	            var element = new PlannedListItem
                {
	                ActivityId = activity.ActivityId,
	                Name = task.Name,
	                Description = activity.Comment,
	                Date = lastPart.Start,
	                Image = ImagesService.Instance.SelectImage(task.Typ)
	            };
	            _plannedList.Add(element);
	        }
	        Device.BeginInvokeOnMainThread(() =>
	        {
	            PlannedTasks.ItemsSource = _plannedList;
	        });
        }

	    private async void PlannedTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
            var item = e.Item as PlannedListItem;
	        if (item == null)
	        {
	            return;
	        }
	        var activity = await UserService.Instance.GetActivity(item.ActivityId);
	        var task = await UserService.Instance.GetTaskById(activity.TaskId);
	        if (activity.SyncStatus != SyncStatus.ToUpload)
	        {
	            await SynchronizationService.Instance.DeletePlanned(activity, task);
	        }
	        activity.Status = StatusType.Canceled;
	        await UserService.Instance.SaveActivity(activity);
	        _plannedList.Clear();
	        await ListInitiateAsync();
        }

	    private async void SyncItem_OnClicked(object sender, EventArgs e)
	    {
	        var send = await SynchronizationService.Instance.SendTasks();
	        if (!send)
	        {
	            await DisplayAlert("Error", "Wystąpił problem z synchronizacją", "Ok");
	            return;
	        }
	        await SynchronizationService.Instance.SendActivities();
	        await SynchronizationService.Instance.GetActivities();
	        await SynchronizationService.Instance.SendFavorites();
	        await SynchronizationService.Instance.GetFavorites();
	        await SynchronizationService.Instance.SendPlannedAsync();
	        await SynchronizationService.Instance.GetPlanned();
        }
    }
}