using System.Windows;
using ClientModels;
using Ninject;
using ValueObjects;
using WpfLibrary;
using WpfLibrary.Xaml;

namespace WpfApp
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var login = this.login.Text;
            var password = this.password.Password;
            var u = new User(login, password);
            var user = App.UserHandler.ClientUser.TryGet(u, App.UserHandler.URI);

            if (user is null)
            {
                App.container.Get<INotificationDraw>().ShowNotification(new Notification("Неверный логин или пороль", NotificationType.Error));
            }
            else
            {
                Setup(user.Login);
                var mainWindow = new MainWindow();
                mainWindow.ShowWindow();
                Close();
            }
        }

        private void Setup(string login)
        {
            var uri = App.UserHandler.URI;
            App.UserHandler.Setup(login);
            App.EventHandler.Setup(login, uri);
            App.NotificationHandler.Setup(login, uri);
            App.GroupHandler.Setup(login, uri);
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var regis = new Registration();
            regis.Show();
            Close();
        }
    }
}