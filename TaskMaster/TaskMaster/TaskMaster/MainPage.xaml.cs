using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskMaster.Models;
using TaskMaster.Pages;
using Xamarin.Forms;
using XamForms.Controls;
namespace TaskMaster
{

	public partial class MainPage : ContentPage
	{
        Calendar calendar;
        Label MSG;
        List<PartsOfActivity> parts;
        public MainPage()
		{
            MSG = new Label
            {
                Text = "Wybierz Dzień",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center
            };
            calendar = new Calendar
            {
                MultiSelectDates = false,
                DisableAllDates = false,
                WeekdaysShow = true,
                ShowNumberOfWeek = true,
                ShowNumOfMonths = 1,
                EnableTitleMonthYearView = true,
                WeekdaysTextColor = Color.Teal,
                StartDay = DayOfWeek.Monday,
                SelectedTextColor = Color.Fuchsia,
            };

            calendar.DateClicked += (sender, e) => {
                 Navigation.PushModalAsync(new CalendarDayListPage((calendar.SelectedDates[0])));
               
            };
            var vm = new CalendarVM();
            calendar.SetBinding(Calendar.DateCommandProperty, nameof(vm.DateChosen));
            calendar.SetBinding(Calendar.SelectedDateProperty, nameof(vm.Date));
            calendar.BindingContext = vm;
            InitializeComponent ();
		    ListInitiate();
		}

	    protected override void OnAppearing()
	    {
	        ListInitiate();
	    }

	    private async void ListInitiate()
	    {
	        var result = await App.Database.GetActivitiesByStatus(StatusType.Start);
	        List<Tasks> activeTasks = new List<Tasks>();
	        foreach (var activity in result)
	        {
	            if (activity.TaskId == 0)
	            {
	                var task = new Tasks()
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                };
	                activeTasks.Add(task);
	            }
	            else
	            {
	                var task2 = await App.Database.GetTaskById(activity.TaskId);
	                activeTasks.Add(task2);
	            }
	        }
	        ActiveTasks.ItemsSource = activeTasks;
	    }

        private async void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new StartTaskPage());
	    }

	    private async void PlanTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new PlanTaskPage());
	    }

	    private async void FastTaskButton_OnClicked(object sender, EventArgs e)
	    {
	        var task = new Tasks()
	        {
	            TaskId = 0
	        };
	        await App.Database.SaveTask(task);
	        var activity = new Activities()
	        {
	            Status = StatusType.Start
	        };
	        activity.ActivityId = await App.Database.SaveActivity(activity);
	        DateTime now = DateTime.Now;
	        var part = new PartsOfActivity()
	        {
	            ActivityId = activity.ActivityId,
	            Start = now.ToString("HH:mm:ss dd/MM/yyyy")
	        };
	        await App.Database.SavePartOfTask(part);
	        Stopwatch sw = new Stopwatch();
	        App.Stopwatches.Add(sw);
	        App.Stopwatches[App.Stopwatches.Count - 1].Start();
	        ListInitiate();
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
            FillCalendar();
            await Navigation.PushModalAsync(new CalendarPage
            {
                BackgroundColor = Color.White,
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, Device.OS == TargetPlatform.iOS ? 25 : 5, 5, 5),
                        VerticalOptions = LayoutOptions.Center, 
                        Children = {
                            MSG, calendar
                        }
                    }
                }
            });
        }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new HistoryPage());
	    }

	    private void ActiveTasks_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        //DisplayAlert("test", sender.ToString(), "a", "f");
	        //throw new NotImplementedException();
        }
        private async void FillCalendar()
        {
            parts = await App.Database.GetPartsList();

            foreach (PartsOfActivity p in parts)
            {
                calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(p.Start)) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.Blue, BorderWidth = 8, Selectable = true });
            }
            calendar.RaiseSpecialDatesChanged();//refresh
        }
    }
}
