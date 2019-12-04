using Android.App;
using Firebase.Iid;

namespace Fac.Droid.Push
{
    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        //private readonly ILogger logger = LogManager.ForContext<MyFirebaseIIDService>();

        public MyFirebaseIIDService()
        {

        }

        public override void OnTokenRefresh()
        {
            var pushNotificationService = Locator.Instance.Resolve<IPushNotificationService>();

            var refreshedToken = FirebaseInstanceId.Instance.Token;
            //logger.LogVerbose($"OnTokenRefresh: {refreshedToken}");
            pushNotificationService.SetNotificationId(refreshedToken);
        }
    }
}
