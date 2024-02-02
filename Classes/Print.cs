using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Validate platform compatibility
namespace ScreenShotTool;

// https://learn.microsoft.com/en-us/dotnet/api/system.drawing.printing?view=dotnet-plat-ext-8.0
// other options: https://craftmypdf.com/blog/5-ways-to-generate-pdfs-with-c-sharp/
public class Print
{
    private PrintDocument printDocument;
    public List<string> InstalledPrinters = new();
    public List<string> PdfPrinters = new();
    readonly string DefaultPrinter;
    public Font Font;
    private StreamReader? streamToPrint;
    private PaperSize? sizeA4;
    public float MarginLeftPercent = 10f;
    public float MarginRightPercent = 5f;
    public float MarginTopPercent = 5f;
    public float MarginBottomPercent = 5f;
    public float ImageScale = 100;
    public bool FitToPage = true;

    public Print()
    {
        printDocument = new PrintDocument();
        InstalledPrinters = PrinterSettings.InstalledPrinters.Cast<string>().ToList();
        DefaultPrinter = GetDefaultPrinterName();
        PdfPrinters = GetPDFPrinters();
        this.Font = new Font("Arial", 10);
        sizeA4 = GetPaperSizeByName("A4");
        if (sizeA4 != null)
        {
            printDocument.DefaultPageSettings.PaperSize = sizeA4;
        }
    }


    public bool SelectPrinter(string printerName)
    {
        printDocument.PrinterSettings.PrinterName = printerName;
        return printDocument.PrinterSettings.IsValid;
    }

    public string GetDefaultPrinterName()
    {
        PrinterSettings settings = new PrinterSettings();
        return settings.PrinterName;
    }

    public void SelectDefaultPrinter()
    {
        SelectPrinter(DefaultPrinter);
    }

    private List<string> GetPDFPrinters()
    {
        return GetPrintersByWildcard("PDF");
    }

    public PaperSize PaperSize
    {
        get
        {
            return printDocument.DefaultPageSettings.PaperSize;
        }
        set
        {
            printDocument.DefaultPageSettings.PaperSize = value;
        }
    }

    public List<PaperSize> GetPaperSizes()
    {
        List<PaperSize> paperSizes = new();
        paperSizes =  printDocument.PrinterSettings.PaperSizes.Cast<PaperSize>().ToList();
        return paperSizes;
    }

    public List<string> GetPaperSizeNames()
    {
        List<PaperSize> paperSizes = GetPaperSizes();
        List<string> paperSizeNames = new();
        foreach (PaperSize paperSize in paperSizes)
        {
            paperSizeNames.Add(paperSize.PaperName);
            //Debug.WriteLine(paperSize.PaperName);
        }
        return paperSizeNames;
    }

    public PaperSize? GetPaperSizeByName(string name)
    {
        List<PaperSize> paperSizes = GetPaperSizes();
        foreach (PaperSize paper in paperSizes)
        {
            if (paper.PaperName == name)
                return paper;
        }
        return null;
    }

    public List<string> GetPrintersByWildcard(string wildcard)
    {
        List<string> result = new();
        foreach (string printer in InstalledPrinters)
        {
            if (printer.Contains(wildcard, StringComparison.InvariantCultureIgnoreCase))
            {
                result.Add(printer);
            }
        }
        return result;
    }

