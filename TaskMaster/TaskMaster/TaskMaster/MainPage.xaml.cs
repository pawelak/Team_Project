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
        List<PartsOfActivity> parts;
        public MainPage()
		{
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
                System.Diagnostics.Debug.WriteLine(calendar.SelectedDates);
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
                var task = await App.Database.GetTaskById(activity.TaskId);
                activeTasks.Add(task);
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
            App.Stopwatches[App.Stopwatches.Count-1].Start();
        }
	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
            fillCalendar();
            await Navigation.PushModalAsync(new CalendarPage
            {
                BackgroundColor = Color.White,
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, Device.OS == TargetPlatform.iOS ? 25 : 5, 5, 5),
                        Children = {
                            calendar
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
	        throw new NotImplementedException();
	    }
        private async void fillCalendar()
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
