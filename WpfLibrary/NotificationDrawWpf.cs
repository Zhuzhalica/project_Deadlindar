using System;
using System.Windows;
using Notifications.Wpf;
using ValueObjects;
using NType = Notifications.Wpf.NotificationType;
using NotificationType = ValueObjects.NotificationType;


namespace WpfLibrary.Xaml
{
    public class NotificationDrawWpf : INotificationDraw
    {
        private NotificationManager NotificationManager = new NotificationManager();

        public void ShowNotification(Notification notification)
        {
            var type = GetNotificationType(notification.Type);
            var content = new NotificationContent()
            {
                Title = notification.Title,
                Message = notification.Text,
                Type = type
            };
            NotificationManager.Show(content);
        }

        private NType GetNotificationType(NotificationType type)
        {
            return type switch
            {
                NotificationType.Error => NType.Error,
                NotificationType.Information => NType.Information,
                NotificationType.Success => NType.Success,
                NotificationType.Warning => NType.Warning,
                _ => NType.Information
            };
        }
    }
}