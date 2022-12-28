using System.Windows;

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
            var user = App.Handler.ClientUser.TryGet(login, App.Handler.URI);

            if (user is null)
            {
                // TODO
            }
            else
            {
                App.Handler.Setup();
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