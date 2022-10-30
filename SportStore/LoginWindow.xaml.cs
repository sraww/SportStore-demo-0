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
            using (SportStoreContext db = new SportStoreContext())
            {
                if (db.Users.FirstOrDefault(user => user.Login == textBoxLogin.Text && user.Password == passBox.Password) != null)
                {
                    new DataWindow().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        private void ButtonGuest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Гость");
        }
    }
}
