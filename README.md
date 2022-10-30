## Сделать CRUD операции

### DataWindow
![createEF](https://user-images.githubusercontent.com/98191494/198881726-0ef5e82d-dca1-4d13-8f1d-07749aa67993.PNG)
```C#
 <Grid>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition Height="50"/>
        </Grid.RowDefinitions>

        <DataGrid x:Name="usersGrid" 
                  IsReadOnly="True" 
                  AutoGenerateColumns="False" 
                  MouseDoubleClick="SelectUser" >

            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Name}" Header="Имя"/>
                <DataGridTextColumn Binding="{Binding Surname}" Header="Фамилия"/>
                <DataGridTextColumn Binding="{Binding Patronymic}" Header="Отчество" Width="400"/>

                <DataGridTemplateColumn Header="Action">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <Button Content="Удалить" Click="Delete" Width="200" Background="#FF76E383" />
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>

            </DataGrid.Columns>
        </DataGrid>

        <Button Width="120" Height="30" Margin="200, 0, 0, 0" Content="Добавить" Click="AddUsers" Grid.Row="1" Background="#FF76E383" />

        <Button Width="120" Height="30" Margin="-200, 0, 0, 0"  Content="Изменить" Click="UpdateUsers" Grid.Row="1" Background="#FF76E383" />

    </Grid>
```

### Код окна DataWindow
```C#
public partial class DataWindow : Window
    {

        public static DataGrid datagrid;
        public DataWindow()
        {
            InitializeComponent();

            // Read Чтение данных
            using (SportStoreContext db = new SportStoreContext())
            {
                usersGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
            }
        }

        // Update (Вариант 1) Редактирование данных по двойному нажатию мыши
        private void SelectUser(object sender, MouseButtonEventArgs e)
        {
            User user = (User)usersGrid.SelectedItem;

            int Id = (usersGrid.SelectedItem as User).Id;

            new UpdateWindow(user, Id).ShowDialog();
        }

        // Create Добавить пользователя
        private void AddUsers(object sender, RoutedEventArgs e)
        {
            new AddWindow(null).ShowDialog();

            using (SportStoreContext db = new SportStoreContext())
            {
                usersGrid.ItemsSource = db.Users.Include(u => u.Role).ToList();
            }
        }

        // Update (Вариант 2) Редактирование данных по кнопке
        private void UpdateUsers(object sender, RoutedEventArgs e)
        {
            User user = (User)usersGrid.SelectedItem;

            int Id = (usersGrid.SelectedItem as User).Id;

            new UpdateWindow(user, Id).ShowDialog();
        }

        // Delete удалить запись
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
```

### UpdateWindow
![update](https://user-images.githubusercontent.com/98191494/198883786-67e8bf24-66f0-45e5-a0c8-761f6fb7a7b9.PNG)
```C#
   <Grid>
        <Border BorderThickness="1"
                BorderBrush="BlueViolet"
                Margin="10">

            <StackPanel Margin="5">
                <TextBlock Text="Имя" Width="200" Height="20" />
                <TextBox x:Name="textBoxName" Text="{Binding Name}" Width="200" Height="20"/>

                <TextBlock Text="Фамилия" Width="200" Height="20"/>
                <TextBox x:Name="textBoxSurname" Text="{Binding Surname}" Width="200" Height="20" />

                <TextBlock Text="Отчество" Width="200" Height="20"/>
                <TextBox x:Name="textBoxPatronymic" Text="{Binding Patronymic}" Width="200" Height="20" />

                <TextBlock Text="Логин" Width="200" Height="20"/>
                <TextBox x:Name="textBoxLogin" Text="{Binding Login}" Width="200" Height="20" />

                <TextBlock Text="Пароль" Width="200" Height="20"/>
                <TextBox x:Name="textBoxPassword" Text="{Binding Password}" Width="200" Height="20" />

                <TextBlock Text="Роль" Width="200" Height="20"/>
                <TextBox x:Name="textBoxRole" Text="{Binding Role.Name}" Width="200" Height="20"/>

                <Button Content="Изменить" Click="UpdateUserButton" Margin="0, 25, 0, 0" Background="#FF76E383" Width="200" Height="20" />
            </StackPanel>
        </Border>
    </Grid>
```

### Код окна UpdateWindow
```C#
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
```
