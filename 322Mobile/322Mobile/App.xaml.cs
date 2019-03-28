using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace _322Mobile
{
  public partial class App : Application
  {


    public App()
    {




    }

    protected override void OnStart()
    {

      if (Application.Current.Properties.ContainsKey("oauth-token") && Application.Current.Properties["oauth-token"] != null) {
        MainPage = new _322Mobile.MasterPageNavigation();
      }
      else {
        MainPage = new NavigationPage(new StartPage())
        {
          BarBackgroundColor = Color.FromHex("1F2631")
        };
      }



    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
