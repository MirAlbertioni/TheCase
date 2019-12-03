using Fac.Models;
using Newtonsoft.Json;
using RestSharp;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Login(Username.Text, Password.Text);
        }

        public void Login(string username, string password)
        {
            // We are using the RestSharp library which provides many useful
            // methods and helpers when dealing with REST.
            // We first create the request and add the necessary parameters
            var client = new RestClient("https://cubcloud-test.eu.auth0.com");
            var request = new RestRequest("oauth/ro", Method.POST);
            request.AddParameter("client_id", "fJIVJprBiMCm8jVpi3fvaqBQWtpoFPKv");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("connection", "DeliveryManagerApidb");
            request.AddParameter("grant_type", "password");
            request.AddParameter("scope", "openid");

            // We execute the request and capture the response
            // in a variable called `response`
            IRestResponse response = client.Execute(request);

            // Using the Newtonsoft.Json library we deserialaize the string into an object,
            // we have created a LoginToken class that will capture the keys we need
            var token = JsonConvert.DeserializeObject<Token>(response.Content);

            // We check to see if we received an `id_token` and if we did make a secondary call
            // to get the user data. If we did not receive an `id_token` we can safely assume
            // that the authentication failed so we display an error message telling the user
            // to try again.
            if (token.Id_token != null)
            {
                Application.Current.Properties["id_token"] = token.Id_token;
                Application.Current.Properties["access_token"] = token.Access_token;
                GetUserData(token.Id_token);
            }
            else
            {
                DisplayAlert("Oh No!", "It's seems that you have entered an incorrect email or password. Please try again.", "OK");
            };
        }

        public void GetUserData(string token)
        {
            var client = new RestClient("https://cubcloud-test.eu.auth0.com");
            var request = new RestRequest("tokeninfo", Method.GET);
            request.AddParameter("id_token", token);


            IRestResponse response = client.Execute(request);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            // Once the call executes, we capture the user data in the
            // `Application.Current` namespace which is globally available in Xamarin
            Application.Current.Properties["Email"] = user.Email;
            Application.Current.Properties["Name"] = user.Name;

            MessagingCenter.Send(user, "UserEmail");

            // Finally, we navigate the user the the Orders page
            Navigation.PushModalAsync(new MainPage());
        }
    }
}