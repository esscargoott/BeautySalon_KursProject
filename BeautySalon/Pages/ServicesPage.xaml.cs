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



namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        AzaleaDBEntities db;
        public ServicesPage()
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
            dGridServices.ItemsSource = db.Services.ToList();

        }

        //Кнопка для перехода на страницу добавления
        private void btnAddServices_Click(object sender, RoutedEventArgs e)
        {
            ServicesAddWindow winAddServices = new ServicesAddWindow();
            winAddServices.ShowDialog();
        }

        //Код для обновления данных на странице
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dGridServices.ItemsSource = db.Services.ToList();
            }
        }

        //Код для поиска услуг по названию
        private void tbServices_TextChanged(object sender, TextChangedEventArgs e)
        {
            var services = db.Services.ToList().Where(n => n.Name.ToLower().Contains(tbServicesSearch.Text.ToLower())).ToList();

            if (services.Count == 0)
            {
                MessageBox.Show("Нет результатов поиска");
            }
            else
            {
                dGridServices.ItemsSource = services;
            }
        }

        //Кнопка для удаления данных
        private void btnDeleteServices_Click(object sender, RoutedEventArgs e)
        {
            var serviceForRemoving = dGridServices.SelectedItems.Cast<Service>().ToList();
            if (serviceForRemoving.Count() != 0)
            {
                if (MessageBox.Show($"Вы уверены что хотите удалить {serviceForRemoving.Count()}  элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Services.RemoveRange(serviceForRemoving);
                        db.SaveChanges();
                        MessageBox.Show("Данные удалены!");

                        dGridServices.ItemsSource = db.Services.ToList();
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

        //Кнопка для экспорта данных в Excel
        private void btnToExcel_Click(object sender, RoutedEventArgs e)
        {
            var allServices = db.Services.ToList().OrderBy(p => p.Name).ToList();

            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            ExcelApp.Application.Workbooks.Add(Type.Missing);



            ExcelApp.Cells[1][1] = "Название";
            ExcelApp.Cells[2][1] = "Описание";
            ExcelApp.Cells[3][1] = "Стоимость";
            for (int i = 0; i < allServices.Count; i++)
            {
                ExcelApp.Cells[i + 2, 1] = allServices[i].Name;
                ExcelApp.Cells[i + 2, 2] = allServices[i].Description;
                ExcelApp.Cells[i + 2, 3] = allServices[i].Price;

                Excel.Range rangeBorders = ExcelApp.Range[ExcelApp.Cells[1][1], ExcelApp.Cells[3][1]];
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
            var allServices = AzaleaDBEntities.getContext().Services.ToList();

            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph nameParagraph = document.Paragraphs.Add();
            Word.Range nameRange = nameParagraph.Range;
            nameRange.Text = "Прайс-лист";
            nameParagraph.set_Style("Заголовок");
            nameRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table servicesTable = document.Tables.Add(tableRange, allServices.Count() + 1, 3);
            servicesTable.Borders.InsideLineStyle = servicesTable.Borders.OutsideLineStyle
            = Word.WdLineStyle.wdLineStyleSingle;
            servicesTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range cellRange;

            cellRange = servicesTable.Cell(1, 1).Range;
            cellRange.Text = "Название";
            cellRange = servicesTable.Cell(1, 2).Range;
            cellRange.Text = "Описание";
            cellRange = servicesTable.Cell(1, 3).Range;
            cellRange.Text = "Должность";
            servicesTable.Rows[1].Range.Bold = 1;
            servicesTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            for (int i = 0; i < allServices.Count(); i++)
            {
                var currentServices = allServices[i];

                cellRange = servicesTable.Cell(i + 2, 1).Range;
                cellRange.Text = currentServices.Name;

                cellRange = servicesTable.Cell(i + 2, 2).Range;
                cellRange.Text = currentServices.Description;

                cellRange = servicesTable.Cell(i + 2, 3).Range;
                cellRange.Text = currentServices.Price.ToString();
            }
            application.Visible = true;
            document.SaveAs2(@"Прайс-лист_Azalea.docx");
            document.SaveAs2(@"Прайс-лист_Azalea.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }
    }
}
