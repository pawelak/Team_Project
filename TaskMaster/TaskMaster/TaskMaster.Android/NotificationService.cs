using System;
using Android.App;
using Android.Content;
using TaskMaster.Droid;
using TaskMaster.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace TaskMaster.Droid
{
    public class NotificationService : INotificationService
    {
        public void LoadNotifications(string name, string textdesc, int id, DateTime whenToStart)
        {
            var alarmIntent = new Intent(Xamarin.Forms.Forms.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("name", name);
            alarmIntent.PutExtra("textdesc", textdesc);
            alarmIntent.PutExtra("Id", id);

            var pendingIntent = PendingIntent.GetBroadcast(Xamarin.Forms.Forms.Context, id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager)Xamarin.Forms.Forms.Context.GetSystemService(Context.AlarmService);
            alarmManager.Set(AlarmType.RtcWakeup, NotifyTimeInMilliseconds(whenToStart), pendingIntent);
        }

        public long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }

        public void CancelNotification(int id)
        {
            var alarmIntent = new Intent(Xamarin.Forms.Forms.Context, typeof(AlarmReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(Xamarin.Forms.Forms.Context, id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager)Xamarin.Forms.Forms.Context.GetSystemService(Context.AlarmService);
            pendingIntent.Cancel();
            alarmManager.Cancel(pendingIntent);
        }
    }
}