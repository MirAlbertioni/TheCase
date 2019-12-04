using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Autofac;
using Fac.ViewModels;
using Firebase.Messaging;
using Xamarin.Forms;

namespace Fac.Droid.Push
{
    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string UrgentChannel = "se.fac.urgent";
        const string SilentChannel = "se.fac.silent";
        private readonly ISilentPushService silent;
        private readonly Dictionary<string, NotificationChannel> channels = new Dictionary<string, NotificationChannel>();

        public MyFirebaseMessagingService()
        {
            silent = Locator.Instance.Resolve<ISilentPushService>();

            if (!channels.Any())
            {
                var uChannel = new NotificationChannel(UrgentChannel, UrgentChannel, NotificationImportance.High);
                uChannel.EnableVibration(true);
                uChannel.LockscreenVisibility = NotificationVisibility.Public;

                channels.Add(UrgentChannel, uChannel);

                var sChannel = new NotificationChannel(SilentChannel, SilentChannel, NotificationImportance.Default);
                sChannel.EnableVibration(false);
                sChannel.LockscreenVisibility = NotificationVisibility.Secret;

                channels.Add(SilentChannel, sChannel);
            }
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            //SendIt();
            var notificaion = message.GetNotification();

            if (notificaion != null)
            {
                //Push send as notification time to notify the user....
                SendNotification(message.GetNotification().Body);
            }
            else
            {
                //Push sent as silent get data and do the action...
                //var action = message.Data.Keys.FirstOrDefault(v => v == "action");

                silent.ExecuteAction(message.Data);
            }
        }

        void SendNotification(string messageBody)
        {
            var chan = channels.First().Value;

            var notificationManager =
                (NotificationManager)GetSystemService(NotificationService);

            notificationManager.CreateNotificationChannel(chan);

            var mBuilder = new NotificationCompat.Builder(this)
                .SetContentTitle("Attention!")
                .SetContentText(messageBody)
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetChannelId(UrgentChannel);

            notificationManager.Notify(555, mBuilder.Build());
        }
    }
}