using System.Collections.Generic;
using Xamarin.Forms;
using System;
using System.Diagnostics;
using System.Globalization;
using AVFoundation;
using Plugin.LocalNotifications;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
	public partial class App
	{
	    public static List<Stopwatches> Stopwatches = new List<Stopwatches>();
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
                var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                foreach (var part in parts)
                {
                    CrossLocalNotifications.Current.Show(task.Name, "Za 5 minut", part.PartId, DateTime.Parse(part.Start).AddMinutes(-5)); 
                }
            }
            var started = await _userService.GetActivitiesByStatus(StatusType.Start);
            foreach (var start in started)
            {
                var task = await _userService.GetTaskById(start.TaskId);
                var result = await Current.MainPage.DisplayAlert("Error", "Masz niezapauzowaną aktywność "+task.Name+".\n " +
                                                                          "Czy była ona aktywna od zamknięcia aplikacji? \n" +
                                                                          "Jeżeli wybierzesz nie, czas aktywności może być niewłaściwy \n"+
                                                                          "Jeżeli wybierzesz tak, czas końca aktywności będzie czasem zatwierdzenia tego komunikatu", "Tak", "Nie");
                if (!result)
                {
                    var result4 = await Current.MainPage.DisplayAlert("Error",
                        "Czy chcesz żeby aktywność była kontynuowana?", "Tak", "Nie");
                    if (!result4)
                    {
                        start.Status = StatusType.Pause;
                        await _userService.SaveActivity(start);
                        continue;
                    }
                    var part2 = new PartsOfActivityDto
                    {
                        Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                        ActivityId = start.ActivityId,
                        Duration = "0"
                    };
                    part2.PartId = await _userService.SavePartOfActivity(part2);
                    var sw = new Stopwatch();
                    var stopwatch = new Stopwatches(sw,part2.PartId);
                    Stopwatches.Add(stopwatch);
                    Stopwatches[Stopwatches.Count-1].Start();
                    await _userService.SaveActivity(start);
                    continue;
                }
                var part = await _userService.GetLastActivityPart(start.ActivityId);
                part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                var time = DateTime.Now - DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null);
                part.Duration = time.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
                await _userService.SaveActivity(start);
                await _userService.SavePartOfActivity(part);
                var result3 = await Current.MainPage.DisplayAlert("Error",
                    "Czy chcesz żeby aktywność była kontynuowana?", "Tak", "Nie");
                if (!result3)
                {
                    start.Status = StatusType.Pause;
                    await _userService.SaveActivity(start);
                    continue;
                }
                var part3 = new PartsOfActivityDto
                {
                    Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                    ActivityId = start.ActivityId,
                    Duration = "0"
                };
                part3.PartId = await _userService.SavePartOfActivity(part3);
                var sw2 = new Stopwatch();
                var stopwatch2 = new Stopwatches(sw2, part3.PartId);
                Stopwatches.Add(stopwatch2);
                Stopwatches[Stopwatches.Count - 1].Start();
            }
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
       
    }
}
