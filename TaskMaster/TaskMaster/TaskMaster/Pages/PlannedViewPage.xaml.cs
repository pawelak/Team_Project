using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
	        await ListInitiate();
	        Device.BeginInvokeOnMainThread(() =>
	        {
	            PlannedTasks.ItemsSource = _plannedList;
	        });
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

	    private async Task ListInitiate()
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
	                Image = SelectImage(task.Typ)
	            };
	            _plannedList.Add(element);
	        }

	    }

	    private static string SelectImage(string item)
	    {
	        string obraz;
	        switch (item)
	        {
	            case "Sztuka":
	                obraz = "art.png";
	                break;
	            case "Inne":
	                obraz = "OK.png";
	                break;
	            case "Programowanie":
	                obraz = "programming.png";
	                break;
	            case "Sport":
	                obraz = "sport.png";
	                break;
	            case "Muzyka":
	                obraz = "music.png";
	                break;
	            case "Języki":
	                obraz = "language.png";
	                break;
	            case "Jedzenie":
	                obraz = "eat.png";
	                break;
	            case "Rozrywka":
	                obraz = "instrument.png";
	                break;
	            case "Podróż":
	                obraz = "car.png";
	                break;
	            case "Przerwa":
	                obraz = "Cafe.png";
	                break;
	            default:
	                obraz = "OK.png";
	                break;
	        }
	        return obraz;
	    }

	    private async void PlannedTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var item = e.Item as PlannedListItem;
	        if (item == null)
	        {
	            return;
	        }
	        var activity = await UserService.Instance.GetActivity(item.ActivityId);
	        activity.Status = StatusType.Canceled;
	        await UserService.Instance.SaveActivity(activity);
	        // anulowanie notki
	        // wysłać do webApi?
            OnAppearing();
	    }

	    private void SyncItem_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}