using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SportStore
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public DataWindow()
        {
            InitializeComponent();

            using (SportStoreContext db = new SportStoreContext())
            {
                usersGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
                Proxy.userGrid = usersGrid;
            }
        }

        private void SelectUser(object sender, MouseButtonEventArgs e)
        {
            User user = (User)usersGrid.SelectedItem;
            int Id = (usersGrid.SelectedItem as User).Id;
            new AddEditWindow(user, Id).ShowDialog();
        }
        private void UpdateUsers_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = (User)usersGrid.SelectedItem;
                int Id = (usersGrid.SelectedItem as User).Id;
                new AddEditWindow(user, Id).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddUsers(object sender, RoutedEventArgs e)
        {
            new AddEditWindow(null, 0).ShowDialog();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var selectedUsers = usersGrid.SelectedItems.Cast<User>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {selectedUsers.Count()} пользователей", "Внимание!",
                 MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    SportStoreContext db = new SportStoreContext();
                    db.Users.RemoveRange(selectedUsers);
                    db.SaveChanges();
                    usersGrid.ItemsSource = db.Users.ToList();
                    MessageBox.Show("Пользователи удалены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (e.Key == Key.F5)
            //    {
            //        using (SportStoreContext db = new SportStoreContext())
            //        {
            //            usersGrid.ItemsSource = db.Users.ToList();
            //            Proxy.userGrid = usersGrid;
            //        }
            //    }
        }
    }
}

