using System;
using System.Collections.Generic;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using XamForms.Controls;
namespace TaskMaster.Pages
{
   
    public partial class InitializeCalendar
    {
        private readonly UserService _userService = new UserService();
        private readonly Calendar _calendar;
        private List<PartsOfActivityDto> _parts;
        public InitializeCalendar()
        {
            try
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
                FillCalendar();
                Navigation.PushModalAsync(new NavigationPage(new CalendarPage
                {
                    BackgroundColor = Color.White,
                    Content = new ScrollView
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(5, Device.OS == TargetPlatform.iOS ? 25 : 5, 5, 5),
                            VerticalOptions = LayoutOptions.Center,
                            Children = {
                                msg, _calendar
                            }
                        }
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        protected override async void OnDisappearing()
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }
        private async void FillCalendar()
        {
            _parts = await _userService.GetPartsOfActivityList();
            foreach (var p in _parts)
            {
                _calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(p.Start)) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.Blue, BorderWidth = 8, Selectable = true });
            }
            _calendar.RaiseSpecialDatesChanged();//refresh
        }
        protected override void OnAppearing()
        {

        }
    }

}
