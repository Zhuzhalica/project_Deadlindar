using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ValueObjects;
using WpfLibrary;

namespace WpfLibrary
{
    public class ScheduleItem : IEventDrawer
    {
        internal string Time =>
            $"{Event.TimeInterval.startTime.ToShortTimeString()} - {Event.TimeInterval.endTime.ToShortTimeString()}";

        internal Border Panel;

        public bool Clickable { get; set; }
        public Brush BorderColor { get; set; }
        public Brush FillColor { get; set; }

        public Brush DefaultBorderColor = Brushes.Black;
        public Brush DefaultFillColor;
        public  Event Event { get; }

        public object Data; //can hold a reference to an object if needed

        public ScheduleItem(Event _event)
        {
            Event = _event;
            var color = _event.GoalType.Color;
            DefaultFillColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));

            //defaults
            BorderColor = DefaultBorderColor;
            FillColor = DefaultFillColor;
            Clickable = true;
        }

        internal void GeneratePanel(double width, double height, double secondsStart, double secondsEnd,
            double secondsTotal)
        {
            Panel = new Border
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = BorderColor,
                Background = FillColor
            };

            var yPos = (Event.TimeInterval.startTime.TimeOfDay.TotalSeconds - secondsStart) / secondsTotal * height;

            Panel.Width = width > 16 ? width : 0.0;
            Panel.Height = Event.TimeInterval.endTime < Event.TimeInterval.startTime
                ? 0.0
                : (Event.TimeInterval.endTime.TimeOfDay.TotalSeconds - secondsStart) / secondsTotal * height - yPos;

            Canvas.SetLeft(Panel, 8);
            Canvas.SetTop(Panel, yPos);

            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            Label apptTime = new Label
            {
                Margin = new Thickness(1.0, 0.0, 0.0, 0.0),
                FontSize = 11,
                Padding = new Thickness(0.0),
                Foreground = Brushes.Black
            };

            apptTime.Content = Time;

            Label apptDesc = new Label
            {
                Margin = new Thickness(1.0, 0.0, 0.0, 0.0),
                FontSize = 11,
                Padding = new Thickness(0.0),
                Foreground = Brushes.Black
            };

            apptDesc.Content = $"{Event.Group}: {Event.Title}";

            panel.Children.Add(apptTime);
            panel.Children.Add(apptDesc);
            Panel.Child = panel;
        }

        internal static int SortByStart(ScheduleItem x, ScheduleItem y)
        {
            return DateTime.Compare(x.Event.TimeInterval.startTime, y.Event.TimeInterval.startTime);
        }

        public void DrawEvent()
        {
            throw new NotImplementedException();
        }
    }
}