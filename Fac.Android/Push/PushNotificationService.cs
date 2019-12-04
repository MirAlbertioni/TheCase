using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Iid;
using Microsoft.AppCenter.Crashes;
using WindowsAzure.Messaging;

namespace Fac.Droid.Push
{
    public sealed class PushNotificationService : IPushNotificationService
    {
        private readonly NotificationHub hub;
        //private readonly ILogger logger = LogManager.ForContext<PushNotificationService>();
        public PushNotificationService(ISettingsService settings)
        {
            var context = Android.App.Application.Context;
            hub = new NotificationHub(settings.NotificationHubName,
                settings.NotificationHubListenConnectionString, context);
        }


        public async Task Register(IAuthenticatedUserContext context)
        {
            //var userTag = context.User.Id.Replace('|', ':');
            var userTag = "hellokitti";

            var tags = new List<string> { $"user:{userTag}", $"tenant:ica", $"site:kvantum", $"id:123456789" };

            var token = NotificationId;

            if (string.IsNullOrWhiteSpace(token)) token = FirebaseInstanceId.Instance.Token;

            await RegisterToNotificationHub(token, tags);
        }

        public void SetNotificationId(string id)
        {
            NotificationId = id;
        }

        public string NotificationId { get; private set; }


        private async Task RegisterToNotificationHub(string token, List<string> tags)
        {
            await Task.Run(() =>
            {
                try
                {
                    var regId = hub.Register(token, tags.ToArray()).RegistrationId;
                    //logger.LogVerbose($"Successful registration of ID {regId} with tags {string.Join(",", tags)}");
                }
                catch (Exception e)
                {
                    Crashes.TrackError(e);
                }
            });

        }
    }

    public interface ISettingsService
    {
        string Auth0Domain { get; set; }
        string Auth0ClientId { get; set; }
        string AppCenterAnalyticsAndroid { get; set; }
        string ImageBlobStorage { get; set; }
        string MobileGateWayUrl { get; set; }
        string ApiAudience { get; set; }
        string NotificationHubName { get; set; }
        string NotificationHubListenConnectionString { get; set; }
    }
}