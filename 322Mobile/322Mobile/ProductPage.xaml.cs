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



      //StackLayout categoryContainer = new StackLayout { Spacing=10, 
      //Margin = new Thickness(20,20,20,0) }

      Label prodName = new Label { Text = productIds,
        TextColor = Color.FromHex("#fff"),
        FontSize = 25, HorizontalOptions=LayoutOptions.Center
      };

      Image img = new Image { Source = "xr.png", HeightRequest = 200, HorizontalOptions=LayoutOptions.Center};

      StackLayout a1 = new StackLayout { };
      Label catName = new Label
      {
        Text = "cat1",
        TextColor = Color.FromHex("#fff"),
        FontSize = 25,
        HorizontalOptions = LayoutOptions.Start
      };

      StackLayout a1Reviews = new StackLayout { Orientation = StackOrientation.Vertical };

      a1.Children.Add(catName);

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

      for (int i = 0; i < 100; i++)
      {
        a1Reviews.Children.Add(new Button { Text = "Button " + i });
      }

      ScrollView cat1scroll = new ScrollView {  Content = a1Reviews };







      //a1.Children.Add(a1Reviews);

      reviewStack.Children.Add(a1Reviews);
      //a1.


      reviewStack.Children.Add(prodName);
      reviewStack.Children.Add(img);
      reviewStack.Children.Add(a1); 
      


    }

    void OnProfileButtonClicked(object sender, System.EventArgs e)
    {
      //(temp).Text = username.Text + password.Text+email.Text;
    }
    async void OnSearchCompletedAsync(object sender, System.EventArgs e)
    {

      reviewStack.IsVisible = false;
      //DisplayAlert("Alert", "You have been alerted", "OK");

      //(temp).Text = username.Text + password.Text+email.Text;
    }


  }
}
