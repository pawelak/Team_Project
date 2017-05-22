using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.Lists;
using TaskMaster.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlannedViewPage
	{
        private readonly List<PlannedList> _plannedList = new List<PlannedList>();
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
	        await UserService.Instance.LogoutUser();
	        // tu musi być wyjście z apki
	    }

	    private async Task ListInitiate()
	    {
	        var activitiesPlannedList = await UserService.Instance.GetActivitiesByStatus(StatusType.Planned);
	        foreach (var activity in activitiesPlannedList)
	        {
	            var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
	            var task = await UserService.Instance.GetTaskById(activity.TaskId);
	            var element = new PlannedList
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
	        var item = e.Item as PlannedList;
	        if (item == null)
	        {
	            return;
	        }
	        var activity = await UserService.Instance.GetActivity(item.ActivityId);
	        activity.Status = StatusType.Canceled;
	        await UserService.Instance.SaveActivity(activity);
	        // anulowanie notki
	        // wysłać do webApi?
	    }

	    private void SyncItem_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}