using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using TaskMaster.ModelsDto;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        GoogleApiClient.IOnConnectionFailedListener
    {
        private GoogleApiClient _mGoogleApiClient;
        private const int RcSignIn = 9001;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            /*var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestEmail()
                .RequestIdToken("723494873981-np3v1u9js6jman2qri5r0gfd7fl3g3c2.apps.googleusercontent.com")
                .Build();
            _mGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();
            SignIn();  */
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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != RcSignIn)
            {
                return;
            }
            var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
            HandleSignInResult(result);
        }

        private async void HandleSignInResult(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                var acc = result.SignInAccount;
                var email = acc.Email;
                var idToken = acc.IdToken;
                var user = new UserDto
                {
                    Name = email,
                    Token = idToken,
                    TypeOfRegistration = "Google"
                };
                await Services.UserService.Instance.SaveUser(user);
                LoadApplication(new App());
            }
            else
            {
                Finish();
            }
        }

    }

}

