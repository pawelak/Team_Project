using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaskMaster.ModelsDto;
using TaskMaster.Pages;
using Xamarin.Forms;
using XamForms.Controls;
namespace TaskMaster
{
	public partial class MainPage
	{
	    private readonly UserServices _userServices = new UserServices();
        private readonly Calendar _calendar;
        private readonly Label _msg;
        private List<PartsOfActivityDto> _parts;
        public MainPage()
		{
            _msg = new Label
            {
                Text = "Wybierz Dzień",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center
            };
            _calendar = new Calendar
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
            _calendar.DateClicked += (sender, e) => {
                Navigation.PushModalAsync(new CalendarDayListPage(_calendar.SelectedDates[0]));
            };
            var vm = new CalendarVm();
            _calendar.SetBinding(Calendar.DateCommandProperty, nameof(vm.DateChosen));
            _calendar.SetBinding(Calendar.SelectedDateProperty, nameof(vm.Date));
            _calendar.BindingContext = vm;
            InitializeComponent ();
		    ListInitiate();
        }

	    private async void ListInitiate()
	    {
	        var activeTasksList = new List<MainPageList>();
            var result = await _userServices.GetActivitiesByStatus(StatusType.Start);
	        foreach (var activity in result)
	        {
	            if (activity.TaskId == 0)
	            {
	                var item = new MainPageList
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	            else
	            {
	                var task = await _userServices.GetTaskById(activity.TaskId);
	                var item = new MainPageList
	                {
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	        }
            var result2 = await _userServices.GetActivitiesByStatus(StatusType.Pause);
	        foreach (var activity in result2)
	        {
	            if (activity.TaskId == 0)
	            {
	                var item = new MainPageList
	                {
	                    Name = "Unnamed Activity " + activity.ActivityId,
	                    ActivityId = activity.ActivityId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	            else
	            {
	                var task = await _userServices.GetTaskById(activity.TaskId);
	                var item = new MainPageList
	                {
	                    Name = task.Name,
	                    Description = task.Description,
	                    ActivityId = activity.ActivityId,
	                    TaskId = task.TaskId,
	                    Duration = "0"
	                };
	                activeTasksList.Add(item);
	            }
	        }
            ActiveTasks.ItemsSource = activeTasksList;
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
	        var activity = new ActivitiesDto
	        {
	            Status = StatusType.Start,
	            UserId = 1,
	            GroupId = 1,
                TaskId = 0
	        };
	        activity.ActivityId = await _userServices.SaveActivity(activity);
	        DateTime now = DateTime.Now;
	        var part = new PartsOfActivityDto
	        {
	            ActivityId = activity.ActivityId,
	            Start = now.ToString("HH:mm:ss dd/MM/yyyy")
	        };
	        await _userServices.SavePartOfActivity(part);
	        Stopwatch sw = new Stopwatch();
	        App.Stopwatches.Add(sw);
	        App.Stopwatches[App.Stopwatches.Count - 1].Start();
	        ListInitiate();
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
            FillCalendar();
            await Navigation.PushModalAsync(new NavigationPage(new CalendarPage
            {
                BackgroundColor = Color.White,
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, Device.OS == TargetPlatform.iOS ? 25 : 5, 5, 5),
                        VerticalOptions = LayoutOptions.Center, 
                        Children = {
                            _msg, _calendar
                        }
                    }
                }
            }));
        }

	    private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
        { 
            await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
	    }

        private async void FillCalendar()
        {
            _parts = await _userServices.GetPartsOfActivityList();
            foreach (PartsOfActivityDto p in _parts)
            {
                _calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(p.Start)) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.Blue, BorderWidth = 8, Selectable = true });
            }
            _calendar.RaiseSpecialDatesChanged();//refresh
        }

	    private async void ActiveTasks_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var item = (MainPageList)e.Item;
	        await Navigation.PushModalAsync(new EditTaskPage(item));
        }
	}
}
