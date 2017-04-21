using SafariServices;
using TaskMaster.iOS;
using TaskMaster.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]
namespace TaskMaster.iOS
{
    public class LoginRenderer : PageRenderer
    {
        bool showLogin = true;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            OAuthProviderSetting oauth = new OAuthProviderSetting();
            //Get and Assign ProviderName from ProviderLoginPage
            var loginPage = Element as ProviderLoginPage;
            string providername = loginPage.ProviderName;
            if (showLogin)
            {
                var auth = oauth.LoginWithProvider(providername);
                auth.Completed += (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)
                    {

                    }
                };
                PresentViewController((SFSafariViewController)auth.GetUI(), true, null);
            }
        }
    }
}