﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using ValueObjects;
using WpfLibrary;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();

            guicScheduleDay.IntervalHeight = 25.0;
            guicScheduleDay.Interval = new TimeSpan(1, 0, 0);

            foreach (var _event in App.Handler.ClientEvents.Events)
                guicScheduleDay.Add(new ScheduleItem(_event));
            
            guicScheduleMonth.CalendarMonth();
            
            guicScheduleMonth.DrawDays();
            guicScheduleMonth.CalendarEventDoubleClickedEvent += Calendar_CalendarEventDoubleClickedEvent;
            
            guidDate.SelectedDate = DateTime.Today.Date;
            guicEventEdit.Visibility = Visibility.Collapsed;
        }

        public void ShowWindow()
        {
            Show();
            guicScheduleDay.Redraw();
        }

        private void Calendar_CalendarEventDoubleClickedEvent(object? sender, CalendarEventView e)
        {
            if (e.DataContext is Event calendarEvent)
            {
                MessageBox.Show($"'{calendarEvent.Title}' double clicked");
            }
        }

        public void OnPropertyChanged<T>(Expression<Func<T>> exp)
        {
            var memberExpression = (MemberExpression) exp.Body;
            var propertyName = memberExpression.Member.Name;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ClearNew()
        {
            guiEventEditDate.SelectedDate = DateTime.Today.Date;
            guiEventEditStart.Text = null;
            guiEventEditEnd.Text = null;
            guiEventEditTitle.Text = null;
            guiEventEditDesc.Text = null;
        }
        
        private void GuicSchedule_ScheduleItemClick(object sender, EventArgs e)
        {
            if (sender is null)
            {
                ClearNew();
                guicEventEdit.Visibility = Visibility.Collapsed;
                guicEvent.DataContext = null;
                guifEventDelete.IsEnabled = false;
            }
            else
            {
                guicEvent.DataContext = sender;
                guifEventDelete.IsEnabled = true;
            }
        }
    }
}