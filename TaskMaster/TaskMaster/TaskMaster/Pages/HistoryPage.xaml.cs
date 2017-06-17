using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskMaster.Interfaces;
using TaskMaster.Pages;
using TaskMaster.Services;
using Xamarin.Forms;

namespace TaskMaster
{
	
	public partial class HistoryPage
	{
        private readonly List<HistoryListItem> _historyList = new List<HistoryListItem>();
		public HistoryPage ()
		{
			InitializeComponent();
        }

	    private async void MainPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
            await Navigation.PushModalAsync(new NavigationPage(new InitializeCalendar()));
        }

	    private async void PlannedPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NavigationPage(new PlannedViewPage()));
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

        protected override async void OnAppearing()
        {
            await ListInitiateAsync();
        }

	    private async Task ListInitiateAsync()
	    {
	        var activitiesStoppedList = await UserService.Instance.GetActivitiesByStatus(StatusType.Stop);
	        foreach (var activity in activitiesStoppedList)
	        {
	            if (activity.TaskId == 0)
	            {
	                var choose = await DisplayAlert("Error", "Masz nienazwaną zakończoną aktywność " +
	                                                         activity.ActivityId + ".\n" +
	                                                         "Czy chcesz uzupełnić jej dane?\n" +
	                                                         "Aktywność z nieuzupełnionymi danymi nie zostanie wyświetlona", "Tak", "Nie");
	                if (choose)
	                {
	                    await Navigation.PushModalAsync(new FillInformationPage(activity));
	                    return;
	                }
                    continue;
	            }
	            var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
	            var time = parts.Sum(part => long.Parse(part.Duration));
	            var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
	            var task = await UserService.Instance.GetTaskById(activity.TaskId);
	            var t = TimeSpan.FromMilliseconds(time);
	            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
                var element = new HistoryListItem
                {
                    Name = task.Name,
                    Description = activity.Comment,
                    Time = answer,
                    Date = lastPart.Start,
                    Image = ImagesService.Instance.SelectImage(task.Typ)
	            };
	            _historyList.Add(element);
	        }
            HistoryPlan.ItemsSource = _historyList;
        }
    }
}

