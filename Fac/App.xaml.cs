using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fac.Services;
using Fac.Views;
using Fac.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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

            AppCenter.Start("android=172d71d8-c821-4136-b531-fa69fd9f18a1;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
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
