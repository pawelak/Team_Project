using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage
    {

        public CalendarPage()
        {
            InitializeComponent();
        }

        private async void MainPageItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        private async void HistoryPageItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new HistoryPage()));
        }
    }
}

