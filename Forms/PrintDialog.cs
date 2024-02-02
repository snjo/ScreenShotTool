using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
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
    public float ImageScale;
    public bool FitToPage;
    private System.Drawing.Bitmap? image;
    string preferredPaperSize = "A4"; // set with settings later?

    private Print printer;

    public PrintDialog(Print print, Bitmap? image)
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

        ImageScale = 100f;
        this.image = image;
        UpdatePreview();
    }

    

    private void UpdatePreview()
    {
        if (image != null)
        {
            pictureBoxPreview.Image.DisposeAndNull();
            pictureBoxPreview.Image = printer.CreatePrintImage(image, pictureBoxPreview.Size, ImageScale, FitToPage);
        }
    }

    private void comboBoxPrinters_TextChanged(object sender, EventArgs e)
    {
        PrinterName = comboBoxPrinters.Text;
    }

    private void comboBoxPaper_TextChanged(object sender, EventArgs e)
    {
        PaperSize = comboBoxPaper.Text;
    }
    private void UpdateMargins(object sender, EventArgs e)
    {
        printer.MarginLeftPercent = (float)numericMarginLeft.Value;
        printer.MarginRightPercent = (float)numericMarginRight.Value;
        printer.MarginTopPercent = (float)numericMarginTop.Value;
        printer.MarginBottomPercent = (float)numericMarginBottom.Value;
        UpdatePreview();
    }

    private void numericImageScale_ValueChanged(object sender, EventArgs e)
    {
        ImageScale = (float)numericImageScale.Value;
        UpdatePreview();
    }

    private void checkBoxFitToPage_CheckedChanged(object sender, EventArgs e)
    {
        FitToPage = checkBoxFitToPage.Checked;
        UpdatePreview();
    }
}
