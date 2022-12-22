using System.Windows;
using ClientModels;

namespace WpfApp
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void GuiEnterLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = guiEnterLogin.Text;
            var user = new UserLogin(new UserRequests()).TryGet(login, App.Handler.URI);

            if (user is null)
                MessageBox.Show("Пользователя с таким логином не существует");
            else
            {
                App.Handler.Setup(user[0]);
                var mainWindow = new MainWindow();
                mainWindow.ShowWindow();
                Close();
            }
        }
    }
}