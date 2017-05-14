using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotifications;
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly UserService _userService = new UserService();

        protected override async void OnCreate(Bundle bundle)
        {
           // LoadNotifications("dasdsadx", "asdasd 5 minut", 50, DateTime.Now.AddSeconds(30));
          //  LoadNotifications("x", "Za 5 minut", 100, DateTime.Now.AddSeconds(60));
           
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await _userService.GetTaskById(activity.TaskId);
                var part = await _userService.GetLastActivityPart(activity.ActivityId);
                /*
                if (DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null) < DateTime.Now)
                {
                    part.Stop = part.Start;
                    activity.Status = StatusType.Stop;
                    await _userService.SaveActivity(activity);
                    await _userService.SavePartOfActivity(part);
                    continue;
                }
                */
                //CrossLocalNotifications.Current.Show(task.Name, "Za 5 minut", part.PartId, DateTime.Parse(part.Start).AddMinutes(-5));
                LoadNotifications(task.Name, "Za 5 minut", part.PartId, DateTime.Now.AddSeconds(20));
            }
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            PauseActivities();
            StartService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnPause();
        }

        protected override void OnResume()
        {
            RestartActivities();
            StopService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnResume();
        }

        private async void PauseActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
                var part = await _userService.GetPartsOfActivityById(item.GetPartId());
                part.Duration = item.GetTime().ToString();
                await _userService.SavePartOfActivity(part);
                item.Stop();
            }
        }

        private static void RestartActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
                item.Restart();
            }
        }
        private void LoadNotifications(string name,string textdesc, int Id, DateTime whenToStart)
        {
            Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
            alarmIntent.PutExtra("name", name);
            alarmIntent.PutExtra("textdesc", textdesc);
            alarmIntent.PutExtra("Id", Id);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, Id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)this.GetSystemService(Context.AlarmService);

            //TODO: For demo set after 5 seconds.
         
            alarmManager.Set(AlarmType.RtcWakeup, whenToStart.Millisecond, pendingIntent);
            /*
            // Set up an intent so that tapping the notifications returns to this app:
            Intent intent = new Intent(this, typeof(StartOfPlanned));
            
            intent.PutExtra("Id", Id);
            // Create a task stack builder to manage the back stack:
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);

            // Add all parents of SecondActivity to the stack: 
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(StartOfPlanned)));

            // Push the intent that starts SecondActivity onto the stack:
            stackBuilder.AddNextIntent(intent);

            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
 
            PendingIntent pendingIntent =
                 stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
            Notification.Builder builder = new Notification.Builder(this)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                 .SetContentTitle(name)
                 .SetContentText(textdesc)
                 .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                 .SetSmallIcon(Resource.Drawable.historyActive);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            
            notificationManager.Notify(Id, notification);
        }
        
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Intent = intent;
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();
            */

        }


    }


    }

