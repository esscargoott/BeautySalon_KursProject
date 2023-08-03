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
using System.Windows.Forms.DataVisualization.Charting;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        private AzaleaDBEntities _context = new AzaleaDBEntities();
        public StatisticPage()
        {
            InitializeComponent();
          
            ChartPopularServices.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Services")
            {
                IsValueShownAsLabel = true
            };
            ChartPopularServices.Series.Add(currentSeries);


            ChartPopularClient.ChartAreas.Add(new ChartArea("Main"));
            var currentSeriesClients = new Series("Clients")
            {
                IsValueShownAsLabel = true
            };
            ChartPopularClient.Series.Add(currentSeriesClients);


            ChartIncomeClient.ChartAreas.Add(new ChartArea("Main"));
            var currentSeriesClientsIncome = new Series("ClientsIncome")
            {
                IsValueShownAsLabel = true
            };
            ChartIncomeClient.Series.Add(currentSeriesClientsIncome);


            ChartIncomeMasters.ChartAreas.Add(new ChartArea("Main"));
            var currentSeriesMastersIncome = new Series("MastersIncome")
            {
                IsValueShownAsLabel = true
            };
            ChartIncomeMasters.Series.Add(currentSeriesMastersIncome);
        }
             
        private void ChartType_Click(object sender, RoutedEventArgs e)
        {
            Series currentSeries;
            var servicesList = _context.Services.ToList();

            switch (((RadioButton)sender).Name)
            {
                case "ColumnRadio":
                    currentSeries = ChartPopularServices.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Column;
                    currentSeries.Points.Clear();

                    foreach (var service in servicesList)
                    {
                        currentSeries.Points.AddXY(service.Name,
                        _context.Schedules.Where(p => p.IdService == service.IdService).Count());
                    }
                    break;

                case "BarRadio":
                    currentSeries = ChartPopularServices.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Bar;
                    currentSeries.Points.Clear();

                    foreach (var service in servicesList)
                    {
                        currentSeries.Points.AddXY(service.Name,
                        _context.Schedules.Where(p => p.IdService == service.IdService).Count());
                    }
                    break;

                case "PieRadio":
                    currentSeries = ChartPopularServices.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Pie;
                    currentSeries.Points.Clear();

                    foreach (var service in servicesList)
                    {
                        currentSeries.Points.AddXY(service.Name,
                        _context.Schedules.Where(p => p.IdService == service.IdService).Count());
                    }
                    break;

                case "LineRadio":
                    currentSeries = ChartPopularServices.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Line;
                    currentSeries.Points.Clear();

                    foreach (var service in servicesList)
                    {
                        currentSeries.Points.AddXY(service.Name,
                        _context.Schedules.Where(p => p.IdService == service.IdService).Count());
                    }
                    break;

                default:
                    break;
            }
        }

        private void ChartType1_Click(object sender, RoutedEventArgs e)
        {
            Series currentSeries;
            var clientsList = _context.Clients.ToList();

            switch (((RadioButton)sender).Name)
            {
                case "ColumnRadio1":
                    currentSeries = ChartPopularClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Column;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        currentSeries.Points.AddXY(client.FullName,
                        _context.Schedules.Where(c => c.IdClient == client.IdClient).Count());
                    }
                    break;

                case "BarRadio1":
                    currentSeries = ChartPopularClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Bar;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        currentSeries.Points.AddXY(client.FullName,
                       _context.Schedules.Where(c => c.IdClient == client.IdClient).Count());
                    }
                    break;

                case "PieRadio1":
                    currentSeries = ChartPopularClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Pie;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        currentSeries.Points.AddXY(client.FullName,
                       _context.Schedules.Where(c => c.IdClient == client.IdClient).Count());
                    }
                    break;

                case "LineRadio1":
                    currentSeries = ChartPopularClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Line;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        currentSeries.Points.AddXY(client.FullName,
                       _context.Schedules.Where(c => c.IdClient == client.IdClient).Count());
                    }
                    break;

                default:
                    break;
            }
        }

        private void ChartType2_Click(object sender, RoutedEventArgs e)
        {
            Series currentSeries;
            var clientsList = _context.Clients.ToList();

            switch (((RadioButton)sender).Name)
            {
                case "ColumnRadio2":
                    currentSeries = ChartIncomeClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Column;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        var result = (from c in _context.Clients
                                      join ss in _context.Schedules on c.IdClient equals ss.IdClient into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where c.IdClient == client.IdClient && (se == null || se.Price > 0)
                                      group se by c.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                case "BarRadio2":
                    currentSeries = ChartIncomeClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Bar;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        var result = (from c in _context.Clients
                                      join ss in _context.Schedules on c.IdClient equals ss.IdClient into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where c.IdClient == client.IdClient && (se == null || se.Price > 0)
                                      group se by c.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                case "PieRadio2":
                    currentSeries = ChartIncomeClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Pie;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        var result = (from c in _context.Clients
                                      join ss in _context.Schedules on c.IdClient equals ss.IdClient into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where c.IdClient == client.IdClient && (se == null || se.Price > 0)
                                      group se by c.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                case "LineRadio2":
                    currentSeries = ChartIncomeClient.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Line;
                    currentSeries.Points.Clear();

                    foreach (var client in clientsList)
                    {
                        var result = (from c in _context.Clients
                                      join ss in _context.Schedules on c.IdClient equals ss.IdClient into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where c.IdClient == client.IdClient && (se == null || se.Price > 0)
                                      group se by c.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                default:
                    break;
            }
        }


        private void ChartType3_Click(object sender, RoutedEventArgs e)
        {
            Series currentSeries;
            var mastersList = _context.Masters.ToList();

            switch (((RadioButton)sender).Name)
            {
                case "ColumnRadio3":
                    currentSeries = ChartIncomeMasters.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Column;
                    currentSeries.Points.Clear();

                    foreach (var master in mastersList)
                    {
                        var result = (from m in _context.Masters
                                      join ss in _context.Schedules on m.IdMaster equals ss.IdMaster into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where m.IdMaster == master.IdMaster && (se == null || se.Price > 0)
                                      group se by m.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                case "BarRadio3":
                    currentSeries = ChartIncomeMasters.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Bar;
                    currentSeries.Points.Clear();

                    foreach (var master in mastersList)
                    {
                        var result = (from m in _context.Masters
                                      join ss in _context.Schedules on m.IdMaster equals ss.IdMaster into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where m.IdMaster == master.IdMaster && (se == null || se.Price > 0)
                                      group se by m.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                case "PieRadio3":
                    currentSeries = ChartIncomeMasters.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Pie;
                    currentSeries.Points.Clear();

                    foreach (var master in mastersList)
                    {
                        var result = (from m in _context.Masters
                                      join ss in _context.Schedules on m.IdMaster equals ss.IdMaster into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where m.IdMaster == master.IdMaster && (se == null || se.Price > 0)
                                      group se by m.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                case "LineRadio3":
                    currentSeries = ChartIncomeMasters.Series.FirstOrDefault();
                    currentSeries.ChartType = SeriesChartType.Line;
                    currentSeries.Points.Clear();

                    foreach (var master in mastersList)
                    {
                        var result = (from m in _context.Masters
                                      join ss in _context.Schedules on m.IdMaster equals ss.IdMaster into sched
                                      from s in sched.DefaultIfEmpty()
                                      join serv in _context.Services on s.IdService equals serv.IdService into service
                                      from se in service.DefaultIfEmpty()
                                      where m.IdMaster == master.IdMaster && (se == null || se.Price > 0)
                                      group se by m.FullName into g
                                      select new
                                      {
                                          FullName = g.Key,
                                          total_income = g.Sum(s => s == null ? 0 : s.Price)
                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            currentSeries.Points.AddXY(result.FullName, result.total_income);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(StatisticServicesCount, "Вывод на печать!");
            }
        }

        private void btnPrint4_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual( StatisticMasters, "Вывод на печать!");
            }
           
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(StatisticClients, "Вывод на печать!");
            }
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(StatisticClientsCount, "Вывод на печать!");
            }
        }
    }
}
