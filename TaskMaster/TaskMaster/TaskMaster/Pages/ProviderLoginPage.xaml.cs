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
	public partial class ProviderLoginPage : ContentPage
	{
        public string ProviderName { get; set; }
		public ProviderLoginPage (string providerName)
		{
			InitializeComponent ();
		    ProviderName = providerName;
		}
	}
}
