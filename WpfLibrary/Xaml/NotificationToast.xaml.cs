using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ValueObjects;

namespace WpfLibrary.Xaml
{
    public partial class NotificationToast : Window, INotification
    {
        public NotificationToast()
        {
            InitializeComponent();
        }

        private readonly Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public void ShowNotification(Notification notification)
        {
            ShowNotificationByType(notification);
        }

        private void ShowNotificationByType(Notification notification)
        {
            switch (notification.Type)
            {
                case NotificationType.Error:
                    notifier.ShowError(notification.Text);
                    break;
                case NotificationType.Information:
                    notifier.ShowInformation(notification.Text);
                    break;
                case NotificationType.Success:
                    notifier.ShowSuccess(notification.Text);
                    break;
                case NotificationType.Warning:
                    notifier.ShowWarning(notification.Text);
                    break;
            }
        }
    }
}