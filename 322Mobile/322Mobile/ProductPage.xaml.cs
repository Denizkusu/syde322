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
  public partial class ProductPage : ContentPage
  {
    private static string productIds; 
    public ProductPage(string productId)
    {
      
      InitializeComponent();
      productIds = productId; 
      createElements(); 


    }

    void createElements()
    {
      //< StackLayout Spacing = "10" Margin = "20,20,20,0" x: Name = "mainStack" >

      //< Label x: Name = "productName" TextColor = "#ffffff" FontSize = "25" Text = "ProductName" >

      //  </ Label >
      //</ StackLayout >

      string [] categories =  { "abc", "def", "ghi"};



      //StackLayout categoryContainer = new StackLayout { Spacing=10, 
      //Margin = new Thickness(20,20,20,0) }

      Label prodName = new Label { Text = productIds,
        TextColor = Color.FromHex("#fff"),
        FontSize = 25, HorizontalOptions=LayoutOptions.Center
      };

      Image img = new Image { Source = "xr.png", HeightRequest = 200, HorizontalOptions=LayoutOptions.Center};

      reviewStack.Children.Add(prodName);
      reviewStack.Children.Add(img);


      foreach (string x in categories) {
        //var catContainer = new StackLayout { Margin = new Thickness(20, 20, 20, 0) };
        var catContainer = new Grid { Margin = new Thickness(20, 0, 20, 0), HorizontalOptions = LayoutOptions.FillAndExpand};

        catContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
        catContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
        catContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        catContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        Label catName = new Label
        {
          Text = x,
          TextColor = Color.FromHex("#fff"),
          FontSize = 25,
          HorizontalOptions = LayoutOptions.Start
        };


        var reviewScroll = new ScrollView { Orientation = ScrollOrientation.Horizontal, HorizontalScrollBarVisibility = ScrollBarVisibility.Never };

        Label catDown = new Label
        {
          Text = "▼",
          TextColor = Color.FromHex("#fff"),
          FontSize = 25,
          HorizontalOptions = LayoutOptions.End
        };

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (s, e) => {

          if (reviewScroll.IsVisible)
          {
            reviewScroll.IsVisible = false;
          }
          else { reviewScroll.IsVisible = true; }


        };

        BoxView box = new BoxView {BackgroundColor=Color.Transparent, ClassId= x}; 

        box.GestureRecognizers.Add(tapGestureRecognizer); 

        catContainer.Children.Add(catName, 0,0);
        catContainer.Children.Add(catDown, 1, 0);
        catContainer.Children.Add(box, 0, 0);


        catContainer.Children.Add(reviewScroll, 0,1);
        Grid.SetColumnSpan(reviewScroll, 2);
        Grid.SetColumnSpan(box, 2);

        StackLayout a1Review = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 20 };
        for (int i = 0; i < 10; i++)
        {
          Label reviewText = new Label { WidthRequest = 200, TextColor = Color.White, Margin=0 }; 
          reviewText.Text = "loremiheig wiufh eiuh gfiuehiugrh eiuhrgiueh iughieu hfiuahf iuwhfuiwhfiuwh ienfriunuiueufh ju rfjurh urhf uuf iurhiueuh fuiheurh ufhiueh rfiuheufiheiuh iurf iueuhriufheiurufhuehu uhr urhf uhruf hrfuh fiuhwiefjiojijij iejf iej fijfiej fjij ie fi fjiej fij foiejwoiej fwiuh giwehiehrgiuehiugheiurgh iure ";  

          a1Review.Children.Add(reviewText);
        }

        reviewScroll.Content = a1Review;
        if (x != "abc")

        { 
        reviewScroll.IsVisible = false; 
        }




        reviewStack.Children.Add(catContainer);
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
