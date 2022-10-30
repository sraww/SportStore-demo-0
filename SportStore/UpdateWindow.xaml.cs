using SportStore.Models;
using System;
using System.Linq;
using System.Windows;

namespace SportStore
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {

        public int Id;

        public UpdateWindow(User user, int userId)
        {
            InitializeComponent();

            DataContext = user;
            Id = userId;
        }

        private void UpdateUserButton(object sender, RoutedEventArgs e)
        {
            using (SportStoreContext db = new SportStoreContext())
            {
                try
                {
                    User user = (from m in db.Users where m.Id == Id select m).Single();

                    user.Name = textBoxName.Text;
                    user.Surname = textBoxSurname.Text;
                    user.Patronymic = textBoxPatronymic.Text;
                    user.Login = textBoxLogin.Text;
                    user.Password = textBoxPassword.Text;
                    user.Role = db.Roles.Where(r => r.Name == textBoxRole.Text).FirstOrDefault();

                    db.SaveChanges();

                    MessageBox.Show("Данные изменены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
