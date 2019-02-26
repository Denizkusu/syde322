using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Controls;

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
            //(temp).Text = username.Text + password.Text+email.Text;
        }

        void Handle_Unfocused_Email(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            // Ensure email is of x@y.z
            Regex rx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!rx.IsMatch(email.Text))
            {
                emailValidation.Text = "You must enter a valid email address";
            }
            else
            {
                emailValidation.Text = "";
            }
        }

        void Handle_Unfocused_Password(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //min 8 char max 24, 1 special character, 1 number, 1 upper and 1 lowercase
            Regex rx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,24}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!rx.IsMatch(password.Text))
            {
                passwordValidation.Text = "Your password must be between 8-24 characters and contain 1 number, 1 special character and 1 upper and lower case letter";
            }
            else
            {
                passwordValidation.Text = "";
            }
        }

        void Handle_Unfocused_Password_Confirmation(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (password.Text != passwordConfirm.Text)
            {
                passwordConfirmationValidation.Text = "Passwords do not match";
            }
            else
            {
                passwordConfirmationValidation.Text = "";
            }

        }
    }
}