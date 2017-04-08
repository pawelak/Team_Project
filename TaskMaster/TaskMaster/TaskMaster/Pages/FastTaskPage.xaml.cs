using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Models;

namespace TaskMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FastTaskPage : ContentPage
	{
		public FastTaskPage ()
		{
			InitializeComponent ();
		}

	    private void FastTaskStartButton_OnClicked(object sender, EventArgs e)
	    {
	        var activity = new Activities();
	        activity.activityId = App.Database.SaveActivity(activity).Result;
	    }
	}
}
