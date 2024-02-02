using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility

public partial class PrintDialog : Form
{
    public string PrinterName;
    public string PaperSize;
    public float MarginLeft;
    public float MarginTop;
    public float MarginRight;
    public float MarginBottom;
    //public float ImageScale;
    public bool FitToPage;
    private System.Drawing.Bitmap? image;
    string preferredPaperSize = "A4"; // set with settings later?
    bool creationComplete = false;

    private Print printer;

    public PrintDialog(Print print, Bitmap? image, float marginLeft = 5f, float marginRight = 5f, float marginTop = 5f, float marginBottom = 5f)
    {
        InitializeComponent();
        printer = print;
        //MarginLeft = print.MarginLeftPercent;
        //MarginTop = print.MarginTopPercent;
        //MarginRight = print.MarginRightPercent;
        //MarginBottom = print.MarginBottomPercent;
        PrinterName = print.GetDefaultPrinterName();
        comboBoxPrinters.Items.AddRange(print.InstalledPrinters.ToArray());
        comboBoxPrinters.Text = PrinterName;
        comboBoxPaper.Items.AddRange(print.GetPaperSizeNames().ToArray());
        if (print.GetPaperSizeNames().Contains(preferredPaperSize))
        {
            comboBoxPaper.Text = preferredPaperSize; // set with settings later?
            PaperSize = preferredPaperSize;
        }
        else
        {
            string firstPaper = print.GetPaperSizeNames().First();
            comboBoxPaper.Text = firstPaper;
            PaperSize = firstPaper;
        }

        //ImageScale = 100f;
        this.image = image;
        //MarginLeft = marginLeft;
        //MarginRight = marginRight;
        //MarginTop = marginTop;
        //MarginBottom = marginBottom;
        numericMarginLeft.Value = (decimal)marginLeft;
        numericMarginRight.Value = (decimal)marginRight;
        numericMarginTop.Value = (decimal)marginTop;
        numericMarginBottom.Value = (decimal)marginBottom;
        creationComplete = true;
        UpdatePrinterValues();
        UpdatePreview();
    }

    private void UpdatePrinterValues()
    {
        if (creationComplete == false) return;
        printer.MarginLeftPercent = (float)numericMarginLeft.Value;
        printer.MarginRightPercent = (float)numericMarginRight.Value;
        printer.MarginTopPercent = (float)numericMarginTop.Value;
        printer.MarginBottomPercent = (float)numericMarginBottom.Value;
        //printer.DPI = (float)numericDPI.Value;
        printer.FitToPage = checkBoxFitToPage.Checked;
        printer.ImageScale = (float)numericImageScale.Value;
        if (printer.SelectPrinter(PrinterName) == false)
        {
            Debug.WriteLine($"Invalid printer name {PrinterName}, not set");
        }
        PaperSize? paperSize = printer.GetPaperSizeByName(PaperSize);
        if (paperSize != null )
        {
            printer.PaperSize = paperSize;
            Debug.WriteLine($"Paper size = {printer.PaperSize.PaperName}");
        }
        else
        {
            Debug.WriteLine("Invalind paper size");
        }
    }

    private void UpdatePreview()
    {
        UpdatePrinterValues();
        if (image != null)
        {
            pictureBoxPreview.Image.DisposeAndNull();
            pictureBoxPreview.Image = printer.CreatePrintImage(image, pictureBoxPreview.Size, preview: true);
        }
    }

    private void comboBoxPrinters_TextChanged(object sender, EventArgs e)
    {
        PrinterName = comboBoxPrinters.Text;
        UpdatePreview();
    }

    private void comboBoxPaper_TextChanged(object sender, EventArgs e)
    {
        PaperSize = comboBoxPaper.Text;
        UpdatePreview();
    }
    private void NumericChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void numericImageScale_ValueChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxFitToPage_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }
}
