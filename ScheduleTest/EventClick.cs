using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using ValueObjects;
using WebApiClient;
using WpfSchedule;

namespace ScheduleTest
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void GuiEventNew_Click(object sender, RoutedEventArgs e)
        {
            guicEventEdit.Visibility = Visibility.Visible;
            ClearNew();
        }

        private void GuiEventDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (guicEvent.DataContext as ScheduleItem)!;
            
            var req = ClientRequests.DeleteEventRequestConstruct(App.User.Login, item.Event, App.URI);
            ClientRequests.Post(req);
            
            guicScheduleDay.RemoveWithRedraw(item);
            guicEvent.DataContext = null;
            guifEventDelete.IsEnabled = false;
        }

        private void GuiEventEditSave_Click(object sender, RoutedEventArgs e)
        {
            var start = guiEventEditDate.SelectedDate.Value;
            var end = guiEventEditDate.SelectedDate.Value;

            try
            {
                start = start.Add(TimeSpan.Parse(guiEventEditStart.Text));
                end = end.Add(TimeSpan.Parse(guiEventEditEnd.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var _event = new Event(guiEventEditTitle.Text, new GoalType("type", new ColorARGB(Color.Aqua.A, Color.Aqua.R, Color.Aqua.G, Color.Aqua.B)),
                new TimeInterval(start, end), guiEventEditDesc.Text);
            guicScheduleDay.Add(new ScheduleItem(_event));
            guicScheduleDay.Redraw();
            ClearNew();

            var req = ClientRequests.AddNewEventRequestConstruct(App.User.Login, _event, App.URI);
            ClientRequests.Post(req);
        }
    }
}