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
    /// Логика взаимодействия для MastersPage.xaml
    /// </summary>
    public partial class MastersPage : Page
    {
        AzaleaDBEntities db;
        public MastersPage()
        {
            InitializeComponent();
            db = new AzaleaDBEntities();
        }

        //Кнопка для перехода на страницу добавления
        private void btnAddMasters_Click(object sender, RoutedEventArgs e)
        {
            MastersAddWindow winAddMasters = new MastersAddWindow(null);
            winAddMasters.ShowDialog();
        }

        //Код для обнновления данных на странице
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dGridMasters.ItemsSource = db.Masters.ToList();
        }

        //Код для поиска мастеров по ФИО
        private void tbMastersSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var masters = db.Masters.ToList().Where(s => s.FullName.ToLower().Contains(tbMastersSearch.Text.ToLower())).ToList();

            if (masters.Count == 0)
            {
                MessageBox.Show("Нет результатов поиска");
            }
            else
            {
                dGridMasters.ItemsSource = masters;
            }
        }

        
        //Кнопка для удаления данных
        private void btnDeleteMasters_Click(object sender, RoutedEventArgs e)
        {
            var mastersForRemoving = dGridMasters.SelectedItems.Cast<Master>().ToList();

            if (mastersForRemoving.Count() != 0)
            {
                if (MessageBox.Show($"Вы уверены что хотите удалить {mastersForRemoving.Count()}  элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Masters.RemoveRange(mastersForRemoving);
                        db.SaveChanges();
                        MessageBox.Show("Данные удалены!");

                        dGridMasters.ItemsSource = db.Masters.ToList();
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
        private void btnEditMasters_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dGridMasters.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Вы не выделили мастера для реактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                MastersAddWindow winEditMasters = new MastersAddWindow(selectedItem as Master);
                winEditMasters.ShowDialog();
            }

        }

        //Кнопка для экспорта данных в Excel
        private void btnToExcel_Click(object sender, RoutedEventArgs e)
        {
            var allMasters = db.Masters.ToList().OrderBy(p => p.FullName).ToList();

            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            ExcelApp.Application.Workbooks.Add(Type.Missing);

            ExcelApp.Cells[1][1] = "ФИО";
            ExcelApp.Cells[2][1] = "Телефон";
            ExcelApp.Cells[3][1] = "Адрес";
            ExcelApp.Cells[4][1] = "Должность";
            for (int i = 0; i < allMasters.Count; i++)
            {
                ExcelApp.Cells[i + 2, 1] = allMasters[i].FullName;
                ExcelApp.Cells[i + 2, 2] = allMasters[i].Phone;
                ExcelApp.Cells[i + 2, 3] = allMasters[i].Address;
                ExcelApp.Cells[i + 2, 4] = allMasters[i].JobTitle;

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
            var allMasters = AzaleaDBEntities.getContext().Masters.ToList();

            var application = new Word.Application();
            Word.Document document = application.Documents.Add();


            Word.Paragraph nameParagraph = document.Paragraphs.Add();
            Word.Range nameRange = nameParagraph.Range;
            nameRange.Text = "Все мастера салона";
            nameParagraph.set_Style("Заголовок");
            nameRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table mastersTable = document.Tables.Add(tableRange, allMasters.Count() + 1, 4);
            mastersTable.Borders.InsideLineStyle = mastersTable.Borders.OutsideLineStyle
            = Word.WdLineStyle.wdLineStyleSingle;
            mastersTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range cellRange;

            cellRange = mastersTable.Cell(1, 1).Range;
            cellRange.Text = "ФИО";
            cellRange = mastersTable.Cell(1, 2).Range;
            cellRange.Text = "Телефон";
            cellRange = mastersTable.Cell(1, 3).Range;
            cellRange.Text = "Адрес";
            cellRange = mastersTable.Cell(1, 4).Range;
            cellRange.Text = "Должность";
            mastersTable.Rows[1].Range.Bold = 1;
            mastersTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            for (int i = 0; i < allMasters.Count(); i++)
            {
                var currentMasters = allMasters[i];

                cellRange = mastersTable.Cell(i + 2, 1).Range;
                cellRange.Text = currentMasters.FullName;


                cellRange = mastersTable.Cell(i + 2, 2).Range;
                cellRange.Text = currentMasters.Phone;


                cellRange = mastersTable.Cell(i + 2, 3).Range;
                cellRange.Text = currentMasters.Address;

                cellRange = mastersTable.Cell(i + 2, 4).Range;
                cellRange.Text = currentMasters.JobTitle;
            }
            application.Visible = true;
            document.SaveAs2(@"Мастера_Azalea.docx");
            document.SaveAs2(@"Мастера_Azalea.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }
    }
}
