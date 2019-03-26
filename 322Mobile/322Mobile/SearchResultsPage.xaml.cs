using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace _322Mobile
{
    public partial class SearchResultsPage : ContentPage
    {
    private static string[] phoneArray;
    private static string[] phonePriceArray;
    private static string[] phoneScoreArray;
    private static string[] pidar;

    public SearchResultsPage(string searchString)
        {
            InitializeComponent();

            makePostRequest(searchString); 
            
        }


        void makePostRequest(string searchString)
        {


          //var numOfResults = 3;
          //phoneArray = new string[numOfResults]; 
          
          phoneArray = new string[] { "abcd", "efgh", "ijkl", "hijk", "lmno" };
      phonePriceArray = new string[] { "12.2", "18.8", "92.4", "21.9", "18.3" };
      phoneScoreArray = new string[] { "3", "4", "9" , "2.2", "90"};
      pidar = new string[] { "a3", "s4", "d9", "f2.2", "g90" };

      generateElements(); 

    }

    void generateElements()
    { 
      if (phoneArray.Length == 0) { 
      
      
      }
      else 
      { 
        for (int i = 0; i< phoneArray.Length; i++) {
          StackLayout contentData = new StackLayout {
            BackgroundColor = Color.FromHex("#00aced"),
            Padding = new Thickness(10, 10, 10, 10),
            HeightRequest = 150
            
          };



          contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 20, Text = phoneArray[i] });
          contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 18, Text = "Overall Score: " + phoneScoreArray[i] });
          contentData.Children.Add(new Label { TextColor = Color.FromHex("#fff"), FontSize = 18, Text = "Price: $" + phonePriceArray[i] });
          //Image imageData = new Image
          //{
          //  Source = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"))
          //};
          Image imageData = new Image { Source = "xr.png", HeightRequest = 150 };

          imageData.ClassId = pidar[i];
          contentData.ClassId = pidar[i];

          var tapGestureRecognizer = new TapGestureRecognizer();
          tapGestureRecognizer.Tapped += (s, e) => {
            var parm = ((Image)s).ClassId;
            Navigation.PushAsync(new ProductPage(parm));
          };

          var tapGestureRecognizer2 = new TapGestureRecognizer();
          tapGestureRecognizer2.Tapped += (zo, ed) => {
            var parm = ((StackLayout)zo).ClassId;
            Navigation.PushAsync(new ProductPage(parm));
          };

          contentData.GestureRecognizers.Add(tapGestureRecognizer2);
          imageData.GestureRecognizers.Add(tapGestureRecognizer);

          grid.Children.Add(contentData, 0+(i%2), i);
          grid.Children.Add(imageData, 1-(i%2), i); 
        }




      
      }




    }

    private async void tgr1(object sender, EventArgs e)
    {
      var parm = ((StackLayout)sender).ClassId;
      await Navigation.PushAsync(new ProductPage(parm));

    }


    void OnProfileButtonClicked(object sender, System.EventArgs e)
        {
            //(temp).Text = username.Text + password.Text+email.Text;

        }

        void ForgotPasswordCommand(object sender, System.EventArgs e)
        {
            //(temp).Text = username.Text + password.Text+email.Text;
            DisplayAlert("Alert", "You have been alerted", "OK");

        }


        async void OnSearchCompletedAsync(object sender, System.EventArgs e)
        {
            //DisplayAlert("Alert", "You have been alerted", "OK");

            await Navigation.PushAsync(new SearchResultsPage(searchText.Text));
            Navigation.RemovePage(Navigation.NavigationStack[1]);
            //Navigation.InsertPageBefore(new HomePage(), this);

      //(temp).Text = username.Text + password.Text+email.Text;
    }
        void OnButC(object sender, System.EventArgs e)
        {
            //(temp).Text = username.Text + password.Text+email.Text;
        }
    }


}
