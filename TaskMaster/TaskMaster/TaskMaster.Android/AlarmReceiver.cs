using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace TaskMaster.Droid
{
    [BroadcastReceiver]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var name = intent.GetStringExtra("name");
            var textdesc = intent.GetStringExtra("textdesc");
            int Id = intent.Extras.GetInt("Id", -1);
            // Set up an intent so that tapping the notifications returns to this app:
            Intent intentnote = new Intent(context, typeof(StartOfPlanned));

            intentnote.PutExtra("Id", Id);
            var pendingIntent = PendingIntent.GetService(context, Id, intentnote,0);
            var manager = NotificationManagerCompat.From(context);
            // Create a task stack builder to manage the back stack:
           // TaskStackBuilder stackBuilder = TaskStackBuilder.Create(context);

            // Add all parents of SecondActivity to the stack: 
           // stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(StartOfPlanned)));

            // Push the intent that starts SecondActivity onto the stack:
           // stackBuilder.AddNextIntent(intentnote);

            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):

            //PendingIntent pendingIntent =
                // stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
            Notification.Builder builder = new Notification.Builder(context)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                 .SetContentTitle(name)
                 .SetContentText(textdesc)
                 .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                 .SetSmallIcon(Resource.Drawable.historyActive);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
           // NotificationManager notificationManager =
               // GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:

            manager.Notify(Id, notification);
        }
    }
}