﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskMaster.Pages
{

    public partial class CalendarDayListPage
    {
        private DateTime _calendarDay;
        private readonly UserService _userService = new UserService();
        public CalendarDayListPage(DateTime dateTime)
        {
            _calendarDay = dateTime;
            InitializeComponent();
            ListInitiate();
        }
        protected override void OnAppearing()
        {
            ListInitiate();
        }

        private async void ListInitiate()
        {
            try
            {
                var result = await _userService.GetActivitiesByStatus(StatusType.Stop);
                var dayPlan = new List<CustomList>();
                foreach (var activity in result)
                {
                    var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Where(part => DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy").Equals(_calendarDay.ToString("dd/MM/yyyy"))).Sum(part => long.Parse(part.Duration));
                    var task = await _userService.GetTaskById(activity.TaskId);
                    var t = TimeSpan.FromMilliseconds(time);
                    var element = new CustomList
                    {
                        Name = task.Name,
                        Description = activity.Status.ToString(),
                        Time = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s"
                    };
                    dayPlan.Add(element);
                }
                var result2 = await _userService.GetActivitiesByStatus(StatusType.Planned);
                foreach (var activity in result2)
                {
                    var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Where(part => DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy").Equals(_calendarDay.ToString("dd/MM/yyyy"))).Sum(part => long.Parse(part.Duration));
                    var task = await _userService.GetTaskById(activity.TaskId);
                    var t = TimeSpan.FromMilliseconds(time);
                    var element = new CustomList
                    {
                        Name = task.Name,
                        Description = activity.Status.ToString(),
                        Time = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s"
                    };
                    dayPlan.Add(element);
                }
                var result3 = await _userService.GetActivitiesByStatus(StatusType.Pause);
                foreach (var activity in result3)
                {
                    var parts = await _userService.GetPartsOfActivityByActivityId(activity.ActivityId);
                    var time = parts.Where(part => DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null).ToString("dd/MM/yyyy").Equals(_calendarDay.ToString("dd/MM/yyyy"))).Sum(part => long.Parse(part.Duration));
                    var task = await _userService.GetTaskById(activity.TaskId);
                    var t = TimeSpan.FromMilliseconds(time);
                    var element = new CustomList
                    {
                        Name = task.Name,
                        Description = activity.Status.ToString(),
                        Time = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s"
                    };
                    dayPlan.Add(element);
                }
                DayPlan.ItemsSource = dayPlan;
            }
            catch (Exception e)
            {
                await DisplayAlert("erorr", e.Message, "ok");
                throw;
            }
        }
       
    }
}