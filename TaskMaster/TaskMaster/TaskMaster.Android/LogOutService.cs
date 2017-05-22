using Android.App;
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
            //Stop or pause all activities?
            ((Activity)Forms.Context).Finish();
        }
    }
}