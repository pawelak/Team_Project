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
            Auth.GoogleSignInApi.SignOut(MainActivity.MGoogleApiClient);
            ((Activity) Forms.Context).Finish();
        }
    }
}