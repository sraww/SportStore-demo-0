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
    public partial class AddWindow : Window
    {
        public AddWindow(User user)
        {
            InitializeComponent();

            DataContext = user;
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

                try
                {
                    db.SaveChanges();

                    MessageBox.Show($"Пользователь {textBoxName.Text} успешно добавлен !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                } 
            }
        }
    }
}