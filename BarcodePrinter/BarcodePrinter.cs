using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace BarcodePrinter
{
    public class MyBarcodePrinter1
    {
        private string number;
        private PrintDocument printDocument;
        private Brush brush = Brushes.Black;
        private Font fntTitle = new Font("verdana", 8, FontStyle.Bold);
        private Font fntName = new Font("verdana", 6);
        private Font fntNumber = new Font("verdana", 6);
        public MyBarcodePrinter1(string number, string printerName)
        {
            this.number = number;
            printDocument = new PrintDocument();
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocument.PrinterSettings.PrinterName = printerName;
            printDocument.DefaultPageSettings.PaperSize = new PaperSize(null, 138, 118);
        }

        public void Print()
        {
            this.printDocument.Print();
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var writer = new ZXing.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.CODE_128;
            writer.Options.Width = 138;
            writer.Options.Height = 20;
            writer.Options.PureBarcode = true;
            var bm = writer.Write(number);
            g.DrawImage(bm, 0, 78, 138, 15);

            number = "序号:" + number;
            g.DrawString(number, fntNumber, Brushes.Black, 12, 52);
        }
        //private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    var g = e.Graphics;
        //    var titleM = g.MeasureString("样本保存液", fntTitle);
        //    var titleX = (138f - titleM.Width) / 2;
        //    g.DrawString("样本保存液", fntTitle, Brushes.Black, titleX, 8);
        //    g.DrawString("姓名:", fntName, Brushes.Black, 5, 30);
        //    g.DrawString("编号:", fntName, Brushes.Black, 75, 30);
        //    g.DrawString("采样时间:", fntName, Brushes.Black, 5, 50);
        //    var writer = new ZXing.BarcodeWriter();
        //    writer.Format = ZXing.BarcodeFormat.CODE_128;
        //    writer.Options.Width = 138;
        //    writer.Options.Height = 20;
        //    writer.Options.PureBarcode = true;
        //    var bm = writer.Write(number);
        //    g.DrawImage(bm, 0, 65, 138, 17);
        //    var numberM = g.MeasureString(number, fntNumber);
        //    var numberX = (138f - numberM.Width) / 2;
        //    g.DrawString(number, fntNumber, Brushes.Black, numberX, 84);
        //}
    }
    public class MyBarcodePrinter2
    {
        private string number;
        private PrintDocument printDocument;
        private Brush brush = Brushes.Black;
        private Font fntTitle = new Font("verdana", 8, FontStyle.Bold);
        private Font fntName = new Font("verdana", 6);
        private Font fntNumber = new Font("verdana", 6);
        public MyBarcodePrinter2(string number, string printerName)
        {
            this.number = number;
            printDocument = new PrintDocument();
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocument.PrinterSettings.PrinterName = printerName;
            printDocument.DefaultPageSettings.PaperSize = new PaperSize(null, 138, 118);
        }

        public void Print()
        {
            this.printDocument.Print();
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var titleM = g.MeasureString("样本保存液", fntTitle);
            var titleX = (138f - titleM.Width) / 2;
            g.DrawString("样本保存液", fntTitle, Brushes.Black, titleX, 8);
            g.DrawString("姓名:", fntName, Brushes.Black, 5, 30);
            g.DrawString("编号:", fntName, Brushes.Black, 75, 30);
            g.DrawString("采样时间:", fntName, Brushes.Black, 5, 50);
            var writer = new ZXing.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.CODE_128;
            writer.Options.Width = 138;
            writer.Options.Height = 20;
            writer.Options.PureBarcode = true;
            var bm = writer.Write(number);
            g.DrawImage(bm, 0, 65, 138, 17);
            var numberM = g.MeasureString(number, fntNumber);
            var numberX = (138f - numberM.Width) / 2;
            g.DrawString(number, fntNumber, Brushes.Black, numberX, 82);
        }
    }
}
