using System.Windows;
using ClientModels;
using Ninject;
using ValueObjects;
using WpfLibrary;

namespace WpfApp
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var login = this.login.Text;
            var password = this.password.Text;
            var u = new User(login, password);
            var loginExist = App.UserHandler.ClientUser.CheckLoginExist(u, App.UserHandler.URI);
            
            if (!loginExist)
            {
                var user = new User(name.Text, surname.Text, login, password);
                var success = App.UserHandler.ClientUser.TryAdd(user.Login, user, App.UserHandler.URI);
                if (!success)
                    App.container.Get<INotificationDraw>().ShowNotification(new Notification("Что-то пошло не так. Попробуйте позже снова", NotificationType.Error));
                else
                {
                    var log = new Login();
                    log.Show();
                    Close();
                }
            }
            else
            {
                App.container.Get<INotificationDraw>().ShowNotification(new Notification("Пользователь с таким логином уже существует", NotificationType.Error));
            }
        }
    }
}