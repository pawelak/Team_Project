using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Auth;
namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

	    private void Google_OnClicked(object sender, EventArgs e)
	    {
	        var auth = new OAuth2Authenticator(
	            "723494873981-qsnnp5vsa72f4d74bo8m8kqfsrbo25cq.apps.googleusercontent.com",
	            "taskmaster-163310",
                "email",
                new Uri( "https://accounts.google.com/o/oauth2/auth"),
	            new Uri("urn:ietf:wg:oauth:2.0:oob"),
                new Uri("https://accounts.google.com/o/oauth2/token")
	            );
	        var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
	        presenter.Login(auth);
        }

	    private void Facebook_OnClicked(object sender, EventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}
}
