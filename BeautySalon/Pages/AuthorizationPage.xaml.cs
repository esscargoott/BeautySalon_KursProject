using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        //Кнопка для выхода с программы
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //Кнопка для входа в программу
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            var admin = AzaleaDBEntities.getContext().Admins.FirstOrDefault(a => a.Login == txtLogin.Text)?.IdAdmin;

            if (authorize(txtLogin.Text, passBoxPassword.Password))
            {
                MessageBox.Show("Вы успешно авторизованы", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
                var history = new LoginHistory();
                history.IdAdmin = admin.Value;
                history.DateTime = DateTime.Now;
                AzaleaDBEntities.getContext().LoginHistories.Add(history);
                AzaleaDBEntities.getContext().SaveChanges();
                Manage.MainFrame.Navigate(new HistoryPage());
            }
        }

        //Метод для авторизации
        private bool authorize (string login, string password)
        {
            int errors = 0;
            try
            {
                foreach (var admin in AzaleaDBEntities.getContext().Admins.ToList())
                { 
                    if(login == admin.Login && password == admin.Password)
                    {
                        Manage.adminName = admin.Name;
                        Manage.adminPatronymic = admin.Patronymic;
                        errors = 0;
                        break;
                    }
                    else
                    { errors++; }
                }
                if(errors == 0)
                {
                    return true;
                }
                else
                { MessageBox.Show("Введенны неверные данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с базой данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return false;
        }

        //Код для скрытия/отображения пароля
        private void checkBoxShowPass_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxShowPass.IsChecked == true)
            {
                txtBoxPassword.Text = passBoxPassword.Password; // скопируем в TextBox из PasswordBox
                txtBoxPassword.Visibility = Visibility.Visible; // TextBox - отобразить
                passBoxPassword.Visibility = Visibility.Collapsed; // PasswordBox - скрыть
                txtBlockPassword.Text = "Скрыть пароль";
            }
            else
            {
                passBoxPassword.Password = txtBoxPassword.Text; // скопируем в PasswordBox из TextBox 
                txtBoxPassword.Visibility = Visibility.Collapsed; // TextBox - скрыть
                passBoxPassword.Visibility = Visibility.Visible; // PasswordBox - отобразить
                txtBlockPassword.Text = "Показать пароль";
            }
        }

    }
}
