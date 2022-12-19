using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ScheduleTest
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void GuidDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            guicScheduleDay.CurrentDate = guidDate.SelectedDate.Value;
        }

        private void GuifDatePrev_Click(object sender, RoutedEventArgs e)
        {
            guidDate.SelectedDate = guidDate.SelectedDate.Value.AddDays(-1.0);
        }

        private void GuifDateToday_Click(object sender, RoutedEventArgs e)
        {
            guidDate.SelectedDate = DateTime.Today.Date;
        }

        private void GuifDateNext_Click(object sender, RoutedEventArgs e)
        {
            guidDate.SelectedDate = guidDate.SelectedDate.Value.AddDays(1.0);
        }
    }
}