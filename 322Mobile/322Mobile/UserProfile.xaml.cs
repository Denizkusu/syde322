using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Xamarin.Forms;


namespace _322Mobile
{
  public partial class UserProfile : ContentPage
  {
    public UserProfile()
    {
      InitializeComponent();
      useremail.Text = Application.Current.Properties["useremail"] as string; 

    }
    async void OnProfileButtonClicked(object sender, System.EventArgs e)
    {
      //(temp).Text = username.Text + password.Text+email.Text;
      //DisplayAlert("yeet", "yeet", "yeet");


      Application.Current.MainPage = new NavigationPage(new StartPage())
      {
        BarBackgroundColor = Color.FromHex("1F2631")
      };

      await Navigation.PopToRootAsync();
      Application.Current.Properties["useremail"] = null;
      Application.Current.Properties["oauth-token"] = null;
      await Application.Current.SavePropertiesAsync(); 


    }

    async void OnPhonesButtonClicked(object sender, System.EventArgs e)
    {
      Application.Current.MainPage = new NavigationPage(new StartPage())
      {
        BarBackgroundColor = Color.FromHex("1F2631")
      };

      await Navigation.PopToRootAsync();
      Application.Current.Properties["useremail"] = null;
      Application.Current.Properties["oauth-token"] = null;
      await Application.Current.SavePropertiesAsync();


    }

  }
}
