using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace _322Mobile
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      //MainPage = new _322Mobile.MasterPageNavigation();
      MainPage = new NavigationPage(new ProductPage("abc"))
      {
        BarBackgroundColor = Color.FromHex("1F2631"),
      };



    }

    protected override void OnStart()
    {
      // Handle when your app starts
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
