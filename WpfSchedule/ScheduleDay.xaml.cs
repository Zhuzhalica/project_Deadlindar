using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfSchedule
{
    /// <summary>
    /// Interaction logic for ScheduleDay.xaml
    /// </summary>
    public partial class ScheduleDay : UserControl
    {
        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value.Date;
                _guitTitle.Content = string.Format("{0} {1:00}/{2}", CurrentDate.DayOfWeek.ToString(), CurrentDate.Day,
                    CurrentDate.Month);
                ChangeDate();
            }
        }

        public TimeSpan Interval { get; set; }
        public bool FitToSize { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }

        private int timeDisplayInterval;
        private double intervalHeight;

        public double IntervalHeight
        {
            get => intervalHeight;
            set
            {
                intervalHeight = value;
                if (TimeVisualIntervalHeight % value == 0)
                {
                    timeDisplayInterval = (int) (TimeVisualIntervalHeight / value);
                }
                else
                {
                    timeDisplayInterval = (int) (TimeVisualIntervalHeight / value) + 1;
                }
            }
        }

        private bool darkTheme;

        public bool DarkTheme
        {
            get => darkTheme;
            set
            {
                darkTheme = value;
                if (darkTheme)
                {
                    _guicGridTimeline.BorderBrush = Brushes.AntiqueWhite;
                    _guicTop.BorderBrush = Brushes.AntiqueWhite;
                    // DefaultBorderColor = Brushes.AntiqueWhite;
                    foreach (ScheduleItem item in Items)
                    {
                        item.Panel.BorderBrush = Brushes.AntiqueWhite;
                    }
                }
                else
                {
                    _guicGridTimeline.BorderBrush = Brushes.Black;
                    _guicTop.BorderBrush = Brushes.Black;
                    // ScheduleItem.DefaultBorderColor = Brushes.Black;
                    foreach (ScheduleItem item in Items)
                    {
                        item.Panel.BorderBrush = Brushes.Black;
                    }
                }
            }
        }

        private List<ScheduleItem> Items;

        public event EventHandler ScheduleItemClick;

        private static double
            TimeVisualIntervalHeight = 50.0; //minimum height between displaying the time interval        

        public ScheduleDay()
        {
            InitializeComponent();
            Items = new List<ScheduleItem>();

            //assign event handlers
            _guicCanvas.MouseLeftButtonDown += OnScheduleItemClick;

            //default values
            Interval = new TimeSpan(0, 30, 0);
            TimeStart = new TimeSpan(0, 0, 0);
            TimeEnd = new TimeSpan(1, 0, 0, 0);
            CurrentDate = DateTime.Today.Date;
            FitToSize = false;
            IntervalHeight = 50.0;
            DarkTheme = false;

            Redraw();
        }

        public void Add(ScheduleItem item)
        {
            var totalSeconds = TimeEnd.TotalSeconds - TimeStart.TotalSeconds;
            item.GeneratePanel(_guicCanvas.ActualWidth, _guicCanvas.ActualHeight, TimeStart.TotalSeconds,
                TimeEnd.TotalSeconds, totalSeconds);
            Items.Add(item);
            DrawItems();
        }

        public void RemoveWithRedraw(ScheduleItem item)
        {
            Items.Remove(item);
            DrawItems();
        }

        public void Clear()
        {
            _guicCanvas.Children.Clear();
            Items.Clear();
        }

        public IReadOnlyCollection<ScheduleItem> GetItems()
        {
            return Items.AsReadOnly();
        }


        private void OnScheduleItemClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Items.Count > 0)
            {
                var i = Items.FindIndex(x => x.Event.TimeInterval.startTime >= CurrentDate.Add(TimeStart));
                for (; i < Items.Count && !Items[i].Panel.IsMouseOver; i++)
                {
                }

                if (i < Items.Count && Items[i].Clickable)
                {
                    ScheduleItemClick?.Invoke(Items[i], EventArgs.Empty);
                }
                else
                {
                    //nothing was clicked, fire event with no object
                    ScheduleItemClick?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        private void ChangeDate()
        {
            _guicCanvas.Children.Clear();
            DrawItems();
        }

        public void Redraw()
        {
            _guicGrid.Children.Clear();
            _guicGrid.RowDefinitions.Clear();

            this.UpdateLayout();

            var canvasHeight = 0.0;

            var rows = 0;
            //calculate number of intervals in day and height of drawing canvas
            for (var c = TimeEnd; c > TimeStart; c -= Interval)
            {
                rows++;
                canvasHeight += IntervalHeight;
            }

            _guicGrid.Children.Add(_guicGridTimeline);
            _guicGridTimeline.SetValue(Grid.RowSpanProperty, rows);
            _guicCanvas.SetValue(Grid.RowSpanProperty, rows);

            //recalculate intervals and override canvas height if FitToSize is enabled
            if (FitToSize)
            {
                canvasHeight = _guicScroll.ActualHeight;
                IntervalHeight = canvasHeight / (double) rows;
            }

            //create grid rows and draw time intervals
            var time = TimeStart;
            for (var i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(IntervalHeight);
                _guicGrid.RowDefinitions.Add(row);

                Border border = new Border();
                border.BorderBrush = _guicGridTimeline.BorderBrush;
                border.BorderThickness = new Thickness(0, 0, 0, 1);
                border.SetValue(Grid.ColumnSpanProperty, 2);
                border.SetValue(Grid.RowProperty, i);
                _guicGrid.Children.Add(border);

                if (i == 0 || i % timeDisplayInterval == 0)
                {
                    Label label = new Label();
                    label.Content = string.Format("{0:00}:{1:00}", time.Hours, time.Minutes);
                    label.SetValue(Grid.ColumnProperty, 0);
                    label.SetValue(Grid.RowProperty, i);
                    label.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                    label.SetValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                    _guicGrid.Children.Add(label);
                }

                time = time.Add(Interval);
            }

            //draw items
            Items.Sort(ScheduleItem.SortByStart);
            DrawItems();
        }

        private void DrawItems()
        {
            _guicCanvas.Children.Clear();
            var startIndex = Items.FindIndex(x => x.Event.TimeInterval.startTime >= CurrentDate.Add(TimeStart));
            if (startIndex > -1)
                for (var i = startIndex;
                    i < Items.Count && Items[i].Event.TimeInterval.startTime < CurrentDate.Add(TimeEnd);
                    i++)
                {
                    Items[i].GeneratePanel(_guicCanvas.ActualWidth, _guicCanvas.ActualHeight, TimeStart.TotalSeconds,
                        TimeEnd.TotalSeconds, TimeEnd.TotalSeconds - TimeStart.TotalSeconds);
                    _guicCanvas.Children.Add(Items[i].Panel);
                }
        }
    }
}