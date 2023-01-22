using System;
using System.Windows;
using ClientModels;
using Ninject;
using ValueObjects;
using WpfLibrary;
using WpfLibrary.Xaml;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUserDataHandler UserHandler;
        public static IEventHandler EventHandler;
        public static INotificationHandler NotificationHandler;
        public static IGroupHandler GroupHandler;
        public static StandardKernel container;
        public App()
        {
            container = ConfigureContainer();
            UserHandler = container.Get<IUserDataHandler>();
            EventHandler = container.Get<IEventHandler>();
            NotificationHandler = container.Get<INotificationHandler>();
            GroupHandler = container.Get<IGroupHandler>();
        }

        private StandardKernel ConfigureContainer()
        {
            var container = new StandardKernel();
            
            container.Bind<IEventRequest>().To<EventRequests>().InSingletonScope();
            container.Bind<INotificationRequests>().To<NotificationRequests>().InSingletonScope();
            container.Bind<IUserRequest>().To<UserRequests>().InSingletonScope();
            container.Bind<IGroupRequest>().To<GroupRequest>().InSingletonScope();

            container.Bind<IClientSaver>().To<JsonSaver>().InSingletonScope();
            
            container.Bind<ClientEvents>().To<ClientEvents>().InSingletonScope();
            container.Bind<ClientNotifications>().To<ClientNotifications>().InSingletonScope();
            container.Bind<ClientUser>().To<ClientUser>().InSingletonScope();
            container.Bind<ClientGroup>().To<ClientGroup>().InSingletonScope();

            container.Bind<IUserDataHandler>().To<UserDataHandler>().InSingletonScope();
            container.Bind<IEventHandler>().To<EventsHandler>().InSingletonScope();
            container.Bind<INotificationHandler>().To<NotificationHandler>().InSingletonScope();
            container.Bind<IGroupHandler>().To<GroupHandler>().InSingletonScope();
            
            container.Bind<INotificationDraw>().To<NotificationDrawWpf>().InSingletonScope();

            return container;
        }
    }
}