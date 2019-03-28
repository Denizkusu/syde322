using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using _322Mobile.Models;


namespace _322Mobile
{
  public partial class SearchResultsPage : ContentPage
  {

    public string SearchString { get; set; }
    public string SearchStringDisplay; 
    private static Phone[] _phones;
    private static HttpClient client;

    public SearchResultsPage(string searchString)
    {
      InitializeComponent();
      client = new HttpClient();
      client.DefaultRequestHeaders.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Application.Current.Properties["oauth-token"] as string);
      SearchString = searchString;
      SearchStringDisplay = searchString; 
      initialLoad(); 

    }

    private async void initialLoad()
    {
      _phones = await SearchPhones();
      generateElements();
      searchText.Text = null; 
    }


    private async Task<Phone[]> SearchPhones()
    {
      Image loader = new Image { Source = "loading.png", HorizontalOptions = LayoutOptions.Center, HeightRequest = 100 };
      if (SearchString != "")
      {
        SearchString = Uri.EscapeUriString(SearchString);
      }
      else
      {
        return null;
      }
      string HttpGetUrl = String.Format("https://ehl.me/api/phone?phoneName={0}", SearchString);
      try
      {


        grid.Children.Add(loader, 0,1);
        Grid.SetColumnSpan(loader, 2);
        await loader.RotateTo(360, 200);
        loader.RotateTo(18000, 10000);
        var response = await client.GetAsync(HttpGetUrl);
        loader.IsVisible = false; 



        if (!response.IsSuccessStatusCode)
        {
        
          Label noResultsText = new Label { Text =  String.Format("Status Code: {0} {1}",  (int) response.StatusCode, response.StatusCode.ToString()), TextColor = Color.White };
          grid.Children.Add(noResultsText, 0, 0);
          Grid.SetColumnSpan(noResultsText, 2);
          return null; 
        }
        else
        {
          //Marshall into Phone objectq
          var responseString = await response.Content.ReadAsStringAsync();


          Phone[] phones = JsonConvert.DeserializeObject<Phone[]>(responseString);
          return phones;
        }
      }
      catch (WebException ex)
      {
        grid.Children.Add(loader, 0, 1);
        Grid.SetColumnSpan(loader, 2);
        await loader.RotateTo(360, 200);
        loader.IsVisible = false; 
        if (ex.Response is HttpWebResponse)
        {
          var httpResponse = ex.Response as HttpWebResponse;
          //error.Text = httpResponse.StatusDescription;
        }
        else
        {

          Label noResultsText = new Label { Text = ex.Message , TextColor = Color.White };
          grid.Children.Add(noResultsText, 0, 0);
          Grid.SetColumnSpan(noResultsText, 2);
          return null; 
          //error.Text = ex.Message;
        }
        return null;
      }


    }

    void generateElements()
    {
      if (_phones != null)
      {
        if (_phones.Length == 0)
        {

          //No results
          Label noResultsText = new Label { Text = String.Format("No Results Found for: {0}", SearchStringDisplay), TextColor = Color.White };
          grid.Children.Add(noResultsText, 0, 0);
          Grid.SetColumnSpan(noResultsText, 2);
        }
        else
        {
          for (int i = 0; i < _phones.Length; i++)
          {
            StackLayout contentData = new StackLayout
            {
              BackgroundColor = Color.FromHex("#00aced"),
              Padding = new Thickness(10, 10, 10, 10),
              HeightRequest = 150

            };



            contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 20, Text = _phones[i].Name.ToUpper() });
            contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 18, Text = "Overall Score: " + _phones[i].Score });
            contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 18, Text = "Price: $" + _phones[i].Price });

            Image imageData = new Image { Source = "xr.png", HeightRequest = 150 };

            imageData.ClassId = _phones[i].Id.ToString();
            contentData.ClassId = _phones[i].Id.ToString();
            var tempStore = _phones[i];

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            { 
              Navigation.PushAsync(new ProductPage(tempStore));
            };

            contentData.GestureRecognizers.Add(tapGestureRecognizer);
            imageData.GestureRecognizers.Add(tapGestureRecognizer);

            grid.Children.Add(contentData, 0 + (i % 2), i);
            grid.Children.Add(imageData, 1 - (i % 2), i);
          }

        }


      }
    }



    void OnSearchCompletedAsync(object sender, System.EventArgs e)
    {
      grid.Children.Clear();
      SearchString = searchText.Text;
      SearchStringDisplay = searchText.Text; 
      initialLoad(); 


      //await Navigation.PushAsync(new SearchResultsPage(searchText.Text));
      //Navigation.RemovePage(Navigation.NavigationStack[1]);


    }

  }


}
