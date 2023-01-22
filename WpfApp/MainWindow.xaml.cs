using System;
using System.ComponentModel;
using System.Linq;
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
            SetupGroup();
            SetupEvent();

            guicScheduleDay.IntervalHeight = 25.0;
            guicScheduleDay.Interval = new TimeSpan(1, 0, 0);

            foreach (var _event in App.EventHandler.Events)
                guicScheduleDay.Add(new ScheduleItem(_event));
            
            foreach (var _event in App.GroupHandler.Events)
                guicScheduleDay.Add(new ScheduleItem(_event));
            
            guicScheduleMonth.CalendarMonth(App.GroupHandler.Events);
            
            guicScheduleMonth.DrawDays();
            guicScheduleMonth.CalendarEventDoubleClickedEvent += Calendar_CalendarEventDoubleClickedEvent;
            
            guidDate.SelectedDate = DateTime.Today.Date;
            guicEventEdit.Visibility = Visibility.Collapsed;
        }

        public void ShowWindow()
        {
            Show();
            guicScheduleMonth.DrawDays();
            guicScheduleDay.Redraw();
            Show();
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
            ShowWindow();
        }

        private void GuicSchedule_ScheduleItemClick(object sender, EventArgs e)
        {
            if (sender is null)
            {
                ClearEditNew();
                guicEventEdit.Visibility = Visibility.Collapsed;
                guicEvent.DataContext = null;
                guifEventDelete.IsEnabled = false;
            }
            else
            {
                var item = sender as ScheduleItem;
                SetSelectedEvent(item.Event);
                guicEvent.DataContext = sender;
                guifEventDelete.IsEnabled = true;
            }
        }

        private void SetSelectedEvent(Event @event)
        {
            guiEventSelectDate.Text = @event.TimeInterval.startTime.Date.ToString();
            guiEventSelectStart.Text = @event.TimeInterval.startTime.TimeOfDay.ToString();
            guiEventSelectEnd.Text = @event.TimeInterval.startTime.TimeOfDay.ToString();
            guiEventSelectTitle.Text = @event.Title;
            guiEventSelectDescription.Text =  @event.Description;
            guiEventSelectType.Text = @event.GoalType.Title;
            guiEventSelectGroup.Text = @event.Group;
        }
        
        private void DeleteSelectedEvent(Event @event)
        {
            guiEventSelectDate.Text = null;
            guiEventSelectStart.Text = null;
            guiEventSelectEnd.Text = null;
            guiEventSelectTitle.Text = null;
            guiEventSelectDescription.Text = null;
            guiEventSelectType.Text = null;
            guiEventSelectGroup.Text = null;
        }
    }
}