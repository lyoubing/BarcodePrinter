using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarcodePrinter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToList();
            var styles = new List<string> { "十混一", "五混一" };
            this.DataContext = App.Settings;
            InitializeComponent();
            cbLabelPrinter.ItemsSource = printers;
            cbLabelStyle.ItemsSource = styles;
        }

        private void btnAutoPrint_Click(object sender, RoutedEventArgs e)
        {
            Print(App.Settings.LastMonth, App.Settings.LastNumber + 1, App.Settings.BatchNumber);
            App.Settings.LastNumber += App.Settings.BatchNumber;
            CheckMonthSeq();
            txtCurentMonth.Text = App.Settings.LastMonth.ToString();
            txtCurrentSeq.Text = (App.Settings.LastNumber + 1).ToString();
            App.Settings.Save();
        }


        private void cbLabelPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Settings.Save();
        }

        private void btnFixPrint_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFixMonth.Text) || string.IsNullOrEmpty(txtFixSeq.Text) || string.IsNullOrEmpty(txtFixPrintCount.Text))
            {
                MessageBox.Show("打印前请先指定月序,起始序号和连续打印数量.");
                return;
            }
            var month = int.Parse(txtFixMonth.Text.Trim());
            var seq = int.Parse(txtFixSeq.Text.Trim());
            var count = int.Parse(txtFixPrintCount.Text.Trim());
            if(count == 0)
            {
                MessageBox.Show("连续打印数量不能为0.");
                return;
            }
            Print(month, seq, count);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckMonthSeq();
            txtCurentMonth.Text = App.Settings.LastMonth.ToString();
            txtCurrentSeq.Text = (App.Settings.LastNumber + 1).ToString();
            txtAutoPrintCount.Text = App.Settings.BatchNumber.ToString();
        }
        private void Print(int month, int startNumber, int count)
        {
            if(string.IsNullOrEmpty(App.Settings.LabelPrinter))
            {
                MessageBox.Show("打印前请先选择标签打印机.");
                return;
            }
            var first = 2000000000L + month * 10000000 + startNumber;

            for(int i = 0; i < count;i++)
            {
                var seq = first + i;
                var number = CRCBuilder.Make(seq);
                if (App.Settings.LabelStyle == 0)
                {
                    var printer = new MyBarcodePrinter1(number, App.Settings.LabelPrinter);
                    printer.Print();
                }
                else if(App.Settings.LabelStyle == 1)
                {
                    var printer = new MyBarcodePrinter2(number, App.Settings.LabelPrinter);
                    printer.Print();
                }
                else
                {
                    var printer = new MyBarcodePrinter3(number, App.Settings.LabelPrinter);
                    printer.Print();
                }
            }
        }
        private  void CheckMonthSeq()
        {
            var monthSeq = (DateTime.Now.Year - 2020) * 12 + DateTime.Now.Month;
            if (App.Settings.LastMonth != monthSeq)
            {
                App.Settings.LastMonth = monthSeq;
                App.Settings.LastNumber = 0;
            }
        }

        private void txtAutoPrintCount_LostFocus(object sender, RoutedEventArgs e)
        {
            App.Settings.BatchNumber = int.Parse(txtAutoPrintCount.Text.Trim());
            App.Settings.Save();
        }

        private void OnlyNumeric_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers != ModifierKeys.None) e.Handled = true;
            else if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Back)) e.Handled = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            App.Settings.Save();
        }
    }
}
