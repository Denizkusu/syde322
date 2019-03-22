using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Http;
using System.Net;

namespace _322Mobile
{
  public partial class RegisterPage : ContentPage
  {
    private static HttpClient client;
    private static bool emailCondition;
    private static bool initialEntry;
    private static bool passMatchCondition;
    private static bool validPassCondition;
    private static bool initialEntry2; 
    public RegisterPage()
    {
      client = new HttpClient();
      emailCondition = false;
      initialEntry = true;
      initialEntry2 = true;
      passMatchCondition = false;
      validPassCondition = false; 
      InitializeComponent();


    }

    async void OnRegisterButtonClicked(object sender, System.EventArgs e)
    {
      //(temp).Text = username.Text + password.Text+email.Text;
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
        var response = await client.PostAsync("https://ehl.me/api/user", content);
        var responseString = await response.Content.ReadAsStringAsync();
        Navigation.InsertPageBefore(new HomePage(), Navigation.NavigationStack[0]);

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

        await Navigation.PushAsync(new HomePage());

      }

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

      if ((!rx.IsMatch(email.Text) && email.Text != "") || email.Text == null || email.Text == "")
      {
        emailCondition = false;
        button1.IsEnabled = false;
        emailValidation.Text = "You must enter a valid email address";
      }
      else
      {
        emailValidation.Text = "";
        emailCondition = true;
        if (passMatchCondition == true && validPassCondition == true)
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
        if (passMatchCondition == true)
        {
          button1.IsEnabled = true;
        }

      }

    }


    void Handle_Unfocused_Password(object sender, Xamarin.Forms.FocusEventArgs e)
    {
      validatePassword();
      initialEntry2 = false;
      passMatchCheck(); 
    }

    void validatePassword()
    {
      //min 8 char max 24, 1 special character, 1 number, 1 upper and 1 lowercase
      Regex rx = new Regex(@"(?=^.{8,24}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      if (!rx.IsMatch(password.Text) && password.Text != "")
      {
        passwordValidation.Text = "Your password must be between 8-24 characters and contain 1 number, 1 special character and 1 upper and lower case letter";
        validPassCondition = false; 
      }
      else
      {
        validPassCondition = true; 
        passwordValidation.Text = "";
      }
    }

    void validatePasswordSoft()
    {
      //min 8 char max 24, 1 special character, 1 number, 1 upper and 1 lowercase
      Regex rx = new Regex(@"(?=^.{8,24}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      if (!rx.IsMatch(password.Text) && password.Text != "")
      {
        validPassCondition = false;
      }
      else
      {
        validPassCondition = true;
      }
    }


    void Handle_Unfocused_Password_Confirmation(object sender, Xamarin.Forms.FocusEventArgs e)
    {
      if (!passMatchCondition)
      {
        passwordConfirmationValidation.Text = "Passwords do not match";
        button1.IsEnabled = false; 
      }
      else
      {
        passwordConfirmationValidation.Text = "";
      }

    }

    void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
    {
      if (initialEntry == false)
      {
        validateEmail();

      }
      else
      {
        validateEmailSoft();
      }
    }

    void passMatchCheck()
    {


        if (password.Text == passwordConfirm.Text)
        {
          passMatchCondition = true;
          passwordConfirmationValidation.Text = "";
          if (passMatchCondition && emailCondition && validPassCondition)
          {
            button1.IsEnabled = true;
          }
          else
          {
            button1.IsEnabled = false; 
          }

        }
        else 
        {
          passMatchCondition = false;
          passwordConfirmationValidation.Text = "Passwords do not match";
          button1.IsEnabled = false; 
          
        }


    }


    void Handle_TextChanged_1(object sender, Xamarin.Forms.TextChangedEventArgs e)
    {
      //if (!initialEntry2)
      //{
      //  passMatchCheck();
      //  }
      //else
      //{ valid 
      //}
      validatePasswordSoft();
      passMatchCheck(); 


    }

    void Handle_TextChanged_2(object sender, Xamarin.Forms.TextChangedEventArgs e)
    {
      passMatchCheck();
    }
  }
}