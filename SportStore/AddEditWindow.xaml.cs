using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Linq;
using System.Text;
using System.Windows;


namespace SportStore
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        User currentUser = new User();

        int Id;

        public AddEditWindow(User user, int userId)
        {
            InitializeComponent();

            if (user == null)
                currentUser = null;
            else
            {
                currentUser = user;
                Id = userId;
                DataContext = currentUser;
            }
        }

        private void AddUserButton(object sender, RoutedEventArgs e)
        {
            // Валидация полей
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(textBoxPatronymic.Text))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
                errors.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(textBoxRole.Text))
                errors.AppendLine("Укажите роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (SportStoreContext db = new SportStoreContext())
            {
                if (currentUser == null)
                {
                    User user = new User()
                    {
                        Name = textBoxName.Text,
                        Surname = textBoxSurname.Text,
                        Patronymic = textBoxPatronymic.Text,
                        Login = textBoxLogin.Text,
                        Password = textBoxPassword.Text,
                        Role = db.Roles.Where(r => r.Name == textBoxRole.Text).FirstOrDefault()
                    };

                    db.Users.Add(user);
                }

                else
                {
                    //currentUser.Role = db.Roles.Where(r => r.Name == textBoxRole.Text).FirstOrDefault();
                    //db.Update(currentUser);

                    User user = (from m in db.Users where m.Id == Id select m).Single();

                    user.Name = textBoxName.Text;
                    user.Surname = textBoxSurname.Text;
                    user.Patronymic = textBoxPatronymic.Text;
                    user.Login = textBoxLogin.Text;
                    user.Password = textBoxPassword.Text;
                    user.Role = db.Roles.Where(r => r.Name == textBoxRole.Text).FirstOrDefault();
                }

                try
                {
                    if (db.Roles.Where(r => r.Name == textBoxRole.Text).FirstOrDefault() != null)
                    {
                        db.SaveChanges();
                        MessageBox.Show("Данные добавлены");
                    }
                    else
                    {
                        MessageBox.Show($"Такой роли нет");
                    }

                    Proxy.userGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}