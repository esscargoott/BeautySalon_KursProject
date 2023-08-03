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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для ScheduleAddWindow.xaml
    /// </summary>
    public partial class ScheduleAddWindow : Window
    {
        AzaleaDBEntities db;
        private  Schedule _currentSchedule = new Schedule();

        public ScheduleAddWindow(Schedule selectedValue)
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
            if(selectedValue != null)
            {
                _currentSchedule = selectedValue;
            }

            ComboClients.ItemsSource = db.Clients.ToList();
            ComboServices.ItemsSource = db.Services.ToList();
            ComboMasters.ItemsSource = db.Masters.ToList();
            
            DataContext = _currentSchedule;
        }

        //Кнопка для перехода на страницу расписания
        private void btnBackSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите выйти? Данные в полях не будут сохранены.", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        //Кнопка для добавления записи на прием
        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var masterId = from m in db.Masters
                           where m.FullName == ComboMasters.Text
                           select m.IdMaster;

            var clientId = from c in db.Clients
                           where c.FullName == ComboClients.Text
                           select c.IdClient;

            var serviceId = from s in db.Services
                            where s.Name == ComboServices.Text
                            select s.IdService;

            if (masterId.FirstOrDefault() == 0)
                errors.AppendLine("Выберите мастера");
            if (clientId.FirstOrDefault() == 0)
                errors.AppendLine("Выберите клиента");
            if (serviceId.FirstOrDefault() == 0)
                errors.AppendLine("Выберите услугу");
            if (dtPicker.Value == null)
                errors.AppendLine("Выберите дату");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentSchedule = new Schedule 
            {
                IdMaster = masterId.FirstOrDefault(),
                IdClient = clientId.FirstOrDefault(),
                IdService = serviceId.FirstOrDefault(),
                DateTime = (DateTime)dtPicker.Value
            };

            if (_currentSchedule.IdSchedule == 0)
            {
                db.Schedules.Add(_currentSchedule);
            }
            else
            {
                _currentSchedule.IdMaster = masterId.FirstOrDefault();
                _currentSchedule.IdClient = clientId.FirstOrDefault();
                _currentSchedule.IdService = serviceId.FirstOrDefault();
                _currentSchedule.DateTime = (DateTime)dtPicker.Value;
            }
           

            try
            {
                db.SaveChanges();
                MessageBox.Show("Информация сохранена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //Код для обновления ComboServices при изменении значения ComboMasters 
        private void ComboMasters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Master selectedValue = ComboMasters.SelectedItem as Master;
            if (selectedValue != null)
            {
                List<Service> linkedValues = GetLinkedValuesFromDatabase(selectedValue);
                ComboServices.ItemsSource = linkedValues;
            }
        }

        //Метод GetLinkedValuesFromDatabase()
        private List<Service> GetLinkedValuesFromDatabase(Master selectedValue)
        {
            List<Service> linkedValues = new List<Service>();
            using (var context = new AzaleaDBEntities())
            {
                var master = context.Masters.Where(m => m.FullName == selectedValue.FullName);
                var masterName = selectedValue.FullName;

                if (master != null)
                {
                    string query = @"SELECT s.* FROM Services s 
                   JOIN ServicesOfMasters som ON s.IdService = som.idService
                   JOIN Masters m ON som.idMaster = m.IdMaster 
                   WHERE m.FullName = @masterName";

                    linkedValues = context.Database.SqlQuery<Service>(query, new SqlParameter("@masterName", selectedValue.FullName)).ToList();
                }
            }
            return linkedValues;
        }


        


    }
}
