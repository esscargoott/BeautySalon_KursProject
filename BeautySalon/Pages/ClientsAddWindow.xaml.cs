using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для ClientsEditWindow.xaml
    /// </summary>
    public partial  class ClientsAddWindow : Window
    {
        AzaleaDBEntities db;
        private Client _currentClients = new Client();

        public ClientsAddWindow(Client selectedClient)
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
            if(selectedClient != null)
            { _currentClients = selectedClient; }

            DataContext = _currentClients;
        }

        //Кнопка для перехода на страницу клиентов
        private void btnBackClients_Click(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show(" При выходе данные в полях не сохранятся, вы хотите выйти?", "Внимание!" , 
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
           {
                this.Close();
           }
        }

        //Кнопка для добавления клиентов
        private void btnClientsAdd_Click(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            int count = tbPhone.Text.Length;
            if (!(Regex.IsMatch(tbEmail.Text, emailPattern)))
            {
                MessageBox.Show("Неверный формат почты!", "Ошибка");
            }
            else if  (count < 11)
            {
                MessageBox.Show("Номер телефона меньше 11 цифр!", "Ошибка");
            }
            else { 
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(_currentClients.FullName))
                    errors.AppendLine("Введите ФИО клиента");
                if (string.IsNullOrWhiteSpace(_currentClients.Phone))
                    errors.AppendLine("Введите номер телефона");
                if (string.IsNullOrWhiteSpace(_currentClients.Address))
                    errors.AppendLine("Ввведите адрес клиента");
                if (string.IsNullOrWhiteSpace(_currentClients.Email))
                    errors.AppendLine("Введите почту клиента");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                if (_currentClients.IdClient == 0)
                {
                    db.Clients.Add(_currentClients);
                }
                else
                {
                    var client = db.Clients.Find(_currentClients.IdClient);

                    client.FullName = _currentClients.FullName;
                    client.Phone = _currentClients.Phone;
                    client.Address = _currentClients.Address;
                    client.Email = _currentClients.Email;
                }
               
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена! ");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        //Метод для ввода только цифр в поле 
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        //Вызов метода CheckIsNumeric(e);
        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        //Метод для ввода только русских букв в поле "ФИО клиента"
        private void TextBoxFullname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if ((inp < 'А' || inp > 'Я' )&&(inp < 'а' || inp > 'я'))
                e.Handled = true;
           
        }
    }
}
