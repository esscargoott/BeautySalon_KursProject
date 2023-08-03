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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizationPage());
            Manage.MainFrame = MainFrame;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Manage.MainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
                btnClients.Visibility = Visibility.Visible;
                btnServices.Visibility = Visibility.Visible;
                btnMasters.Visibility = Visibility.Visible;
                btnSchedule.Visibility = Visibility.Visible;
                btnStatistic.Visibility = Visibility.Visible;
                btnHistory.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Collapsed;
                btnClients.Visibility = Visibility.Collapsed;
                btnServices.Visibility = Visibility.Collapsed;
                btnMasters.Visibility = Visibility.Collapsed;
                btnSchedule.Visibility = Visibility.Collapsed;
                btnStatistic.Visibility = Visibility.Collapsed;
                btnHistory.Visibility = Visibility.Collapsed;
                btnExit.Visibility = Visibility.Collapsed;
            }
        }

        private void hypClients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientsPage());
        }

        private void hypServices_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServicesPage());
        }

        private void hypMasters_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MastersPage());
        }

        private void hypSchedule_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SchedulePage());
        }

        private void hypStatistic_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StatisticPage());
        }

        private void hypBack_Click(object sender, RoutedEventArgs e)
        {
            Manage.MainFrame.GoBack();
        }

        private void hypHistory_Click(object sender, RoutedEventArgs e)
        {
            Manage.MainFrame.Navigate(new HistoryPage());
        }

        private void hypExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
