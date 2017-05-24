using System;
using Android.App;
using Android.Content;
using TaskMaster.ModelsDto;
using TaskMaster.Services;

namespace TaskMaster.Droid
{

    [Service]
    [IntentFilter(new[] { "com.xamarin.StockService" })]
    public class StartOfPlannedService : IntentService
    {
        private ActivitiesDto _activity;
        private DateTime _now;
        private PartsOfActivityDto _part;
        
        protected override async void OnHandleIntent(Intent intent)
        {
            var id = intent.Extras.GetInt("Id", -1);
            _activity = await Services.UserService.Instance.GetActivity(id);
            _activity.Status = StatusType.Start;
            _part = await Services.UserService.Instance.GetLastActivityPart(id);
            _now = DateTime.Now;
            var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Start = date;
            await Services.UserService.Instance.SavePartOfActivity(_part);
            await Services.UserService.Instance.SaveActivity(_activity);
            StopwatchesService.Instance.AddStopwatch(_part.PartId);

        }
    }
}