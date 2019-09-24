using MySpectrum.Helper;
using MySpectrum.View;
using MySpectrum.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MySpectrum
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
        }

        public App(IUserRepository usersRepository)
        {
            InitializeComponent();

            var usersPage = new UsersPage()
            {
                BindingContext = new UsersViewModel(usersRepository)
            };

            MainPage = new NavigationPage(usersPage);
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
