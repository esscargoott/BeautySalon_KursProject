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

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для MastersAddWindow.xaml
    /// </summary>
    public partial class MastersAddWindow : Window
    {
        AzaleaDBEntities db;
        private Master _currentMasters = new Master();

        public MastersAddWindow(Master selectedMaster)
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
            if (selectedMaster != null)
            { _currentMasters = selectedMaster; }

            DataContext = _currentMasters;
        }

        //Кнопка для перехода на страницу мастеров
        private void btnBackMasters_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите выйти? Данные в полях не будут сохранены.", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
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

        //Кнопка для добавления мастеров
        private void btnMastersAdd_Click(object sender, RoutedEventArgs e)
        {
            int count = tbPhone.Text.Length;

            if (count < 11)
            {
                MessageBox.Show("Номер телефона меньше 11 цифр!", "Ошибка");
            }
            else
            {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(_currentMasters.FullName))
                    errors.AppendLine("Введите ФИО мастера");
                if (string.IsNullOrWhiteSpace(_currentMasters.JobTitle))
                    errors.AppendLine("Введите должность мастера ");
                if (string.IsNullOrWhiteSpace(_currentMasters.Address))
                    errors.AppendLine("Введите адрес мастера ");
                if (string.IsNullOrWhiteSpace(_currentMasters.Phone))
                    errors.AppendLine("Введите телефон мастера ");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                if (_currentMasters.IdMaster == 0)
                {
                    db.Masters.Add(_currentMasters);
                }
                else
                {
                    var master = db.Masters.Find(_currentMasters.IdMaster);

                    master.FullName = _currentMasters.FullName;
                    master.Phone = _currentMasters.Phone;
                    master.Address = _currentMasters.Address;
                    master.JobTitle = _currentMasters.JobTitle;
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

        //Метод для ввода только русских букв в поле "ФИО мастера"
        private void tbFulname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if ((inp < 'А' || inp > 'Я') && (inp < 'а' || inp > 'я'))
                e.Handled = true;
        }

        //Метод для ввода только русских букв в поле "должность мастера"
        private void tbJobTitle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char inp = e.Text[0];
            if ((inp < 'А' || inp > 'Я') && (inp < 'а' || inp > 'я'))
                e.Handled = true;
        }
    }
}
