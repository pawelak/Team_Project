using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
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
            var task = new Tasks()
            {
                name = ActivityName.Text              
            };
            
	        DisplayAlert("Tytuł", ActivityName.Text, "OK", "No");
	    }
	}
}
