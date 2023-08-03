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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();
            adminInfo.Text = "С возвращением,   " + Manage.adminName;
            adminInfo2.Text = Manage.adminPatronymic;
        }

        //Метод для обновления данных на странице
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                AzaleaDBEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridHistory.ItemsSource = AzaleaDBEntities.getContext().LoginHistories.ToList();
            }
        }
    }
}
