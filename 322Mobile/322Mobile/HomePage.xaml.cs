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

    protected override async void  OnAppearing()
    {
      User user = await GetUser();
      if (!(user is null))
      {
        this.User = user;
        generateElements(); 

      }



      //TODO: dynamically make elements for each string in history and have them link to search results page 


    }

    void generateElements()
    {
      if (User.History != null)
      {
        if (User.History.Length == 0)
        {

          //No results
          Label noResultsText = new Label { Text = "No History Yet", TextColor = Color.White };
          grid.Children.Add(noResultsText, 0, 0);
          Grid.SetColumnSpan(noResultsText, 2);
        }
        else
        {
          for (int i = 0; i < User.History.Length; i++)
          {
            StackLayout contentData = new StackLayout
            {
              BackgroundColor = Color.FromHex("#00aced"),
              Padding = new Thickness(10, 10, 10, 10),
              HeightRequest = 50

            };



            contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 20, Text = User.History[i].ToUpper() });
            var tempStore = User.History[i]; 

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
              Navigation.PushAsync(new SearchResultsPage(tempStore));
            };

            contentData.GestureRecognizers.Add(tapGestureRecognizer);


            grid.Children.Add(contentData, 0, i);

          }

        }


      }
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
