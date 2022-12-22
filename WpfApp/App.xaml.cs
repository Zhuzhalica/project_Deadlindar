using System;
using System.Windows;
using ClientModels;
using ValueObjects;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHandler Handler;
        public App()
        {
            var eventService = new EventService(new EventRequests());
            var notificationService = new NotificationsService(new NotificationRequests());
            Handler = new UserDataHandler(eventService, notificationService);
        }
    }
}