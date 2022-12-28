using System;
using System.Windows;
using ClientModels;
using Ninject;
using ValueObjects;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHandler Handler;
        public static StandardKernel container;
        public App()
        {
            container = ConfigureContainer();
            Handler = container.Get<IHandler>();
        }

        private StandardKernel ConfigureContainer()
        {
            var container = new StandardKernel();
            
            container.Bind<IRequests<Event>>().To<EventRequests>().InSingletonScope();
            container.Bind<IRequests<Notification>>().To<NotificationRequests>().InSingletonScope();
            container.Bind<IRequests<User>>().To<UserRequests>().InSingletonScope();
            
            container.Bind<ClientEvents>().To<ClientEvents>().InSingletonScope();
            container.Bind<ClientNotifications>().To<ClientNotifications>().InSingletonScope();
            container.Bind<ClientUser>().To<ClientUser>().InSingletonScope();

            container.Bind<IHandler>().To<UserDataHandler>().InSingletonScope();

            return container;
        }
    }
}