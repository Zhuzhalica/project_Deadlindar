using System.Windows;
using WebApiClient;

namespace ScheduleTest
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
            App.User = RequestForUser.TryAuthorization(login, App.URI);
            
            if (App.User is null)
                MessageBox.Show("Пользователя с таким логином не существует");

            else
            {
                var mainWindow = new MainWindow();
                mainWindow.ReShow();  
                Close();
            }
        }
    }
}