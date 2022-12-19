using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WpfSchedule
{
    public partial class ScheduleMonth : INotifyPropertyChanged
    {
                public SolidColorBrush GridBrush
        {
            get { return (SolidColorBrush)GetValue(GridBrushProperty); }
            set { SetValue(GridBrushProperty, value); }
        }

        public static readonly DependencyProperty GridBrushProperty =
            DependencyProperty.Register("GridBrush", typeof(SolidColorBrush), typeof(ScheduleMonth),
                new PropertyMetadata(Brushes.Black));


        public SolidColorBrush Color0
        {
            get { return (SolidColorBrush)GetValue(Color0Property); }
            set { SetValue(Color0Property, value); }
        }

        public static readonly DependencyProperty Color0Property =
            DependencyProperty.Register("Color0", typeof(SolidColorBrush), typeof(ScheduleMonth),
                new PropertyMetadata(Brushes.LightCyan));


        public SolidColorBrush Color1
        {
            get { return (SolidColorBrush)GetValue(Color1Property); }
            set { SetValue(Color1Property, value); }
        }

        public static readonly DependencyProperty Color1Property =
            DependencyProperty.Register("Color1", typeof(SolidColorBrush), typeof(ScheduleMonth),
                new PropertyMetadata(Brushes.PaleTurquoise));

        public SolidColorBrush Color2
        {
            get { return (SolidColorBrush)GetValue(Color2Property); }
            set { SetValue(Color2Property, value); }
        }

        public static readonly DependencyProperty Color2Property =
            DependencyProperty.Register("Color2", typeof(SolidColorBrush), typeof(ScheduleMonth),
                new PropertyMetadata(Brushes.SkyBlue));

        public SolidColorBrush HighlightColor
        {
            get { return (SolidColorBrush)GetValue(HighlightColorProperty); }
            set { SetValue(HighlightColorProperty, value); }
        }

        public static readonly DependencyProperty HighlightColorProperty =
            DependencyProperty.Register("HighlightColor", typeof(SolidColorBrush), typeof(ScheduleMonth),
                new PropertyMetadata(Brushes.DodgerBlue));


        public double GridBorderThickness
        {
            get { return (double)GetValue(GridBorderThicknessProperty); }
            set { SetValue(GridBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty GridBorderThicknessProperty =
            DependencyProperty.Register("GridBorderThickness", typeof(double), typeof(ScheduleMonth),
                new PropertyMetadata(0.5));
    }
}