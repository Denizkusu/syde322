using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace _322Mobile
{
  public partial class LoginPage : ContentPage
  {
    private static HttpClient client;
    public LoginPage()
    {
      client = new HttpClient();
      BackgroundColor.Equals("#FFF");
      InitializeComponent();

    }

    void Handle_Unfocused_Email(object sender, Xamarin.Forms.FocusEventArgs e)
    {
      // Ensure email is of x@y.z
      Regex rx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
    RegexOptions.Compiled | RegexOptions.IgnoreCase);

      if (!rx.IsMatch(email.Text) && email.Text != "")
      {
        emailValidation.Text = "You must enter a valid email address";
      }
      else
      {
        emailValidation.Text = "";
      }
    }

    async void OnLoginButtonClicked(object sender, System.EventArgs e)
    {
      error.Text = "";
      var values = new Dictionary<string, string>
            {
               { "Username", email.Text },
               { "Password", password.Text }
            };


      var content = new FormUrlEncodedContent(values);

      try
      {
        var response = await client.PostAsync("https://localhost:5001/api/login", content);
        var responseString = await response.Content.ReadAsStringAsync();
        Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack[0]);
        await Navigation.PopToRootAsync();

      }
      catch (WebException ex)
      {

        if (ex.Response is HttpWebResponse)
        {
          var httpResponse = ex.Response as HttpWebResponse;
          error.Text = httpResponse.StatusDescription;
        }
        else
        {
          error.Text = ex.Message;
        }

        await Navigation.PushAsync(new MainPage());

      }
    }
  }
}
