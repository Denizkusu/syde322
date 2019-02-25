using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _322Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor.Equals("#FFF");
            InitializeComponent();

        }

        async void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        async void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

    }
}
