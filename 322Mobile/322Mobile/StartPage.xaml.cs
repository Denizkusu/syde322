using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _322Mobile
{
  public partial class StartPage : ContentPage
  {
    public StartPage()
    {
      InitializeComponent();

    }

    async void OnLoginButtonClicked(object sender, System.EventArgs e)
    {

      await Navigation.PushAsync(new NavigationPage(new LoginPage())
      {
        BarBackgroundColor = Color.FromHex("1F2631")
      });

    }

    async void OnRegisterButtonClicked(object sender, System.EventArgs e)
    {
      await Navigation.PushAsync(new NavigationPage(new RegisterPage())
      {
        BarBackgroundColor = Color.FromHex("1F2631")
      });
    }

  }
}
