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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using BeautySalon.Pages;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        AzaleaDBEntities db;
        public ClientsPage()
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
        }

        //Кнопка для перехода на страницу добавления
        private void btnAddClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsAddWindow winAddClients = new ClientsAddWindow(null);
            winAddClients.ShowDialog();
        }

        //Код для обновления данных на странице
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridClients.ItemsSource = db.Clients.ToList();
            }
        }

        //Код для поиска клиентов по ФИО
        private void tbClientsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var clients = db.Clients.ToList().Where(s => s.FullName.ToLower().Contains(tbClientsSearch.Text.ToLower())).ToList();

            if (clients.Count == 0)
            {
                MessageBox.Show("Нет результатов поиска");
            }
            else
            {
                dGridClients.ItemsSource = clients;
            }
        }

        //Кнопка для удаления данных
        private void btnDeleteClients_Click(object sender, RoutedEventArgs e)
        {
            var clientsForRemoving = dGridClients.SelectedItems.Cast<Client>().ToList();

            if (clientsForRemoving.Count() != 0)
            { 
                if (MessageBox.Show($"Вы уверены что хотите удалить {clientsForRemoving.Count()}  элементов?", "Внимание!", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Clients.RemoveRange(clientsForRemoving);
                        db.SaveChanges();
                        MessageBox.Show("Данные удалены!");

                        dGridClients.ItemsSource = db.Clients.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали данные для удаления!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Кнопка для передачи данных для редактирования
        private void btnEditClients_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dGridClients.SelectedItem;
            if(selectedItem == null)
            {
                MessageBox.Show("Вы не выделили клиента для реактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);

            }
            else
            {
                ClientsAddWindow winEditClients = new ClientsAddWindow(selectedItem as Client);
                winEditClients.ShowDialog();
            }
            
        }

        //Кнопка для экспорта данных в Excel
        private void btnToExcel_Click(object sender, RoutedEventArgs e)
        {
            var allClients = db.Clients.ToList().OrderBy(p => p.FullName).ToList();

            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            ExcelApp.Application.Workbooks.Add(Type.Missing);

            ExcelApp.Cells[1][1] = "ФИО";
            ExcelApp.Cells[2][1] = "Телефон";
            ExcelApp.Cells[3][1] = "Адрес";
            ExcelApp.Cells[4][1] = "Почта";
            for (int i = 0; i < allClients.Count; i++)
            {
                ExcelApp.Cells[i + 2, 1] = allClients[i].FullName;
                ExcelApp.Cells[i + 2, 2] = allClients[i].Phone;
                ExcelApp.Cells[i + 2, 3] = allClients[i].Address;
                ExcelApp.Cells[i + 2, 4] = allClients[i].Email;

                Excel.Range rangeBorders = ExcelApp.Range[ExcelApp.Cells[1][1], ExcelApp.Cells[4][1]];
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
            var allClients = AzaleaDBEntities.getContext().Clients.ToList();
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            
            Word.Paragraph nameParagraph = document.Paragraphs.Add();
            Word.Range nameRange = nameParagraph.Range;
            nameRange.Text = "Все клиенты салона";
            nameParagraph.set_Style("Заголовок");
            nameRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table clientsTable = document.Tables.Add(tableRange, allClients.Count() + 1, 4);
            clientsTable.Borders.InsideLineStyle = clientsTable.Borders.OutsideLineStyle
            = Word.WdLineStyle.wdLineStyleSingle;
            clientsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range cellRange;

            cellRange = clientsTable.Cell(1, 1).Range;
            cellRange.Text = "ФИО";
            cellRange = clientsTable.Cell(1, 2).Range;
            cellRange.Text = "Телефон";
            cellRange = clientsTable.Cell(1, 3).Range;
            cellRange.Text = "Адрес";
            cellRange = clientsTable.Cell(1, 4).Range;
            cellRange.Text = "Почта";
            clientsTable.Rows[1].Range.Bold = 1;
            clientsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            for (int i = 0; i < allClients.Count(); i++)
            { 
                  var currentClients = allClients[i];

                cellRange = clientsTable.Cell(i + 2, 1).Range;
                cellRange.Text = currentClients.FullName;

                cellRange = clientsTable.Cell(i + 2, 2).Range;
                cellRange.Text = currentClients.Phone;

                cellRange = clientsTable.Cell(i + 2, 3).Range;
                cellRange.Text = currentClients.Address;

                cellRange = clientsTable.Cell(i + 2, 4).Range;
                cellRange.Text = currentClients.Email;
            }
            application.Visible = true;
            document.SaveAs2(@"Клиенты_Azalea.docx");
            document.SaveAs2(@"Клиенты_Azalea.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }

        private void btnMoreClient_Click(object sender, RoutedEventArgs e)
        {
            
          
            MoreInfoClientWindow window = new MoreInfoClientWindow((sender as Button).DataContext as Client);
            window.ShowDialog();
        }
    }
}
