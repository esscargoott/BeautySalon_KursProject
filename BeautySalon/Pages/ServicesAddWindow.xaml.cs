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
    /// Логика взаимодействия для ServicesAddWindow.xaml
    /// </summary>
    public partial class ServicesAddWindow : Window
    {
        private Service _currentServices = new Service();
        AzaleaDBEntities db;

        public ServicesAddWindow()
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
            ComboMasters.ItemsSource = db.Masters.ToList();

            DataContext = _currentServices;
        }

        //Кнопка для перехода на страницу услуг
        private void btnBackServices_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите выйти? Данные в полях не будут сохранены.", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        //Кнопка для добавления услуг
        private void btnAddServices_Click(object sender, RoutedEventArgs e)
        {
            var masterId = from m in db.Masters
                           where m.FullName == ComboMasters.Text
                           select m.IdMaster;

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentServices.Name))
                errors.AppendLine("Введите название услуги");
            if (_currentServices.Price == 0 )
                errors.AppendLine("Введите стоимость услуги");
            if (string.IsNullOrWhiteSpace(_currentServices.Description))
                errors.AppendLine("Введите описание услуги");
            if (masterId.FirstOrDefault() == 0)
                errors.AppendLine("Выберите мастера");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentServices.IdService == 0)
            {
                db.Services.Add(_currentServices);
                _currentServices.Masters.Add(db.Masters.FirstOrDefault(m => m.IdMaster == masterId.FirstOrDefault()));
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
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
    }
}
