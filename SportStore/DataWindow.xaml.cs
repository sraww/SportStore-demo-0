using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportStore
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {

        public static DataGrid datagrid;
        public DataWindow()
        {
            InitializeComponent();

            using (SportStoreContext db = new SportStoreContext())
            {
                usersGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
            }
        }

        private void SelectUser(object sender, MouseButtonEventArgs e)
        {
            User user = (User)usersGrid.SelectedItem;

            int Id = (usersGrid.SelectedItem as User).Id;

            new UpdateWindow(user, Id).ShowDialog();
        }

        private void AddUsers(object sender, RoutedEventArgs e)
        {
            new AddWindow(null).ShowDialog();

            using (SportStoreContext db = new SportStoreContext())
            {
                usersGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
            }
        }

        private void UpdateUsers(object sender, RoutedEventArgs e)
        {
            User user = (User)usersGrid.SelectedItem;

            int Id = (usersGrid.SelectedItem as User).Id;

            new UpdateWindow(user, Id).ShowDialog();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            using (SportStoreContext db = new SportStoreContext())
            {
                User user = (User)usersGrid.SelectedItem;
                if (user is null)
                    return;

                if (MessageBox.Show($"Вы точно хотитие удалить запись {user.Name}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        MessageBox.Show($"Пользователь {user.Name} удален");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                usersGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
            }
        }
    }
}

