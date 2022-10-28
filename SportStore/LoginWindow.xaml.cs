using SportStore.Models;
using System.Linq;
using System.Windows;

namespace SportStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteContext db = new SQLiteContext())
            {
                if (db.Users.FirstOrDefault(user => user.Name == textBoxLogin.Text && user.Password == passBox.Password) != null)
                {
                    MessageBox.Show("Успешно");
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        private void ButtonGuest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
