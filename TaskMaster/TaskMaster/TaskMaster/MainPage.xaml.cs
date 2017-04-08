using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskMaster.Models;
using TaskMaster.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;
namespace TaskMaster
{
    public struct ElemList
    {
        public string Name { get; set; }
        public int ActivityId { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
    }
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
                Navigation.PushModalAsync(new CalendarDayListPage(calendar.SelectedDates[0]));
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
	        List<ElemList> activeTasks = new List<ElemList>();
	        foreach (var activity in result)
	        {
	            if (activity.TaskId == 0)
	            {
	                var item = new ElemList()
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
                        ActivityId = activity.ActivityId
	                };
	                activeTasks.Add(item);
	            }
	            else
	            {
	                var task = await App.Database.GetTaskById(activity.TaskId);
                    var item = new ElemList()
                    {
                        Name = task.Name,
                        Description = task.Description,
                        ActivityId = activity.ActivityId,
                        TaskId = task.TaskId
                    };
	                activeTasks.Add(item);
	            }
	        }
	        var result2 = await App.Database.GetActivitiesByStatus(StatusType.Pause);
	        foreach (var activity in result2)
	        {
	            if (activity.TaskId == 0)
	            {
	                var item = new ElemList()
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId
	                };
	                activeTasks.Add(item);
	            }
	            else
	            {
	                var task = await App.Database.GetTaskById(activity.TaskId);
	                var item = new ElemList()
	                {
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId
	                };
	                activeTasks.Add(item);
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
	            Status = StatusType.Start,
                UserId = 1,
                GroupId = 1
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

        private async void FillCalendar()
        {
            parts = await App.Database.GetPartsList();
            foreach (PartsOfActivity p in parts)
            {
                calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(p.Start)) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.Blue, BorderWidth = 8, Selectable = true });
            }
            calendar.RaiseSpecialDatesChanged();//refresh
        }

	    private async void ActiveTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var item = (ElemList)e.Item;
	        await Navigation.PushModalAsync(new EditTaskPage(item));
	    }
	}
}
