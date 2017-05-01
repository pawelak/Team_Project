using System;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Auth.XamarinForms;
namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
        public LoginPage ()
		{
			InitializeComponent ();
		}

	    private async void Google_OnClicked(object sender, EventArgs e)
	    {
	        try
	        {
	            var oauth = new OAuthProviderSetting();
	            var auth = oauth.LoginWithProvider("Google");
	            auth.Completed += (s, ea) =>
	            {
	                StringBuilder sb = new StringBuilder();
	                if (ea.Account?.Properties != null)
	                {
	                    sb.Append("Token = ").AppendLine($"{ea.Account.Properties["access_token"]}");
	                }
	                else
	                {
	                    sb.Append("Not authenticated ").AppendLine("Account.Properties does not exist");
	                }
	                DisplayAlert
	                (
	                    "Authentication Results",
	                    sb.ToString(),
	                    "OK"
	                );
	            };

	            auth.Error += (s, ea) =>
	            {
	                var sb = new StringBuilder();
	                sb.Append("Error = ").AppendLine($"{ea.Message}");
	                DisplayAlert
	                (
	                    "Authentication Error",
	                    sb.ToString(),
	                    "OK"
	                );
	            };
	            AuthenticationState.Authenticator = auth;
	            if (true)
	            {
	                // Renderers Implementation
	                await Navigation.PushModalAsync(new AuthenticatorPage()
	                {
	                    Authenticator = auth
	                });
	            }
	            else
	            {
	                // Presenters Implementation
	                Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
	                presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
	                presenter.Login(auth);
	            }
	        }
	        catch (Exception exception)
	        {
	            await DisplayAlert("e", exception.Message, "OK");
	            throw;
	        }
	    }

	    private void Facebook_OnClicked(object sender, EventArgs e)
	    {
	        //await Navigation.PushModalAsync(new ProviderLoginPage("FaceBook"));
        }
	}
}
