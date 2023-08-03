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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;


namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {

        AzaleaDBEntities db;
        public SchedulePage()
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
            comboSearchClient.ItemsSource = db.Clients.ToList();
            dGridSchedules.ItemsSource = db.Schedules.ToList();
        }
                        
        //Кнопка для добавления записи
        private void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAddWindow winAddSession = new ScheduleAddWindow(null);
            winAddSession.ShowDialog();
        }

        //Кнопка для экспорта данных в Excel
        private void btnToExcel_Click(object sender, RoutedEventArgs e)
        {
            string query = @"USE AzaleaDB 
               select Masters.FullName AS MasterName , Clients.FullName AS ClientName, 
                Services.Name AS ServiceName, Services.Price AS ServicePrice, DateTime AS ScheduleDateTime
                from Schedules join Clients 
                on Schedules.IdClient = Clients.IdClient
                join Masters
                on Schedules.IdMaster = Masters.IdMaster
                join Services
                on Schedules.IdService = Services.IdService
                ";

            string connectionString = @"Data Source=LAPTOP-C9C6IRVS\SQLEXPRESS;Initial Catalog=AzaleaDB;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            var SheduleTable = dataTable.AsEnumerable().Select(row => new {
                MasterName = row.Field<string>("MasterName"),
                ClientName = row.Field<string>("ClientName"),
                ServiceName = row.Field<string>("ServiceName"),
                ServicePrice = row.Field<decimal>("ServicePrice"),
                ScheduleDateTime = row.Field<DateTime>("ScheduleDateTime")

            }).ToList();

            var wSchedules = SheduleTable;

            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            ExcelApp.Application.Workbooks.Add(Type.Missing);

            ExcelApp.Cells[1][1] = "ФИО клиента";
            ExcelApp.Cells[2][1] = "ФИО мастера";
            ExcelApp.Cells[3][1] = "Название услуги";
            ExcelApp.Cells[4][1] = "Стоимость услуги";
            ExcelApp.Cells[5][1] = "Дата проведения";
            for (int i = 0; i < wSchedules.Count; i++)
            {
                var currentSchedule = wSchedules[i]; 

                ExcelApp.Cells[i + 2, 1] = currentSchedule.ClientName;
                ExcelApp.Cells[i + 2, 2] = currentSchedule.MasterName;
                ExcelApp.Cells[i + 2, 3] = currentSchedule.ServiceName;
                ExcelApp.Cells[i + 2, 4] = currentSchedule.ServicePrice;
                ExcelApp.Cells[i + 2, 5] = currentSchedule.ScheduleDateTime;

                Excel.Range rangeBorders = ExcelApp.Range[ExcelApp.Cells[1][1], ExcelApp.Cells[5][1]];
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                ExcelApp.Columns.AutoFit();
                ExcelApp.Rows.AutoFit();
            }
            ExcelApp.Visible = true;
        }

        //Кнопка для экспорта данных в Word
        private void btnToWord_Click(object sender, RoutedEventArgs e)
        {
            string query = @"USE AzaleaDB 
               select Masters.FullName AS MasterName , Clients.FullName AS ClientName, Services.Name AS ServiceName, Services.Price AS ServicePrice, DateTime AS ScheduleDateTime
                from Schedules join Clients 
                on Schedules.IdClient = Clients.IdClient
                join Masters
                on Schedules.IdMaster = Masters.IdMaster
                join Services
                on Schedules.IdService = Services.IdService
                ";

            string connectionString = @"Data Source=LAPTOP-C9C6IRVS\SQLEXPRESS;Initial Catalog=AzaleaDB;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            var SheduleTable = dataTable.AsEnumerable().Select(row => new {
                MasterName = row.Field<string>("MasterName"),
                ClientName = row.Field<string>("ClientName"),
                ServiceName = row.Field<string>("ServiceName"),
                ServicePrice = row.Field<decimal>("ServicePrice"),
                ScheduleDateTime = row.Field<DateTime>("ScheduleDateTime")
            }).ToList();

            var wSchedules = SheduleTable;

            var WordApp = new Word.Application();
            Word.Document document = WordApp.Documents.Add();
            Word.Paragraph nameParagraph = document.Paragraphs.Add();
            Word.Range nameRange = nameParagraph.Range;
            nameRange.Text = "Расписание";
            nameParagraph.set_Style("Заголовок");
            nameRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table shedulesTable = document.Tables.Add(tableRange, wSchedules.Count() + 1, 5);
            shedulesTable.Borders.InsideLineStyle = shedulesTable.Borders.OutsideLineStyle
            = Word.WdLineStyle.wdLineStyleSingle;
            shedulesTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range cellRange;

            cellRange = shedulesTable.Cell(1, 1).Range;
            cellRange.Text = "ФИО клиента";
            cellRange = shedulesTable.Cell(1, 2).Range;
            cellRange.Text = "ФИО мастера";
            cellRange = shedulesTable.Cell(1, 3).Range;
            cellRange.Text = "Название услуги";
            cellRange = shedulesTable.Cell(1, 4).Range;
            cellRange.Text = "Стоимость услуги";
            cellRange = shedulesTable.Cell(1, 5).Range;
            cellRange.Text = "Дата проведения";

            shedulesTable.Rows[1].Range.Bold = 1;
            shedulesTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            for (int i = 0; i < wSchedules.Count(); i++)
            {
                var currentSchedule = wSchedules[i];

                cellRange = shedulesTable.Cell(i + 2, 1).Range;
                cellRange.Text = currentSchedule.ClientName;

                cellRange = shedulesTable.Cell(i + 2, 2).Range;
                cellRange.Text = currentSchedule.MasterName;

                cellRange = shedulesTable.Cell(i + 2, 3).Range;
                cellRange.Text = currentSchedule.ServiceName;

                cellRange = shedulesTable.Cell(i + 2, 4).Range;
                cellRange.Text = currentSchedule.ServicePrice.ToString();

                cellRange = shedulesTable.Cell(i + 2, 5).Range;
                cellRange.Text = currentSchedule.ScheduleDateTime.ToString();
            }
            WordApp.Visible = true;
            document.SaveAs2(@"Расписание_Azalea.docx");
            document.SaveAs2(@"Расписание_Azalea.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }

        //Код для поиска клиентов в таблице
        private void comboSearchClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = new AzaleaDBEntities();
            Client selectedValue = comboSearchClient.SelectedItem as Client;
            if (selectedValue != null)
            {
                var client = context.Clients.FirstOrDefault(m => m.IdClient == selectedValue.IdClient);
                if (client != null)
                {
                    var schedules = context.Schedules
                        .Where(s => s.IdClient == client.IdClient).ToList();
                    if (schedules.Count > 0)
                    {
                        dGridSchedules.ItemsSource = schedules;
                    }
                    else
                    {
                        MessageBox.Show("Нет результатов поиска");
                    }
                }
            }
        }


        private void btnDeleteSchedules_Click(object sender, RoutedEventArgs e)
        {
            var schedulesForRemoving = dGridSchedules.SelectedItems.Cast<Schedule>();

            if (MessageBox.Show($"Вы уверены что хотите удалить {schedulesForRemoving.Count()}  " +
                $"элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    db.Schedules.RemoveRange(schedulesForRemoving);
                    db.SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dGridSchedules.ItemsSource = db.Schedules.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
            }

        }

        public void btnEditSchedules_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = dGridSchedules.SelectedItem;

            if (selectedValue == null)
            {
                MessageBox.Show("Вы не выделили расписание для реактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                ScheduleAddWindow winEditSession = new ScheduleAddWindow(selectedValue as Schedule);
                winEditSession.ShowDialog();

            }        
        }
    }
}
