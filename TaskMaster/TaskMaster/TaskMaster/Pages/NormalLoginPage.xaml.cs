using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NormalLoginPage : ContentPage
	{
		public NormalLoginPage ()
		{
			InitializeComponent ();
		}
        private async void AcceptLogin_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Dane", LoginEntry.Text+" "+PasswordEntry.Text, "Ok");
        }
    }
}
