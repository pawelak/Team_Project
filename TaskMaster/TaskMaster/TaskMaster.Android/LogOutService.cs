using Android.App;
using Android.Gms.Auth.Api;
using TaskMaster.Droid;
using TaskMaster.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(LogOutService))]
namespace TaskMaster.Droid
{
    public class LogOutService:ILogOutService
    {
        public void LogOut()
        {
            Auth.GoogleSignInApi.SignOut(MainActivity._mGoogleApiClient);
            //Stop or pause all activities?
            ((Activity)Forms.Context).Finish();
        }
    }
}