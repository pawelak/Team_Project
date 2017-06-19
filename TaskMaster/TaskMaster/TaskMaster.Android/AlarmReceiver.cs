using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace TaskMaster.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var name = intent.GetStringExtra("name");
            var textDesc = intent.GetStringExtra("textDesc");
            var id = intent.Extras.GetInt("Id", -1);
            var intentNote = new Intent(context, typeof(StartOfPlannedService));

            intentNote.PutExtra("Id", id);
            var pendingIntent = PendingIntent.GetService(context, id, intentNote,0);
            var manager = NotificationManagerCompat.From(context);
            var builder = new Notification.Builder(context)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                 .SetContentTitle(name)
                 .SetContentText(textDesc)
                 .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate | NotificationDefaults.Lights)
                 .SetSmallIcon(Resource.Drawable.historyActive);
            var notification = builder.Build();
            manager.Notify(id, notification);
        }
    }
}