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

    }
    async void OnProfileButtonClicked(object sender, System.EventArgs e)
    {
      //(temp).Text = username.Text + password.Text+email.Text;
      //DisplayAlert("yeet", "yeet", "yeet");
      Navigation.InsertPageBefore(new StartPage(), Navigation.NavigationStack[0]);
      await Navigation.PopToRootAsync(); 

      //await Navigation.PushAsync(new StartPage());

      //foreach (Page nav in Navigation.NavigationStack)
      //{
      //  Navigation.RemovePage(nav);
      //}

    }

  }
}
