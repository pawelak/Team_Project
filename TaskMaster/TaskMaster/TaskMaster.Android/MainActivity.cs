using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Widget;
using TaskMaster.Enums;
using TaskMaster.ModelsDto;
using TaskMaster.Services;
using Xamarin.Forms;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        GoogleApiClient.IOnConnectionFailedListener
    {
        public static GoogleApiClient MGoogleApiClient;
        private const int RcSignIn = 9001;
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestEmail()
                .RequestIdToken("723494873981-np3v1u9js6jman2qri5r0gfd7fl3g3c2.apps.googleusercontent.com")
                .Build();
            MGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();
            var check = await Services.UserService.Instance.IsLoggedUser();
            if(check)
            {
                LoadApplication(new App());
            }
            else
            {
                SignIn();
            }
        }
        private void SignIn()
        {
            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(MGoogleApiClient);
            StartActivityForResult(signInIntent, RcSignIn);
        }

        protected override void OnPause()
        {
            StartService(new Intent(this, typeof(BackgroundStopwatches)));            
            base.OnPause();
        }

        protected override void OnResume()
        {
            StopService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnResume();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {

        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != RcSignIn)
            {
                return;
            }
            var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
            await HandleSignInResult(result);
        }

        private async Task HandleSignInResult(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                var acc = result.SignInAccount;
                var email = acc.Email;
                var idToken = acc.IdToken;
                var user = await Services.UserService.Instance.GetUserByEmail(email);
                if (user == null)
                {
                    var userDto = new UserDto
                    {
                        Name = email,
                        Token = idToken,
                        TypeOfRegistration = "Google",
                        SyncStatus = SyncStatus.ToUpload,
                        IsLoggedIn = true
                    };
                    userDto.UserId = await Services.UserService.Instance.SaveUser(userDto);
                    Services.UserService.Instance.SetLoggedUser(userDto);
                    await SynchronizationService.Instance.SendUser(userDto);
                }
                else
                {
                    user.Token = idToken;
                    user.SyncStatus = SyncStatus.ToUpload;
                    user.IsLoggedIn = true;
                    user.UserId = await Services.UserService.Instance.SaveUser(user);
                    Services.UserService.Instance.SetLoggedUser(user);
                    await SynchronizationService.Instance.SendUser(user);
                }
                /*await SynchronizationService.Instance.SendTasks();
                await SynchronizationService.Instance.SendActivities();
                await SynchronizationService.Instance.GetActivities();
                await SynchronizationService.Instance.SendFavorites();
                await SynchronizationService.Instance.GetFavorites();
                await SynchronizationService.Instance.SendPlannedAsync();
                await SynchronizationService.Instance.GetPlanned();*/
                LoadApplication(new App());
            }
            else
            {
                Toast.MakeText(this, "Nie udało się zalogować", ToastLength.Long);
                Finish();
            }
        }

    }

}

