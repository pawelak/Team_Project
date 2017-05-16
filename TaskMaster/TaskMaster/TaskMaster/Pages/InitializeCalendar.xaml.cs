using System;
using System.Collections.Generic;
using TaskMaster.ModelsDto;
using TaskMaster.Services;
using Xamarin.Forms;
using XamForms.Controls;
namespace TaskMaster.Pages
{
   
    public partial class InitializeCalendar
    {
        private readonly Calendar _calendar;
        private List<PartsOfActivityDto> _parts;

        public InitializeCalendar()
        {
            var msg = new Label
            {
                Text = "Wybierz Dzień",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center
            };
            _calendar = new Calendar
            {
                MultiSelectDates = false,
                DisableAllDates = true,
                WeekdaysShow = true,
                ShowNumberOfWeek = true,
                ShowNumOfMonths = 1,
                EnableTitleMonthYearView = true,
                WeekdaysTextColor = Color.Teal,
                StartDay = DayOfWeek.Monday,
                SelectedTextColor = Color.Fuchsia
            };
            _calendar.DateClicked += (sender, e) =>
            {
                Navigation.PushModalAsync(new CalendarDayListPage(e.DateTime));
            };
            var vm = new CalendarVm();
            _calendar.SetBinding(Calendar.DateCommandProperty, nameof(vm.DateChosen));
            _calendar.SetBinding(Calendar.SelectedDateProperty, nameof(vm.Date));
            _calendar.BindingContext = vm;
            FillCalendar();
            Navigation.PushModalAsync(new NavigationPage(new CalendarPage
            {
                BackgroundColor = Color.White,
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, Device.RuntimePlatform == "iOS" ? 25 : 5, 5, 5),
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            msg,
                            _calendar
                        }
                    }
                }
            }));
        }

        private void FillCalendar()
        {
            AddDatesByStatus(StatusType.Stop);
            AddDatesByStatus(StatusType.Pause);
            AddDatesByStatus(StatusType.Planned);
            _calendar.RaiseSpecialDatesChanged();
        }
        
        private async void AddDatesByStatus(StatusType status)
        {
            var activities = await UserService.Instance.GetActivitiesByStatus(status);
            foreach (var activity in activities)
            {
                var last = await UserService.Instance.GetLastActivityPart(activity.ActivityId);

                if (DateTime.Compare(DateTime.Now.AddDays(-7),
                        DateTime.ParseExact(last.Start, "HH:mm:ss dd/MM/yyyy", null)) >= 0)
                {
                    continue;
                }
                if (activity.TaskId == 0)
                {
                    continue;
                }
                _parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
                foreach (var k in _parts)
                {
                    _calendar.SpecialDates.Add(new SpecialDate(DateTime.ParseExact(k.Start, "HH:mm:ss dd/MM/yyyy", null)) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.Blue, BorderWidth = 8, Selectable = true });
                }
            }
        }
    }

}
