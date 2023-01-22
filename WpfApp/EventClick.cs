using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using ClientModels;
using Ninject;
using ValueObjects;
using WpfLibrary;

namespace WpfApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void SetupEvent()
        {
            var names = App.GroupHandler.GetNamesByLogin(App.UserHandler.Login, App.UserHandler.URI);
            guiEventEditGroup.ItemsSource = names;
        }

        private void GuiEventNew_Click(object sender, RoutedEventArgs e)
        {
            guicEventEdit.Visibility = Visibility.Visible;
            ClearEditNew();
        }

        private void GuiEventDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (guicEvent.DataContext as ScheduleItem)!;
            if (item.Event.Group == "Личные")
                App.EventHandler.Delete(App.UserHandler.Login, item.Event, App.UserHandler.URI);
            else
                App.GroupHandler.RemoveEvent(App.UserHandler.Login, item.Event.Group, item.Event, App.UserHandler.URI);

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
                App.container.Get<INotificationDraw>().ShowNotification(
                    new Notification("Неверный формат ввода для времени (должно быть hh:mm)", NotificationType.Error));
                return;
            }

            var _event = new Event(guiEventEditTitle.Text,
                new GoalType("type", new ColorARGB(Color.Aqua.A, Color.Aqua.R, Color.Aqua.G, Color.Aqua.B)),
                new TimeInterval(start, end), guiEventEditDesc.Text);

            if (guiEventEditGroup.Text != null)
            {
                var name = (string) guiEventEditGroup.SelectedItem;
                _event.Group = name;
                App.GroupHandler.AddEvent(App.UserHandler.Login, name, _event, App.UserHandler.URI);
            }
            else
            {
                App.EventHandler.Add(App.UserHandler.Login, _event, App.UserHandler.URI);
            }

            guicScheduleDay.Add(new ScheduleItem(_event));
            guicScheduleDay.Redraw();
            ClearEditNew();
        }

        private void GuiEventChange_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GuiEventEditCancel_Click(object sender, RoutedEventArgs e)
        {
            guicEventEdit.Visibility = Visibility.Collapsed;
            ClearEditNew();
        }

        private void ClearEditNew()
        {
            guiEventEditDate.SelectedDate = DateTime.Today.Date;
            guiEventEditStart.Text = null;
            guiEventEditEnd.Text = null;
            guiEventEditTitle.Text = null;
            guiEventEditDesc.Text = null;
            guiEventEditType.Text = null;
            guiEventEditGroup.Text = null;
        }
    }
}