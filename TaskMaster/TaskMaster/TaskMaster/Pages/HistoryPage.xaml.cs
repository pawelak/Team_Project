using System;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.Pages;
using TaskMaster.Services;
using Xamarin.Forms;

namespace TaskMaster
{
	
	public partial class HistoryPage
	{
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

	    private async void LogoutItem_OnClicked(object sender, EventArgs e)
	    {
	        await UserService.Instance.LogoutUser();
	        // tu musi być wyjście z apki
	    }

        protected override void OnAppearing()
        {
            ListInitiate();
        }

	    private async void ListInitiate()
	    {
	        var activitiesStoppedList = await UserService.Instance.GetActivitiesByStatus(StatusType.Stop);
	        var historyPlan = new List<HistoryList>();
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
	                }
	                else
	                {
	                    continue;
	                }
	            }
	            var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
	            var time = parts.Sum(part => long.Parse(part.Duration));
	            var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
	            var task = await UserService.Instance.GetTaskById(activity.TaskId);
	            var t = TimeSpan.FromMilliseconds(time);
	            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
                var element = new HistoryList
                {
                    Name = task.Name,
                    Description = activity.Comment,
                    Time = answer,
                    Date = lastPart.Start,
                    Image = SelectImage(task.Typ)
	            };
	            historyPlan.Add(element);
	        }
            HistoryPlan.ItemsSource = historyPlan;
        }
        private string SelectImage(string item)
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

	    
    }
}

