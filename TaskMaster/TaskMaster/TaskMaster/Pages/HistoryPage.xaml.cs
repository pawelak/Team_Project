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
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent();
		}

	    private async void MainPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new MainPage());
	    }

	    private async void CalendarPageItem_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new CalendarPage());
	    }
	}
}
