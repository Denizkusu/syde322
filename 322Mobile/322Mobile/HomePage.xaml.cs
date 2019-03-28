using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Xamarin.Forms;
using _322Mobile.Models;
using Newtonsoft.Json;


namespace _322Mobile
{
  public partial class HomePage : ContentPage
  {
    public User User { get; set; }
    public HttpClient client { get; set; }

    public HomePage()
    {
      client = new HttpClient();
      client.DefaultRequestHeaders.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Application.Current.Properties["oauth-token"] as string);
      InitializeComponent();
    }

    protected virtual async void OnAppearing()
    {
      User user = await GetUser();
      if (!(user is null))
      {
        this.User = user;
      }

      //TODO: dynamically make elements for each string in history and have them link to search results page 
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

    public async Task<User> GetUser()
    {
      string HttpGetUrl = String.Format("https://ehl.me/api/user/whoami");
      try
      {
        HttpResponseMessage response = await client.GetAsync(HttpGetUrl);
        if (!response.IsSuccessStatusCode)
        {
          //TODO: display error
        }
        string res = await response.Content.ReadAsStringAsync();
        User user = JsonConvert.DeserializeObject<User>(res);
        return user;
      }
      catch
      {
        //TODO: put in an error if something is wrong
        return null;
      }
    }

  }
}
