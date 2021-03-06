﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace _322Mobile
{
  public partial class LoginPage : ContentPage
  {
    private static HttpClient client;
    private static bool emailCondition;
    private static bool initialEntry;
    public LoginPage()
    {
      client = new HttpClient();
      emailCondition = false;
      initialEntry = true;
      BackgroundColor.Equals("#FFF");
      InitializeComponent();

    }

    void Handle_Unfocused_Email(object sender, Xamarin.Forms.FocusEventArgs e)
    {
      validateEmail();
      initialEntry = false;
    }

    void validateEmail()
    {
      // Ensure email is of x@y.z
      Regex rx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
      RegexOptions.Compiled | RegexOptions.IgnoreCase);

      
      if (email.Text == null || (!rx.IsMatch(email.Text) && email.Text != "") || email.Text == "")
      {
        emailCondition = false;
        button1.IsEnabled = false;
        emailValidation.Text = "You must enter a valid email address";
      }
      else
      {
        emailValidation.Text = "";
        emailCondition = true;
        if (password.Text != null && password.Text != "")
        {
         
          button1.IsEnabled = true;
        }

      }

    }

    void validateEmailSoft()
    {
      // Ensure email is of x@y.z
      Regex rx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
      RegexOptions.Compiled | RegexOptions.IgnoreCase);

      if ((!rx.IsMatch(email.Text) && email.Text != "") || email.Text == null || email.Text == "")
      {
        emailCondition = false;
        button1.IsEnabled = false;
      }
      else
      {
        emailCondition = true;
        if (password.Text != null && password.Text != "")
        {
          button1.IsEnabled = true;
        }

      }

    }


    void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
    {
      if (error.Text != "")
      {
        error.Text = "";
      }

      if (password.Text != null && password.Text != "")
      {
        if (emailCondition == true)
        {
          button1.IsEnabled = true;
        }
      }
      else
      {
        button1.IsEnabled = false;
      }
    }

    void Handle_TextChanged_1(object sender, Xamarin.Forms.TextChangedEventArgs e)
    {

      if (error.Text != "")
      {
        error.Text = "";
      }

      if (initialEntry == false)
      {
        validateEmail();

      }
      else
      {
        validateEmailSoft();
      }
    }

    async void OnLoginButtonClicked(object sender, System.EventArgs e)
    {

      Image loader = new Image { Source = "loading.png", HorizontalOptions = LayoutOptions.Center, HeightRequest = 100 };

      error.Text = "";
      var values = new Dictionary<string, string>
            {
               { "Username", email.Text },
               { "Password", password.Text }
            };


      var jsonC = JsonConvert.SerializeObject(values);
      var content = new StringContent(jsonC, Encoding.UTF8, "application/json");

      try
      {
        stackLayout.Children.Add(loader);
        await loader.RotateTo(360, 200);
        loader.RotateTo(18000, 10000);

        var response = await client.PostAsync("https://ehl.me/api/login", content);
        var responseString = await response.Content.ReadAsStringAsync();
        loader.IsVisible = false; 
        if (!response.IsSuccessStatusCode)
        {
          error.Text = "Invalid Credentials";
          button1.IsEnabled = false;
        }
        else
        {
          //App.Token = responseString;
          Application.Current.Properties["useremail"] = email.Text;
          Application.Current.Properties["oauth-token"] = responseString;
          await Application.Current.SavePropertiesAsync(); 


          Application.Current.MainPage = new MasterPageNavigation();


        }



      }
      catch (WebException ex)
      {
        stackLayout.Children.Add(loader);
        await loader.RotateTo(360, 200);
        loader.IsVisible = false;
        if (ex.Response is HttpWebResponse)
        {
          var httpResponse = ex.Response as HttpWebResponse;
          error.Text = httpResponse.StatusDescription;
        }
        else
        {
          error.Text = ex.Message;
        }

      }
    }
  }
}