using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotTool.Forms
{
    [SupportedOSPlatform("windows")]
#pragma warning disable CA1416 // Validate platform compatibility
    public partial class ScreenshotEditor : Form
    {
        Image? originalImage;
        Image? overlayImage;
        Graphics? overlayGraphics;
        Pen linePen = new Pen(new SolidBrush(Color.Green), 2);
        Brush lineBrush = new SolidBrush(Color.Black);
        Brush fillBrush = new SolidBrush(Color.Gray);
        List<GraphicSymbol> symbols = new List<GraphicSymbol>();

        public ScreenshotEditor(string? file = null)
        {
            InitializeComponent();
            pictureBoxOverlay.Parent = pictureBoxOriginal;
            if (file != null)
            {
                LoadImage(file);
            }
        }



        public void LoadImage(string filename)
        {
            if (File.Exists(filename))
            {
                Debug.WriteLine("Loading file: " + filename);

                CreateOverlay();
                originalImage = Image.FromFile(filename);
                pictureBoxOriginal.Image = originalImage;
            }
        }

        public void SaveImage(string filename)
        {
            //originalImage.
            if (originalImage != null)
            {
                Graphics saveGraphic = Graphics.FromImage(originalImage);
                DrawElements(saveGraphic);
                originalImage.Save(filename);
                saveGraphic.Dispose();
            }
        }

        public void CopyToClipboard()
        {
            if (originalImage != null)
            {
                Bitmap outImage = new Bitmap(originalImage);
                Graphics saveGraphic = Graphics.FromImage(outImage);
                DrawElements(saveGraphic);
                Clipboard.SetImage(outImage);
                saveGraphic.Dispose();
            }
        }

        private void CreateOverlay()
        {
            if (originalImage != null)
            {
                Debug.WriteLine("Creating overlay");
                if (overlayImage != null)
                {
                    overlayImage.Dispose();
                }
                if (overlayGraphics != null)
                {
                    overlayGraphics.Dispose();
                }
                overlayImage = new Bitmap(originalImage.Width, originalImage.Height);
                overlayGraphics = Graphics.FromImage(overlayImage);
                
                //overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //overlayGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text

                pictureBoxOverlay.Image = overlayImage;
                pictureBoxOverlay.Width = pictureBoxOriginal.Width;
                pictureBoxOverlay.Height = pictureBoxOriginal.Height;
            }
            else
            {
                Debug.WriteLine("Create overlay failed, original image is null");
                //DisposeAndNull(overlayImage);
                //disposeAndNull(overlayGraphics);
            }
        }

        private void UpdateOverlay(GraphicSymbol? temporarySymbol = null)
        {
            if (overlayImage == null || overlayGraphics == null)
            {
                CreateOverlay();
            }
            if (overlayImage != null && overlayGraphics != null)
            {
                pictureBoxOverlay.Image = DrawOverlay(pictureBoxOverlay.Image, temporarySymbol);
            }
            else
            {
                Debug.WriteLine($"Error, overlay exists: image {overlayImage != null} / graphics {overlayGraphics != null}");
            }
        }

        private Image DrawOverlay(Image img, GraphicSymbol? temporarySymbol = null)
        {
            img = new Bitmap(this.Width, this.Height);
            disposeAndNull(overlayGraphics);
            overlayGraphics = Graphics.FromImage(img);

            DrawElements(overlayGraphics, temporarySymbol);

            return img;
        }

        private void DrawElements(Graphics graphic, GraphicSymbol? temporarySymbol = null)
        {
            foreach (GraphicSymbol symbol in symbols)
            {
                symbol.DrawSymbol(graphic);
            }
            if (temporarySymbol != null)
            {
                temporarySymbol.DrawSymbol(graphic);
            }
        }

        private void DisposeAndNull(Bitmap? bitmap)
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
        }

        private void disposeAndNull(Graphics? graphics)
        {
            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadImage(fileDialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveImage(fileDialog.FileName);
            }
        }

        //Brush lineBrush = new SolidBrush(Color.Green);
        //List<Rectangle> drawRectangles = new List<Rectangle>();



        private void pictureBoxOriginal_LoadCompleted(object sender, EventArgs e)
        {
            CreateOverlay();
        }

        private void deleteOverlayElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            symbols.Clear();
            UpdateOverlay();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void addNewSymbolToList(GraphicSymbol symbol)
        {
            if (symbol != null)
            {
                symbols.Add(symbol);
            }
        }

        public enum SymbolType
        {
            None,
            Rectangle,
            Circle,
            Text,
            Arrow,
            Line
        }
        private SymbolType newSymbolType = SymbolType.Rectangle;

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Rectangle;
            UpdateOverlay();
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            //newSymbol = new GsCircle(linePen, lineBrush, Color.Green, Color.Blue, symbols.Count * 4, symbols.Count * 4, 50, 50);
            newSymbolType = SymbolType.Circle;
            UpdateOverlay();
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Line;
            UpdateOverlay();
        }

        bool dragStarted = false;
        int dragStartX = 0;
        int dragStartY = 0;
        Rectangle dragRect = new Rectangle();

        private void pictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            dragStarted = true;
            dragStartX = e.X;
            dragStartY = e.Y;
        }

        private void pictureBoxOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            GraphicSymbol? symbol = GetSymbol(sender, e);
            if (symbol != null)
            {
                addNewSymbolToList(symbol);
            }
            UpdateOverlay();
            dragStarted = false;
        }

        private GraphicSymbol? GetSymbol(object sender, MouseEventArgs e)
        {
            int dragEndX = e.X;
            int dragEndY = e.Y;

            //Debug.WriteLine($"Drag positions: sX {dragStartX} sY {dragStartY} eX {dragEndX} eY {dragEndY}");

            if (dragStarted)
            {
                int dragLeft = Math.Min(dragStartX, dragEndX);
                int dragRight = Math.Max(dragStartX, dragEndX);
                int dragTop = Math.Min(dragStartY, dragEndY);
                int dragBottom = Math.Max(dragStartY, dragEndY);
                int dragWidth = dragRight - dragLeft;
                int dragHeight = dragBottom - dragTop;

                dragRect = new Rectangle(dragLeft, dragTop, dragWidth, dragHeight);

                //Debug.WriteLine("Drag rectangle: " + dragRect.ToString());

                switch (newSymbolType)
                {
                    case SymbolType.Rectangle:
                        return new GsRectangle(linePen, lineBrush, Color.Green, Color.Blue, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height);
                    case SymbolType.Circle:
                        return new GsCircle(linePen, lineBrush, Color.Green, Color.Blue, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height);
                    case SymbolType.Line:
                        return new GsLine(linePen, lineBrush, Color.Green, Color.Blue, dragStartX, dragStartY, dragEndX, dragEndY);
                    default:
                        return null;
                }

                //newSymbol = new GsRectangle(linePen, lineBrush, Color.Green, Color.Blue, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height);
                //addNewSymbolToList(newSymbol);
                //UpdateOverlay();
            }
            else
            {
                return null;
            }
        }

        private void pictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            //if (dragStarted)
            //{
            GraphicSymbol? symbol = GetSymbol(sender, e);
            if (symbol != null)
            {
                UpdateOverlay(symbol);
            }
            //}
        }

        private void pictureBoxOverlay_MouseLeave(object sender, EventArgs e)
        {
            dragStarted = false;
        }
    }

    public class GraphicSymbol
    {
        public Pen pen;
        public Brush brush;
        public Color foregroundColor;
        public Color backgroundColor;
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public GraphicSymbol(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2 = 0, int Y2 = 0)
        { 
            this.brush = brush;
            this.pen = pen;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.X1 = X1; 
            this.Y1 = Y1;
            this.X2 = X2;
            this.Y2 = Y2;
        }

        public GraphicSymbol(GraphicSymbol clonedSymbol)
        {
            this.brush = clonedSymbol.brush;
            this.pen = clonedSymbol.pen;
            this.foregroundColor = clonedSymbol.foregroundColor;
            this.backgroundColor = clonedSymbol.backgroundColor;
            this.X1 = clonedSymbol.X1; // coordinate 1
            this.Y1 = clonedSymbol.Y1; // coordinate 1
            this.X2 = clonedSymbol.X2; // width or coordinate 2
            this.Y2 = clonedSymbol.Y2; // height or coordinate 2
        }

        public virtual void DrawSymbol(Graphics graphic)
        {
        }
    }

    public class GsRectangle : GraphicSymbol
    {
        public GsRectangle(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X1, int Y1, int X2 = 0, int Y2 = 0) : base(pen, brush, foregroundColor, backgroundColor, X1, Y1, X2, Y2)
        {
        }

        public override void DrawSymbol(Graphics graphic)
        {
            graphic.DrawRectangle(pen, new Rectangle(X1, Y1, X2, Y2));
        }
    }

    public class GsCircle : GraphicSymbol
    {
        public GsCircle(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X, int Y, int Width = 0, int Height = 0) : base(pen, brush, foregroundColor, backgroundColor, X, Y, Width, Height)
        {
        }

        public override void DrawSymbol(Graphics graphic)
        {

            graphic.DrawEllipse(pen, new Rectangle(X1, Y1, X2, Y2));
        }
    }
    
    public class GsLine : GraphicSymbol
    {
        public GsLine(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X, int Y, int Width = 0, int Height = 0) : base(pen, brush, foregroundColor, backgroundColor, X, Y, Width, Height)
        {
        }

        public override void DrawSymbol(Graphics graphic)
        {

            graphic.DrawLine(pen, new Point(X1, Y1), new Point(X2, Y2));
        }
    }

    public class GsArrow : GraphicSymbol
    {
        public GsArrow(Pen pen, Brush brush, Color foregroundColor, Color backgroundColor, int X, int Y, int Width = 0, int Height = 0) : base(pen, brush, foregroundColor, backgroundColor, X, Y, Width, Height)
        {
        }

        public override void DrawSymbol(Graphics graphic)
        {
            //PointF mid = new PointF((X1 + X2) / 2, (Y1 + Y2) / 2);
            graphic.DrawLine(pen, new Point(X1, Y1), new Point(X2, Y2));

            //graphic.TranslateTransform(mid.X, mid.Y);
            //graphic.RotateTransform(45f);
            //graphic.DrawPath(pen, new GraphicsPath());
        }
    }
}
