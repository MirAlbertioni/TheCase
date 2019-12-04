
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Android.Util;
using Firebase;
using System.Threading.Tasks;
using Firebase.Iid;
using Android.Content;
using Fac.Droid.Push;
using Android.Gms.Common;
using Microsoft.AppCenter.Crashes;

namespace Fac.Droid
{
    [Activity(Label = "File a case", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //FirebaseApp.InitializeApp(Application.Context);

            try
            {
                var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:389037544240:android:c9073ac989c7b51f")
                .SetApiKey("AIzaSyD_SQbhfVAkyF3aLLf9noAIblv5IRYTgiY")
                .SetDatabaseUrl("https://file-a-case.firebaseio.com")
                .SetStorageBucket("*file-a-case.appspot.com")
                .SetGcmSenderId("389037544240").Build();

                var fapp = FirebaseApp.InitializeApp(this, options);
            }
            catch (System.Exception e)
            {
                Crashes.TrackError(e);
            }
            

            CrossCurrentActivity.Current.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                base.OnBackPressed();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}