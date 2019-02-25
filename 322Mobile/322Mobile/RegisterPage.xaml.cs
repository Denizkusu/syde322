using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace _322Mobile
{
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
            InitializeComponent();

		}

        void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {
            (temp).Text = username.Text + password.Text+email.Text;
        }
    }   
}