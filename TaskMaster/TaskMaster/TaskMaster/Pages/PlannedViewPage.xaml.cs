using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.Lists;
using TaskMaster.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlannedViewPage
	{
        private List<PlannedList> _plannedList = new List<PlannedList>();
		public PlannedViewPage ()
		{
			InitializeComponent ();
		}

	    
	}
}