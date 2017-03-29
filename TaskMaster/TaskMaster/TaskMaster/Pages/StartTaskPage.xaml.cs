using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

	    }
	}
}
