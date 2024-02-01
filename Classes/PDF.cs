using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Validate platform compatibility
namespace ScreenShotTool
{
    // from https://craftmypdf.com/blog/5-ways-to-generate-pdfs-with-c-sharp/
    public class PDF
    {
        private System.ComponentModel.Container components;
        private System.Windows.Forms.Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;
        PrintDocument printDocument;

        public void Save(string filename)
        {
            printDocument = new();
            //document.PrintPage += new PrintPageEventHandler(OnPrintPage);
            printDocument.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            printDocument.Print();
        }

        public void SelectPrinter(string printerName)
        {
            if (printDocument == null)
            {
                printDocument = new PrintDocument();
            }
            var printerList = PrinterSettings.InstalledPrinters;
            string defaultPrinter = string.Empty;
            foreach (var printer in printerList)
            {
                Debug.WriteLine("Printer: *" + printer + "*");
            }
            printDocument.PrinterSettings.PrinterName = printerName;
            IsPrinterDefault();
            if (printDocument.PrinterSettings.IsValid)
            {
                Debug.WriteLine("Printer settings are valid");
            }
            else
            {
                Debug.WriteLine("Printer settings are NOT valid");
            }
        }

        public void SelectDefaultPrinter()
        {
            printDocument.Dispose();
            printDocument = new PrintDocument();
            IsPrinterDefault();
        }

        public bool IsPrinterDefault()
        {
            if (printDocument.PrinterSettings.IsDefaultPrinter)
            {
                Debug.WriteLine("Printer is default");
                return true;
            }
            else
            {
                Debug.WriteLine("Printer is not default");
                return true;
            }
        }

        private static void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null)
            {
                Debug.WriteLine("Error printing to PDF, e.Graphics is null");
                return;
            }
            e.Graphics.DrawString("Hello, World!", new Font("Arial", 20), Brushes.Black, 0, 0);
        }

        public void SaveTextFileToPDF(string file)//printButton_Click(object sender, EventArgs e)
        {
            try
            {
                streamToPrint = new StreamReader
                   (file);
                try
                {
                    printFont = new Font("Arial", 10);
                    printDocument.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
                    printDocument.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count *
                   printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }
    }
}
