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
            var user = App.Handler.ClientUser.TryGet(u, App.Handler.URI);

            if (user is null)
            {
                App.container.Get<INotificationDraw>().ShowNotification(new Notification("Неверный логин или пороль", NotificationType.Error));
            }
            else
            {
                App.Handler.Setup(user.Login);
                var mainWindow = new MainWindow();
                mainWindow.ShowWindow();
                Close();
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var regis = new Registration();
            regis.Show();
            Close();
        }
    }
}