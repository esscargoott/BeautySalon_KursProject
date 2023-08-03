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

namespace BeautySalon.Pages
{
    /// <summary>
    /// Логика взаимодействия для MoreInfoClientWindow.xaml
    /// </summary>
    public partial class MoreInfoClientWindow : Window
    {
        private Client _currentClient = new Client();
        public MoreInfoClientWindow(Client selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null)
            { _currentClient = selectedClient; }
            DataContext = _currentClient;
        }
    }
}
