using Foundation;
using UIKit;
namespace TaskMaster.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Xamarin.Forms.Forms.Init ();
            XamForms.Controls.iOS.Calendar.Init();
            LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
		}
	    public override void DidEnterBackground(UIApplication application)
	    {

	    }
    }
}
