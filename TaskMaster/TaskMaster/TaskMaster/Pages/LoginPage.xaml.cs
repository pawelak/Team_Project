using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using TaskMaster.Droid;
namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
        public LoginPage ()
		{
			InitializeComponent ();
		}

	    private void Google_OnClicked(object sender, EventArgs e)
	    {
	        if (Device.RuntimePlatform == "Android")
	        {
	        }
	        else if (Device.RuntimePlatform == "iOS")
	        {
	        }
	    }

	    private void Facebook_OnClicked(object sender, EventArgs e)
	    {
	        //await Navigation.PushModalAsync(new ProviderLoginPage("FaceBook"));
        }
	}
}
