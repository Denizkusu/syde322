using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace _322Mobile
{
    public partial class SearchResultsPage : ContentPage
    {
        public SearchResultsPage()
        {
            InitializeComponent();
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
            await Navigation.PushAsync(new SearchResultsPage());
            //(temp).Text = username.Text + password.Text+email.Text;
        }
        void OnButC(object sender, System.EventArgs e)
        {
            //(temp).Text = username.Text + password.Text+email.Text;
        }
    }


}
