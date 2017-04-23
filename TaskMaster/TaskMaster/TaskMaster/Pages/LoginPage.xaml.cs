using System;
using Xamarin.Forms.Xaml;
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
	        await Navigation.PushModalAsync(new ProviderLoginPage("Google"));
        }

	    private async void Facebook_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new ProviderLoginPage("FaceBook"));
        }
	}
}
