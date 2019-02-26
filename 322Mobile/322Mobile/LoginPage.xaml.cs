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
    public partial class LoginPage : ContentPage
    {
        private static HttpClient client;
        public LoginPage()
        {
            client = new HttpClient();
            BackgroundColor.Equals("#FFF");
            InitializeComponent();

        }

        async void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            error.Text = "";
            var values = new Dictionary<string, string>
            {
               { "Username", username.Text },
               { "Password", password.Text }
            };

            var content = new FormUrlEncodedContent(values);

            try
            {
                var response = await client.PostAsync("https://localhost:5001/api/login", content);
                var responseString = await response.Content.ReadAsStringAsync();
                Navigation.InsertPageBefore(new HomePage(), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    var httpResponse = ex.Response as HttpWebResponse;
                    error.Text =httpResponse.StatusDescription;
                }
                else
                {
                    error.Text = ex.Message;
                }
            }
        }
    }
}
