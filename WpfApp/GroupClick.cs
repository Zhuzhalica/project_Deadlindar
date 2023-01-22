using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Ninject;
using ValueObjects;
using WpfLibrary;

namespace WpfApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private EditGroupState currentState = EditGroupState.Default;

        private void SetupGroup()
        {
            var roles = new List<GroupRole>() {GroupRole.Admin, GroupRole.Member, GroupRole.Anonym};
            guiGroupMemberRole1.ItemsSource = roles;
            guiGroupMemberRole2.ItemsSource = roles;
            guiGroupMemberRole3.ItemsSource = roles;
            guiGroupMemberRole4.ItemsSource = roles;

            guiGroupEditRole.ItemsSource = roles;

            var names = App.GroupHandler.GetNamesByLogin(App.UserHandler.Login, App.UserHandler.URI);
            guiGroupsComboBox.ItemsSource = names;
        }

        private void GuiGroupNew_Click(object sender, RoutedEventArgs e)
        {
            guiGroupCreate.Visibility = Visibility.Visible;
            ClearGroupNew();
        }

        private void GuiGroupCreateSave_Click(object sender, RoutedEventArgs e)
        {
            var members = new Dictionary<string, GroupRole>();
            if (guiGroupMember1.Text != "" && guiGroupMemberRole1.SelectionBoxItem != null)
                members.Add(guiGroupMember1.Text, (GroupRole) guiGroupMemberRole1.SelectionBoxItem);
            if (guiGroupMember2.Text != "" && guiGroupMemberRole2.SelectionBoxItem != null)
                members.Add(guiGroupMember2.Text, (GroupRole) guiGroupMemberRole2.SelectionBoxItem);
            if (guiGroupMember3.Text != "" && guiGroupMemberRole3.SelectionBoxItem != null)
                members.Add(guiGroupMember3.Text, (GroupRole) guiGroupMemberRole3.SelectionBoxItem);
            if (guiGroupMember4.Text != "" && guiGroupMemberRole4.SelectionBoxItem != null)
                members.Add(guiGroupMember4.Text, (GroupRole) guiGroupMemberRole4.SelectionBoxItem);
            var name = guiGroupCreateName.Text;

            var group = new Group(name, members);
            App.GroupHandler.Create(App.UserHandler.Login, group, App.UserHandler.URI);
            ClearGroupNew();
        }

        private void GuiGroupCreateCancel_Click(object sender, RoutedEventArgs e)
        {
            guiGroupCreate.Visibility = Visibility.Collapsed;
            ClearGroupNew();
        }

        private void ClearGroupNew()
        {
            guiGroupCreateName.Text = null;
            guiGroupMember1.Text = null;
            guiGroupMember2.Text = null;
            guiGroupMember3.Text = null;
            guiGroupMember4.Text = null;

            guiGroupMemberRole1.Text = null;
            guiGroupMemberRole2.Text = null;
            guiGroupMemberRole3.Text = null;
            guiGroupMemberRole4.Text = null;
        }

        private void GuiGroupAddUser_Click(object sender, RoutedEventArgs e)
        {
            ClearGroupEditNew();
            guiGroupEditNameText.Visibility = Visibility.Visible;
            guiGroupEditRole.Visibility = Visibility.Visible;
            OptionGridGroupEdit.Visibility = Visibility.Visible;
            currentState = EditGroupState.AddUser;
        }

        private void GuiGroupRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            ClearGroupEditNew();
            guiGroupEditNameBox.Visibility = Visibility.Visible;
            OptionGridGroupEdit.Visibility = Visibility.Visible;


            guiGroupEditNameBox.ItemsSource = App.GroupHandler.GetByName(App.UserHandler.Login,
                (string) guiGroupsComboBox.SelectedItem, App.UserHandler.URI).Users;
            currentState = EditGroupState.RemoveUser;
        }


        private void GuiGroupChangeRole_Click(object sender, RoutedEventArgs e)
        {
            ClearGroupEditNew();
            guiGroupEditNameBox.Visibility = Visibility.Visible;
            guiGroupEditRole.Visibility = Visibility.Visible;
            OptionGridGroupEdit.Visibility = Visibility.Visible;

            guiGroupEditNameBox.ItemsSource = App.GroupHandler.GetByName(App.UserHandler.Login,
                (string) guiGroupsComboBox.SelectedItem, App.UserHandler.URI).Users;
            currentState = EditGroupState.ChangeRole;
        }

        private void GuiGroupEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (OptionGridGroupEdit.Visibility == Visibility.Visible)
            {
                var role = GroupRole.Anonym;
                switch (currentState)
                {
                    case EditGroupState.AddUser:
                        var addLogin = guiGroupEditNameText.Text;
                        role = (GroupRole) guiGroupEditRole.SelectedItem;
                        App.GroupHandler.AddUser(App.UserHandler.Login, (string) guiGroupsComboBox.SelectedItem,
                            addLogin,
                            role, App.UserHandler.URI);
                        break;
                    case EditGroupState.RemoveUser:
                        var removeLogin = (string) guiGroupEditNameBox.SelectedItem;
                        App.GroupHandler.RemoveUser(App.UserHandler.Login, (string) guiGroupsComboBox.SelectedItem,
                            removeLogin,
                            App.UserHandler.URI);
                        break;
                    case EditGroupState.ChangeRole:
                        var changeLogin = (string) guiGroupEditNameBox.SelectedItem;
                        role = (GroupRole) guiGroupEditRole.SelectedItem;
                        App.GroupHandler.ChangeRole(App.UserHandler.Login, (string) guiGroupsComboBox.SelectedItem,
                            changeLogin,
                            role, App.UserHandler.URI);
                        break;
                }
            }

            currentState = EditGroupState.Default;
            ClearGroupEditNew();
        }

        private void GuiGroupEditCancel_Click(object sender, RoutedEventArgs e)
        {
            guiGroupEdit.Visibility = Visibility.Collapsed;
            ClearGroupEditNew();
            guiGroupsComboBox.Text = null;
        }

        private void GuiGroupsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb && cb.SelectedItem != null)
            {
                guiGroupEdit.Visibility = Visibility.Visible;
                ClearGroupEditNew();
            }
        }

        private void ClearGroupEditNew()
        {
            OptionGridGroupEdit.Visibility = Visibility.Collapsed;
            guiGroupEditNameText.Visibility = Visibility.Collapsed;
            guiGroupEditNameBox.Visibility = Visibility.Collapsed;
            guiGroupEditRole.Visibility = Visibility.Collapsed;
            guiGroupEditRole.Text = null;
            guiGroupEditNameBox.Text = null;
            guiGroupEditNameText.Text = null;
        }
        
    }

    public enum EditGroupState
    {
        Default,
        AddUser,
        RemoveUser,
        ChangeRole
    }
}