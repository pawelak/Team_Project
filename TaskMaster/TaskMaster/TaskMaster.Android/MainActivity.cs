using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;


namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        GoogleApiClient.IOnConnectionFailedListener
    {
        private readonly UserService _userService = new UserService();
        private GoogleApiClient _mGoogleApiClient;
        private const int RcSignIn = 9001;

        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestEmail()
                .Build();
            _mGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();
            SignIn();
            var result2 = await _userService.GetActivitiesByStatus(StatusType.Planned);
            foreach (var activity in result2)
            {
                var task = await _userService.GetTaskById(activity.TaskId);
                var part = await _userService.GetLastActivityPart(activity.ActivityId);
                LoadNotifications(task.Name, "Naciśnij aby rozpocząć aktywność", part.PartId,
                    DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null));
            }
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
        }

        private void SignIn()
        {
            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_mGoogleApiClient);
            StartActivityForResult(signInIntent, RcSignIn);
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

        private void LoadNotifications(string name, string textdesc, int id, DateTime whenToStart)
        {
            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            alarmIntent.PutExtra("name", name);
            alarmIntent.PutExtra("textdesc", textdesc);
            alarmIntent.PutExtra("Id", id);

            var pendingIntent = PendingIntent.GetBroadcast(this, id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager) GetSystemService(AlarmService);
            alarmManager.Set(AlarmType.RtcWakeup, NotifyTimeInMilliseconds(whenToStart), pendingIntent);
        }

        private static long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }

        public void OnConnectionFailed(ConnectionResult result)
        {

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == RcSignIn)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                HandleSignInResult(result);
            }
        }

        private void HandleSignInResult(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                GoogleSignInAccount acc = result.SignInAccount;
            }
        }

    }

}

