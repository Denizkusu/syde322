using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using _322Mobile.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace _322Mobile
{
  public partial class ProductPage : ContentPage
  {
    private static Phone phoneIds;
    private static PhoneReview[] _reviews;
    private static HttpClient client;
    public ProductPage(Phone phoneId)
    {
      
      InitializeComponent();
      phoneIds = phoneId;
      client = new HttpClient();
      client.DefaultRequestHeaders.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Application.Current.Properties["oauth-token"] as string);
      initialLoad();

    }

    private async void initialLoad()
    {
      _reviews = await extractReviews();
      createElements();
    }

    private async Task<PhoneReview[]> extractReviews()
    {
      if (phoneIds.Name != "")
      {
        //phoneIds = Uri.EscapeUriString(phoneIds.Id.ToString());
      }
      else
      {
        return null;
      }
      string HttpGetUrl = String.Format("https://ehl.me/api/review/{0}", phoneIds.Id);
      try
      {
        Image loader = new Image { Source = "loading.png", HorizontalOptions = LayoutOptions.Center, HeightRequest = 100 };

        reviewStack.Children.Add(loader);
        await loader.RotateTo(360, 200);
        loader.RotateTo(18000, 10000);
        var response = await client.GetAsync(HttpGetUrl);
        loader.IsVisible = false;



        if (!response.IsSuccessStatusCode)
        {

          Label noResultsText = new Label { Text = String.Format("Status Code: {0} {1}", (int)response.StatusCode, response.StatusCode.ToString()), TextColor = Color.White };
          reviewStack.Children.Add(noResultsText);
          return null;
        }
        else
        {
          //Marshall into Phone objectq
          var responseString = await response.Content.ReadAsStringAsync();

          PhoneReview[] reviews = JsonConvert.DeserializeObject<PhoneReview[]>(responseString);
          return reviews;
        }
      }
      catch (WebException ex)
      {

        if (ex.Response is HttpWebResponse)
        {
          var httpResponse = ex.Response as HttpWebResponse;
        }
        else
        {

          Label noResultsText = new Label { Text = "Malformed Response Error. Please try again.", TextColor = Color.White };
          reviewStack.Children.Add(noResultsText);
          return null;
        }
        return null;
      }
    }





      void createElements()
    {
      if (_reviews != null) { 
        if (_reviews.Length != 0)
        {
        

        Label prodName = new Label { Text = phoneIds.Name.ToUpper(),
        TextColor = Color.FromHex("#fff"),
        FontSize = 25, HorizontalOptions=LayoutOptions.Center
      };

      Image img = new Image { Source = "xr.png", HeightRequest = 200, HorizontalOptions=LayoutOptions.Start};

      reviewStack.Children.Add(prodName);
      reviewStack.Children.Add(img);

          var reviewCategories = new Dictionary<string, List<PhoneReview>>();

          Random rnd = new Random();
          var temp = _reviews; 
          _reviews = temp.OrderBy(x => rnd.Next()).ToArray();

          foreach (PhoneReview z in _reviews)
          {
            if (reviewCategories.ContainsKey(z.Category))
            {
              reviewCategories[z.Category].Add(z); 
            }
            else
            {
              reviewCategories.Add(z.Category, new List<PhoneReview> { z });
            }


          }


          foreach (var item in reviewCategories)
          {
            //var catContainer = new StackLayout { Margin = new Thickness(20, 20, 20, 0) };
            var catContainer = new Grid { Margin = new Thickness(20, 0, 20, 0), HorizontalOptions = LayoutOptions.FillAndExpand };

            catContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            catContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            catContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            catContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            catContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label catName = new Label
            {
              Text =  char.ToUpper(item.Key[0]) + item.Key.Substring(1),
              TextColor = Color.FromHex("#fff"),
              FontSize = 25,
              HorizontalOptions = LayoutOptions.Start
            };


            var reviewScroll = new ScrollView { Orientation = ScrollOrientation.Horizontal, Margin = new Thickness(0,10,0,0) };

            Label catDown = new Label
            {
              Text = "▼",
              TextColor = Color.FromHex("#fff"),
              FontSize = 25,
              HorizontalOptions = LayoutOptions.End
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {

              if (reviewScroll.IsVisible)
              {
                reviewScroll.IsVisible = false;
              }
              else { reviewScroll.IsVisible = true; }


            };

            BoxView box = new BoxView { BackgroundColor = Color.Transparent };

            box.GestureRecognizers.Add(tapGestureRecognizer);

            BoxView underline = new BoxView { HeightRequest = 5 , Color = Color.White};

            catContainer.Children.Add(catName, 0, 0);
            catContainer.Children.Add(catDown, 1, 0);

            catContainer.Children.Add(box, 0, 0);
            catContainer.Children.Add(underline, 0, 1);
            Grid.SetColumnSpan(underline, 2); 


            catContainer.Children.Add(reviewScroll, 0, 2);
            Grid.SetColumnSpan(reviewScroll, 2);
            Grid.SetColumnSpan(box, 2);

            StackLayout a1Review = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 20 };

            foreach (PhoneReview reviewTextItem in item.Value)
            {
              //BoxView reviewBox = new BoxView { WidthRequest = 200, BackgroundColor = Color.White };

              StackLayout reviewItemStackSub = new StackLayout { }; 
              var actualText = reviewTextItem.ReviewText; 
              if (reviewTextItem.ReviewText.Length > 250) 
              {
                //truncate 
                actualText = reviewTextItem.ReviewText.Substring(0, 250) + "..."; 
              }


              Label reviewText = new Label { WidthRequest = 200, TextColor = Color.White, Margin = 0};

              //Editor reviewText = new Editor { WidthRequest = 200, TextColor = Color.Black, BackgroundColor = Color.White, IsReadOnly = true, HeightRequest = 150, FontSize= }; 

              reviewText.Text = actualText;
              //a1Review.Children.Add(reviewBox); 
              Image reviewSourceLogo = new Image { HeightRequest = 50, VerticalOptions = LayoutOptions.EndAndExpand };
              switch (reviewTextItem.SourceName)
              {
                case "TheVerge":
                  reviewSourceLogo.Source = "theverge.png"; 
                  break;
                case "TechRadar":
                  reviewSourceLogo.Source = "techradar.png";
                  break;
                case "Cnet":
                  reviewSourceLogo.Source = "cnet.png";
                  break;

                default:

                  break;
              }




              reviewItemStackSub.Children.Add(reviewText);
              reviewItemStackSub.Children.Add(reviewSourceLogo);

              a1Review.Children.Add(reviewItemStackSub);
            }

            reviewScroll.Content = a1Review;
            if (item.Key != "Overall")

            {
              reviewScroll.IsVisible = false;
            }




            reviewStack.Children.Add(catContainer);
          }

        }
      }












      //a1.Children.Add(catName);
      //StackLayout a1Reviews = new StackLayout();
      //a1.Children.Add(a1Reviews);


      //for (int i =0; i<20; i++) {
      //  Label cz = new Label
      //  {
      //    Text = "cat1",
      //    MinimumWidthRequest=50,

      //    TextColor = Color.FromHex("#fff"),
      //    FontSize = 25,

      //  };
      //  a1Reviews.Children.Add(cz);

      //}



       









     
      //a1.




      


    }

    void OnProfileButtonClicked(object sender, System.EventArgs e)
    {
      //(temp).Text = username.Text + password.Text+email.Text;
    }
    async void OnSearchCompletedAsync(object sender, System.EventArgs e)
    {

      //reviewStack.IsVisible = false;
      //DisplayAlert("Alert", "You have been alerted", "OK");

      //(temp).Text = username.Text + password.Text+email.Text;
    }


  }
}
