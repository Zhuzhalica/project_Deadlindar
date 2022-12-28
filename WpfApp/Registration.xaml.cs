﻿using System.Windows;
using ClientModels;

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
            var user = App.Handler.ClientUser.TryGet(login, App.Handler.URI);
            
            if (user is null)
            {
                user = new User(name.Text, surname.Text, login);
                // TODO
            }
            else
            {
                // TODO
            }
        }
    }
}