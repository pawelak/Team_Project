using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Models;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTaskPage : ContentPage
	{
		public StartTaskPage ()
		{
			InitializeComponent ();
		}

	    private void StartTaskButton_OnClicked(object sender, EventArgs e)
	    {
            try
            {
                var user = new User() {
                    name = "test",
                    description = "testowe"
                };
                App.Database.SaveUser(user);
            }
            catch(System.TypeInitializationException a)
            {
                DisplayAlert("Error", a.ToString(), "0", "1");
            }
        }
	}
}
