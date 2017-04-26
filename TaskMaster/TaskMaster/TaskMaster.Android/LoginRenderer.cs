using Android.App;
using Android.Content;
using Android.Support.CustomTabs;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TaskMaster.Droid;
using TaskMaster.Pages;
using Xamarin.Auth;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]
namespace TaskMaster.Droid
{
    public class LoginRenderer : PageRenderer
    {
        public static OAuth2Authenticator Auth;
        private bool _showLogin = true;
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            try
            {
                var loginPage = Element as ProviderLoginPage;
                string providerName = loginPage.ProviderName;
                var activity = Context as Activity;
                if (_showLogin)
                {
                    _showLogin = false;
                    OAuthProviderSetting oauth = new OAuthProviderSetting();
                    Auth = oauth.LoginWithProvider(providerName);
                    Auth.Completed += (sender, eventArgs) =>
                    {
                        activity.Finish();
                        if (eventArgs.IsAuthenticated)
                        {
                            Toast.MakeText(activity,"Działa",ToastLength.Long).Show();
                        }
                    };
                    System.Object uiObject = Auth.GetUI(activity);
                    if (Auth.IsUsingNativeUI)
                    {
                        System.Uri uriNetfx = Auth.GetInitialUrlAsync().Result;
                        Android.Net.Uri uriAndroid = Android.Net.Uri.Parse(uriNetfx.AbsoluteUri);
                        CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
                        CustomTabsIntent ctIntent = builder.Build();
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