    public bool IsPrinterDefault()
    {
        if (printDocument != null && printDocument.PrinterSettings.IsDefaultPrinter)
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

    private Bitmap? imageToPrint;
    public void PrintImage(Bitmap image)
    {
        imageToPrint = image;
        printDocument.PrintPage += new PrintPageEventHandler(PrintPageImage);
        printDocument.Print();
    }

    public Bitmap CreatePrintImage(Bitmap image, SizeF canvasSize, bool preview, float dpi = 600)
    {
        //float HardDPI = 600;
        float scale = ImageScale / 100f;
        
        SizeF paperSizePixels = new SizeF(PaperSize.Width * dpi / 100f, PaperSize.Height * dpi / 100f);
        float outputRatio = 1;
        if (preview)
        {
            outputRatio = paperSizePixels.Width / canvasSize.Width;
        }
        float paperRatio = paperSizePixels.Width / paperSizePixels.Height;
        Bitmap previewImage = new Bitmap((int)canvasSize.Width, (int)canvasSize.Height);
        Graphics graphics = Graphics.FromImage(previewImage);

        float left = (canvasSize.Width * (MarginLeftPercent / 100f));
        float right = (canvasSize.Width * (MarginRightPercent / 100f));
        float top = (canvasSize.Height * (MarginTopPercent / 100f));
        float bottom = (canvasSize.Height * (MarginBottomPercent / 100f));
        RectangleF marginRect = new RectangleF(left, top, canvasSize.Width - right - left, canvasSize.Height - bottom - top);
        SizeF imgDrawSize = new SizeF(image.Size.Width / outputRatio * scale, image.Size.Height / outputRatio * scale);
        float imageRatio = imgDrawSize.Width / imgDrawSize.Height;
        if (FitToPage)
        {
            if (imgDrawSize.Width > marginRect.Width)
            {

                imgDrawSize.Width = marginRect.Width;
                imgDrawSize.Height = imgDrawSize.Width / imageRatio;
            }
            if (imgDrawSize.Height > marginRect.Height)
            {
                imgDrawSize.Height = marginRect.Height;
                imgDrawSize.Width = imgDrawSize.Height * imageRatio;
            }
        }

        graphics.DrawImage(image, new Rectangle((int)left, (int)top, (int)imgDrawSize.Width, (int)imgDrawSize.Height));
        graphics.Dispose();
        return previewImage;
    }

    private void PrintPageImage(object sender, PrintPageEventArgs ev)
    {
        if (ev.Graphics == null || imageToPrint == null)
        {
            Debug.WriteLine("PrintPageToText: ev Graphics or image is null");
            return;
        }
        float leftMargin = ev.MarginBounds.Left;
        float topMargin = ev.MarginBounds.Top;
        float areaX = (int)printDocument.DefaultPageSettings.PrintableArea.Width;
        float areaY = (int)printDocument.DefaultPageSettings.PrintableArea.Height;
        int dpiX = printDocument.DefaultPageSettings.PrinterResolution.X;
        int dpiY = printDocument.DefaultPageSettings.PrinterResolution.Y;
        float resX = areaX * dpiX / 100f;
        float resY = areaY * dpiY / 100f;

        Bitmap imageOut = CreatePrintImage(imageToPrint, new SizeF(resX, resY), preview: false, dpiX);
        ev.Graphics.DrawImage(imageOut, new Rectangle(0, 0, ev.PageBounds.Width, ev.PageBounds.Height));
        imageOut.Dispose();
        ev.HasMorePages = false;
    }

    public void PrintTextFile(string file)//printButton_Click(object sender, EventArgs e)
    {
        try
        {
            streamToPrint = new StreamReader (file);
            try
            {
                printDocument.PrintPage += new PrintPageEventHandler (PrintPageText);
                printDocument.Print();
            }
            finally
            {
                streamToPrint.Close();
                streamToPrint = null;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // The PrintPage event is raised for each page to be printed.
    private void PrintPageText(object sender, PrintPageEventArgs ev)
    {
        if (streamToPrint == null)
        {
            Debug.WriteLine("PrintPageToText: stream reader streamToPrint is null");
            return;
        }
        if (ev.Graphics == null)
        {
            Debug.WriteLine("PrintPageToText: ev Graphics is null");
            return;
        }

        //ev.PageSettings.PaperSize = printDocument.PrinterSettings.PaperSizes.

        float linesPerPage = 0;
        float yPos = 0;
        int count = 0;
        float leftMargin = ev.MarginBounds.Left;
        float topMargin = ev.MarginBounds.Top;
        string? line = null;
        
        Graphics graphics = ev.Graphics;
        // Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height / Font.GetHeight(graphics);

        // Print each line of the file.
        while (count < linesPerPage &&
           ((line = streamToPrint.ReadLine()) != null))
        {
            yPos = topMargin + (count *
               Font.GetHeight(graphics));
            graphics.DrawString(line, Font, Brushes.Black,
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
