using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSchedule
{
    /// <summary>
    /// Логика взаимодействия для CalendarEventView.xaml
    /// </summary>
    public partial class CalendarEventView : Page
    {
        private ScheduleMonth _calendar;

        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush), typeof(CalendarEventView));

        public SolidColorBrush DefaultBackfoundColor;

        public CalendarEventView()
        {
            InitializeComponent();
        }

        public CalendarEventView(SolidColorBrush color, ScheduleMonth calendar) : this()
        {
            _calendar = calendar;
            DefaultBackfoundColor = BackgroundColor = color;
        }

        private void EventMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                _calendar.CalendarEventDoubleClicked(this);
            }
            else if (e.ChangedButton == MouseButton.Left && e.ClickCount == 1)
            {
                _calendar.CalendarEventClicked(this);
            }
        }
    }
}
