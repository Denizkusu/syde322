using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _322Mobile
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            BackgroundColor.Equals("#FFF");
            InitializeComponent();

        }

        void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            (temp).Text = username.Text + password.Text;
        }
    }
}
