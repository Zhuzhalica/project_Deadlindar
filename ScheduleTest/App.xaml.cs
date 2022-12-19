using System;
using System.Windows;
using ValueObjects;

namespace ScheduleTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User User;
        public static string URI = "https://localhost:7135";
        public App()
        {
        }
    }
}