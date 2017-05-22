using System;
using TaskMaster.Pages;
using TaskMaster.Services;
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

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            });
            return base.OnBackButtonPressed();
        }

        private async void PlannedPageItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PlannedViewPage()));
        }

        private void SyncItem_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void LogoutItem_OnClicked(object sender, EventArgs e)
        {
            await UserService.Instance.LogoutUser();
            // tu musi być wyjście z apki
        }

    }
}

