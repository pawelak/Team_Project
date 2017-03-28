using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanTaskPage : ContentPage
	{
		public PlanTaskPage ()
		{
			InitializeComponent ();
		}

	    private void PlanTaskStartButton_OnClicked(object sender, EventArgs e)
        {
	        DisplayAlert("Tytuł", ActivityName.Text, "OK", "No");
	    }
	}
}
