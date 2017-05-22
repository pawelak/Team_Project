using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using TaskMaster.ModelsDto;
using Xamarin.Forms;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        GoogleApiClient.IOnConnectionFailedListener
    {
        public static GoogleApiClient _mGoogleApiClient;
        private const int RcSignIn = 9001;
        private int user;
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            /*var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestEmail()
                .RequestIdToken("723494873981-np3v1u9js6jman2qri5r0gfd7fl3g3c2.apps.googleusercontent.com")
                .Build();
            _mGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();
            await GetUser();
            if (user != -1)
            {
                LoadApplication(new App());
            }
            else
            {
                SignIn();
            }*/
            LoadApplication(new App());
        }

        private async Task GetUser()
        {
            user = await Services.UserService.Instance.GetLoggedUser();
        }
        private void SignIn()
        {
            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_mGoogleApiClient);
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
                var userDto = new UserDto
                {
                    Name = email,
                    Token = idToken,
                    TypeOfRegistration = "Google",
                    SyncStatus = SyncStatusType.ToUpload,
                    IsLoggedIn = true
                };
                await Services.UserService.Instance.SaveUser(userDto);
                //await Services.SynchronizationService.Instance.SendUser(user);
                LoadApplication(new App());
            }
            else
            {
                Finish();
            }
        }

    }

}

