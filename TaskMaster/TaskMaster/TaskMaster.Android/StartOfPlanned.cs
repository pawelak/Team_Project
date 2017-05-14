using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android;
using System.Diagnostics;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Android.Content;
using TaskMaster.Droid;

namespace TaskMaster
{

    [Activity(Label = "Start Of Planned")]
    public class StartOfPlanned : Activity
    {
        private ActivitiesDto _activity;
        private DateTime _now;
        private PartsOfActivityDto _part;
        private readonly UserService _userService = new UserService();

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            int Id = Intent.Extras.GetInt("Id", -1);
            //Console.WriteLine(Id);
            
             _activity = await _userService.GetActivity(Id);
             _activity.Status = StatusType.Start;
             _part = await _userService.GetLastActivityPart(Id);
             _now = DateTime.Now;
             var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
             _part.Start = date;
             await _userService.SavePartOfActivity(_part);
             await _userService.SaveActivity(_activity);
             var sw = new Stopwatch();
             var stopwatch = new Stopwatches(sw, _part.PartId);
             App.Stopwatches.Add(stopwatch);
             App.Stopwatches[App.Stopwatches.Count - 1].Start();
            Finish();
           //  Intent i = new Intent(this, typeof(MainActivity));
           //  i.AddFlags(ActivityFlags.ReorderToFront);
           //  this.StartActivity(i);
          //   this.FinishActivity(0);
             


        }
    }   
}