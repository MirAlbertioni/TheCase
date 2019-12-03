using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fac.Services;
using Fac.Views;
using Fac.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Fac
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //public static string AzureBackendUrl = "http://localhost:5000";
        public static string AzureBackendUrl = "http://192.168.1.110:61257";

        public App()
        {
            InitializeComponent();

            Locator.Instance.Build();

            DependencyService.Register<AzureDataStore>();

            //var tabbedPage = new TabbedPage();
            //tabbedPage.Children.Add(new CasesPage());
            //tabbedPage.Children.Add(new UnreadCasesPage());
            ////tabbedPage.Children.Add(new NewCasePage());

            //MainPage = new TabbedPage();
            //MainPage = tabbedPage;

            MainPage = new MainPage();
            //MainPage = tabbedPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MessagingCenter.Subscribe<User>(this, "UserEmail", async user =>
            {
                var x = user.Email;
            });
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
