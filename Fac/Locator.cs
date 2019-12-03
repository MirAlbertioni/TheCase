using Autofac;
using Fac.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fac
{
    public class Locator
    {
        IContainer container;
        readonly ContainerBuilder containerBuilder;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            try
            {
                containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<AboutViewModel>();
                containerBuilder.RegisterType<BaseViewModel>();
                containerBuilder.RegisterType<CaseDetailViewModel>();
                containerBuilder.RegisterType<CasesViewModel>();
                containerBuilder.RegisterType<NewCaseViewModel>();
                containerBuilder.RegisterType<UnreadCasesViewModel>();
            }
            catch (Exception e)
            {

            }
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        public void RegisterSingelton<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();

        public void RegisterInstanceAs<TInterface>(object instance) => containerBuilder.RegisterInstance(instance).SingleInstance().As<TInterface>();

        public void Build() => container = containerBuilder.Build();
    }

    public interface IPushNotificationService
    {
        Task Register(IAuthenticatedUserContext context);

        void SetNotificationId(string id);
        string NotificationId { get; }
    }

    public interface ISilentPushService
    {
        void ExecuteAction(IDictionary<string, string> data);
    }

    public class SilentPushService : ISilentPushService
    {
        public void ExecuteAction(IDictionary<string, string> data)
        {
            //if (data.All(k => k.Key != "action"))
            //{
            //    return;
            //}

            var action = data.First(k => k.Key == "action").Value;

            if (action == "PicklistAssigmentCompleted")
            {
                var ticket = data.FirstOrDefault(k => k.Key == "ticket").Value;
                var picklistId = data.FirstOrDefault(k => k.Key == "picklistId").Value;

                MessagingCenter.Send("SilentPushService", "Hallo där");
            }


        }
    }

    public interface IAuthenticatedUserContext
    {
        //UserContext User { get; }
        bool IsAuthenticated { get; }

        string DeviceId { get; }

        //void SetUser(UserContext user);
        //void SetTenant(TenantViewModel tenant);

        //void SetSite(SiteViewModel site);

        void SetCurrentDeviceId(string id);
    }
}
