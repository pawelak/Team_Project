using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskMaster.Models;
using TaskMaster.Pages;
using Xamarin.Forms;
using XamForms.Controls;
namespace TaskMaster.Pages
{

    public partial class CalendarDayListPage : ContentPage
    {
        private DateTime calendarDay;

        public CalendarDayListPage(DateTime dateTime)
        {
            this.calendarDay = dateTime;
            InitializeComponent();
            // ListInitiate();
            //DisplayAlert("Task", calendarDay.ToString(), "Task", "Task");
        }
        protected override void OnAppearing()
        {
            ListInitiate();
        }

        private async void ListInitiate()
        {
            var result = await App.Database.GetPartsOfActivityByStatus(calendarDay);
            List<CustomList> dayPlan = new List<CustomList>();

            foreach (var part in result)
            {
                var task = new CustomList()
                {
                    Name = part.PartId,
                    Start = part.Start,
                    Stop = part.Stop,
                    Duration = part.Duration,
                };
                dayPlan.Add(task);
            }
            DayPlan.ItemsSource = dayPlan;
        }
        public struct CustomList
        {
            public int Name;
            public string Start;
            public string Stop;
            public string Duration;
        }
    }
}