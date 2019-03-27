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
  public partial class HomePage : ContentPage
  {
    public HomePage()
    {
      InitializeComponent();

    }
    void OnProfileButtonClicked(object sender, System.EventArgs e)
    {
      //(temp).Text = username.Text + password.Text+email.Text;
    }
    async void OnSearchCompletedAsync(object sender, System.EventArgs e)
    {
      //DisplayAlert("Alert", "You have been alerted", "OK");


      await Navigation.PushAsync(new SearchResultsPage(searchText.Text));
      searchText.Text = string.Empty;

      //(temp).Text = username.Text + password.Text+email.Text;
    }
  }
}
