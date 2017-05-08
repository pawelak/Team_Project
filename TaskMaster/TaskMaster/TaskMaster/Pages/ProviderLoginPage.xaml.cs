using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProviderLoginPage
	{
        public string ProviderName { get; set; }
		public ProviderLoginPage (string providerName)
		{
			InitializeComponent ();
		    ProviderName = providerName;
		}
	}
}
