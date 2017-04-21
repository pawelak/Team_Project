using Android.App;
using Android.Support.CustomTabs;
using Java.IO;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TaskMaster.Droid;
using TaskMaster.Pages;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]
namespace TaskMaster.Droid
{
    public class LoginRenderer : PageRenderer
    {
        bool showLogin = true;
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            try
            {
                var loginPage = Element as ProviderLoginPage;
                string providerName = loginPage.ProviderName;
                var activity = Context as Activity;
                if (showLogin)
                {
                    showLogin = false;
                    OAuthProviderSetting oauth = new OAuthProviderSetting();
                    var auth = oauth.LoginWithProvider(providerName);
                    auth.Completed += (sender, eventArgs) =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {

                        }
                    };
                    System.Object uiObject = auth.GetUI(activity);
                    if (auth.IsUsingNativeUI)
                    {
                        System.Uri uriNetfx = auth.GetInitialUrlAsync().Result;
                        Android.Net.Uri uriAndroid = Android.Net.Uri.Parse(uriNetfx.AbsoluteUri);
                        CustomTabsIntent.Builder ctib = (CustomTabsIntent.Builder) uiObject;
                        CustomTabsIntent ctIntent = ctib.Build();
                        ctIntent.LaunchUrl(activity, uriAndroid);
                    }
                    else
                    {
                        Android.Content.Intent i = null;
                        i = (Android.Content.Intent) uiObject;
                        activity.StartActivity(i);
                    }
                }
            }
            catch (System.Exception a)
            {
                System.Console.WriteLine(a);
            }
        }
    }

}