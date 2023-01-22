using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ValueObjects;

namespace WpfLibrary
{
    /// <summary>
    /// Interaction logic for ScheduleMonth.xaml
    /// </summary>
    public partial class ScheduleMonth : INotifyPropertyChanged
    {
        private DateTime _currentDate;

        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                if (_currentDate != value)
                {
                    _currentDate = value;
                    OnPropertyChanged(() => CurrentDate);
                    DrawDays();
                    SetDateSelectionComboBoxesByCurrentDate();
                }
            }
        }

        private void SetDateSelectionComboBoxesByCurrentDate()
        {
            MonthsComboBox.SelectedValue = CurrentDate.Month;
            YearsComboBox.SelectedValue = CurrentDate.Year;
        }

        private void SetCurrentDateByDateSelectionComboBoxes()
        {
            if (YearsComboBox?.SelectedValue != null && MonthsComboBox?.SelectedValue != null)
            {
                CurrentDate = new DateTime((int) YearsComboBox.SelectedValue, (int) MonthsComboBox.SelectedValue, 1);
            }
        }

        public event EventHandler<CalendarEventView> CalendarEventDoubleClickedEvent;

        public ObservableCollection<CalendarDay> DaysInCurrentMonth { get; set; }
        private List<Event> Events;

        public void CalendarMonth(List<Event> events)
        {
            InitializeComponent();
            DaysInCurrentMonth = new ObservableCollection<CalendarDay>();
            Events = events;
            InitializeDayLabels();
            InitializeDateSelectionComboBoxes();
        }

        private void InitializeDayLabels()
        {
            for (var i = 0; i < 7; i++)
            {
                var dayLabel = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };
                dayLabel.SetValue(Grid.ColumnProperty, i);
                // Days.Sunday == 0, so i+1 will make Monday as first day
                dayLabel.Content = CultureInfo.InstalledUICulture.DateTimeFormat.DayNames[(i + 1) % 7];
                dayLabel.FontWeight = FontWeights.Bold;
                DayLabelsGrid.Children.Add(dayLabel);
            }
        }

        private void InitializeDateSelectionComboBoxes()
        {
            CurrentDate = DateTime.Now;
            
            for (var i = 1; i <= 12; i++)
                MonthsComboBox.Items.Add(i);

            for (var i = CurrentDate.Year - 100; i <= CurrentDate.Year + 100; i++)
                YearsComboBox.Items.Add(i);
        }

        public void CalendarEventDoubleClicked(CalendarEventView calendarEventView)
        {
            CalendarEventDoubleClickedEvent?.Invoke(this, calendarEventView);
        }

        internal void CalendarEventClicked(CalendarEventView eventToSelect)
        {
            foreach (CalendarDay day in DaysInCurrentMonth)
            foreach (CalendarEventView e in day.Events.Children)
                e.BackgroundColor = e.DataContext == eventToSelect.DataContext 
                    ? HighlightColor 
                    : e.DefaultBackfoundColor;
        }

        public void DrawDays()
        {
            DaysGrid.Children.Clear();
            DaysInCurrentMonth.Clear();

            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            for (var date = firstDayOfMonth; date.Date <= lastDayOfMonth; date = date.AddDays(1))
            {
                var newDay = new CalendarDay();
                newDay.Border.BorderThickness =
                    new Thickness((double) GridBorderThickness /
                                  2); // /2 because neighbor day has border too, so two half borders next to each other will create final border
                newDay.Border.BorderBrush = GridBrush;
                newDay.Date = date;
                DaysInCurrentMonth.Add(newDay);
            }

            var row = 0;
            var column = 0;

            for (var i = 0; i < DaysInCurrentMonth.Count; i++)
            {
                switch (DaysInCurrentMonth[i].Date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        column = 0;
                        break;
                    case DayOfWeek.Tuesday:
                        column = 1;
                        break;
                    case DayOfWeek.Wednesday:
                        column = 2;
                        break;
                    case DayOfWeek.Thursday:
                        column = 3;
                        break;
                    case DayOfWeek.Friday:
                        column = 4;
                        break;
                    case DayOfWeek.Saturday:
                        column = 5;
                        break;
                    case DayOfWeek.Sunday:
                        column = 6;
                        break;
                }

                Grid.SetRow(DaysInCurrentMonth[i], row);
                Grid.SetColumn(DaysInCurrentMonth[i], column);
                DaysGrid.Children.Add(DaysInCurrentMonth[i]);

                if (column == 6)
                {
                    row++;
                }
            }

            DrawTopBorder();
            DrawBottomBorder();
            DrawRightBorder();
            DrawLeftBorder();

            // set some background today
            var today = DaysInCurrentMonth.FirstOrDefault(d => d.Date == DateTime.Today);
            if (today != null)
            {
                today.DateTextBlock.Background = Color0;
            }

            DrawEvents();
        }

        private void DrawTopBorder()
        {
            // draw top border line to be the same as inner lines in calendar
            for (int i = 0; i < 7; i++)
            {
                DaysInCurrentMonth[i].Border.BorderThickness = new Thickness(
                    DaysInCurrentMonth[i].Border.BorderThickness.Left, GridBorderThickness,
                    DaysInCurrentMonth[i].Border.BorderThickness.Right,
                    DaysInCurrentMonth[i].Border.BorderThickness.Bottom);
            }
        }

        private void DrawBottomBorder()
        {
            // draw bottom border line to be the same as inner lines in calendar
            int daysInCurrentMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            for (int i = daysInCurrentMonth - 1; i >= daysInCurrentMonth - 7; i--)
            {
                DaysInCurrentMonth[i].Border.BorderThickness = new Thickness(
                    DaysInCurrentMonth[i].Border.BorderThickness.Left, DaysInCurrentMonth[i].Border.BorderThickness.Top,
                    DaysInCurrentMonth[i].Border.BorderThickness.Right, GridBorderThickness);
            }
        }

        private void DrawRightBorder()
        {
            // draw right border line to be the same as inner lines in calendar
            IEnumerable<CalendarDay> sundays = DaysInCurrentMonth.Where(d => d.Date.DayOfWeek == DayOfWeek.Sunday);
            foreach (var sunday in sundays)
            {
                sunday.Border.BorderThickness = new Thickness(sunday.Border.BorderThickness.Left,
                    sunday.Border.BorderThickness.Top, GridBorderThickness, sunday.Border.BorderThickness.Bottom);
            }

            // right border for last day in month
            var lastDay = DaysInCurrentMonth.Last();
            lastDay.Border.BorderThickness = new Thickness(lastDay.Border.BorderThickness.Left,
                lastDay.Border.BorderThickness.Top, GridBorderThickness, lastDay.Border.BorderThickness.Bottom);
        }

        private void DrawLeftBorder()
        {
            // draw left border line to be the same as inner lines in calendar
            IEnumerable<CalendarDay> mondays = DaysInCurrentMonth.Where(d => d.Date.DayOfWeek == DayOfWeek.Monday);
            foreach (var monday in mondays)
            {
                monday.Border.BorderThickness = new Thickness(GridBorderThickness, monday.Border.BorderThickness.Top,
                    monday.Border.BorderThickness.Right, monday.Border.BorderThickness.Bottom);
            }

            // left border for first day in month
            var firstDay = DaysInCurrentMonth.First();
            firstDay.Border.BorderThickness = new Thickness(GridBorderThickness, firstDay.Border.BorderThickness.Top,
                firstDay.Border.BorderThickness.Right, firstDay.Border.BorderThickness.Bottom);
        }

        private void DrawEvents()
        {
            // this method can be called when Events is not binded yet. So check that case and return
            if (Events.Count == 0)
            {
                return;
            }
            
            // when Events is binded, check if it is collection of ICalendarEvent (events must have DateFrom and DateTo)
            if (Events is IEnumerable<Event> events)
            {
                // add colors of events to array, to pick up them using number
                SolidColorBrush[] colors = {Color0, Color1, Color2};
            
                // index to array of colors
                int accentColor = 0;
            
                // loop all events
                foreach (var e in events.OrderBy(e => e.TimeInterval.startTime.Date))
                {
                    // if (!e.TimeInterval.startTime.Ha || !e.DateTo.HasValue)
                    // {
                    //     continue;
                    // }
            
                    // number of row in day, in which event should be displayed
                    var eventRow = 0;
            
                    var dateFrom = (DateTime) e.TimeInterval.startTime;
                    var dateTo = (DateTime) e.TimeInterval.endTime;
            
                    // loop all days of current event
                    var date = dateFrom;
                    for (; date <= dateTo; date = date.AddDays(1))
                    {
                        // get DayOfWeek for current day of current event
                        CalendarDay day = DaysInCurrentMonth.Where(d => d.Date.Date == date.Date).FirstOrDefault();
            
                        // day is in another mont, so skip it
                        if (day == null)
                        {
                            continue;
                        }
            
                        // if the DayOfWeek is Monday, event shloud be displayed on first row
                        if (day.Date.DayOfWeek == DayOfWeek.Monday)
                        {
                            eventRow = 0;
                        }
            
                        // but if there are some events before, event won't be on the first row, but after previous events
                        if (day.Events.Children.Count > eventRow)
                        {
                            eventRow = Grid.GetRow(day.Events.Children[day.Events.Children.Count - 1]) + 1;
                        }
            
                        // get color for event
                        var accentColorIndex = accentColor % colors.Count();
                        var calendarEventView = new CalendarEventView(Brushes.Blue, this);
            
                        calendarEventView.DataContext = e;
                        Grid.SetRow(calendarEventView, eventRow);
                        day.Events.Children.Add(calendarEventView);
                    }
            
                    accentColor++;
                }
            }
            else
            {
                throw new ArgumentException("Events must be IEnumerable<Event>");
            }
        }

        private void PreviousMonthButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentDate.Month == 1)
            {
                CurrentDate = CurrentDate.AddYears(-1);
            }

            CurrentDate = CurrentDate.AddMonths(-1);
        }

        private void NextMonthButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentDate.Month == 12)
            {
                CurrentDate = CurrentDate.AddYears(1);
            }

            CurrentDate = CurrentDate.AddMonths(1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T>> exp)
        {
            //the cast will always succeed
            var memberExpression = (MemberExpression) exp.Body;
            var propertyName = memberExpression.Member.Name;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MonthsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetCurrentDateByDateSelectionComboBoxes();
        }

        private void YearsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetCurrentDateByDateSelectionComboBoxes();
        }
    }
}