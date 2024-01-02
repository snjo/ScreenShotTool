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

        public ScreenshotEditor(string? file = null, bool fromClipboard = false)
        {
            InitializeComponent();
            pictureBoxOverlay.Parent = pictureBoxOriginal;
            if (file != null)
            {
                LoadImageFromFile(file);
            }
            else if (fromClipboard)
            {
                LoadImageFromClipboard();
            }
        }

        private void LoadImageFromClipboard()
        {
            try
            {
                originalImage = Clipboard.GetImage();
                pictureBoxOriginal.Image = originalImage;
            }
            catch
            {
                Debug.WriteLine("Could not load from clipboard");
                return;
            }
            DisposeAndNull(overlayGraphics);
            DisposeAndNull(overlayImage);
            symbols.Clear();
        }

        public void LoadImageFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                Debug.WriteLine("Loading file: " + filename);

                //CreateOverlay();
                try
                {
                    originalImage = Image.FromFile(filename);
                }
                catch
                {
                    Debug.WriteLine("Could not load file");

                }
                pictureBoxOriginal.Image = originalImage;
                DisposeAndNull(overlayGraphics);
                DisposeAndNull(overlayImage);
                symbols.Clear();
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
                Image? image = DrawOverlay(pictureBoxOverlay.Image, temporarySymbol);
                pictureBoxOverlay.Image.Dispose();
                pictureBoxOverlay.Image = image;
                //DisposeAndNull(image);
            }
            else
            {
                Debug.WriteLine($"Error, overlay exists: image {overlayImage != null} / graphics {overlayGraphics != null}");
            }
        }

        private Image DrawOverlay(Image img, GraphicSymbol? temporarySymbol = null)
        {
            img = new Bitmap(this.Width, this.Height);
            DisposeAndNull(overlayGraphics);
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

        private void DisposeAndNull(Image? image)
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }

        private void DisposeAndNull(Graphics? graphics)
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
                LoadImageFromFile(fileDialog.FileName);
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
            foreach (GraphicSymbol symbol in symbols)
            {
                symbol.Dispose();
            }
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
                ListViewItem newItem = listViewSymbols.Items.Add(symbol.Name);
                newItem.Text = symbol.Name;
                newItem.Tag = symbol;
                listViewSymbols.Update();
            }
        }

        public enum SymbolType
        {
            None,
            Rectangle,
            Circle,
            Text,
            Arrow,
            Line,
            Image,
            ImageScaled
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
            if (overlayGraphics == null)
            {
                CreateOverlay();
            }
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
            if (newSymbolType == SymbolType.Image || newSymbolType == SymbolType.ImageScaled)
            {
                // don't repeatedly add pasted images
                newSymbolType = SymbolType.None;
            }
        }

        private GraphicSymbol? GetSymbol(object sender, MouseEventArgs e)
        {
            int dragEndX = e.X;
            int dragEndY = e.Y;
            int lineWeight = (int)numericNewLineWeight.Value; // TODO

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
                        return new GsRectangle(linePen, lineBrush, Color.Green, Color.Blue, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight);
                    case SymbolType.Circle:
                        return new GsCircle(linePen, lineBrush, Color.Green, Color.Blue, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight);
                    case SymbolType.Line:
                        return new GsLine(linePen, lineBrush, Color.Green, Color.Blue, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight);
                    case SymbolType.Image:
                        return new GsImage(linePen, lineBrush, Color.Green, Color.Blue, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight);
                    case SymbolType.ImageScaled:
                        return new GsImageScaled(linePen, lineBrush, Color.Green, Color.Blue, dragLeft, dragTop, dragWidth, dragHeight, lineWeight);
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
            GraphicSymbol? symbol = GetSymbol(sender, e);
            if (symbol != null)
            {
                UpdateOverlay(symbol);
                symbol.Dispose();
            }
        }

        private void pictureBoxOverlay_MouseLeave(object sender, EventArgs e)
        {
            dragStarted = false;
        }

        private void itemLoadFromClipboard_Click(object sender, EventArgs e)
        {
            LoadImageFromClipboard();
        }

        private void itemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pasteIntoThisImage_Click(object sender, EventArgs e)
        {
            PasteIntoImage();
        }

        private void PasteIntoImage()
        {
            newSymbolType = SymbolType.Image;
            dragStarted = true;
            dragStartX = 0;
            dragStartY = 0;
            UpdateOverlay();
        }

        private void PasteIntoImageScaled()
        {
            newSymbolType = SymbolType.ImageScaled;
            UpdateOverlay();
        }

        private void ScreenshotEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                PasteIntoImage();
            }
            if (e.KeyCode == Keys.V && e.Modifiers == (Keys.Control | Keys.Shift))
            {
                PasteIntoImageScaled();
            }
        }

        private void itemPasteScaled_Click(object sender, EventArgs e)
        {
            PasteIntoImageScaled();
        }

        private void listViewSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                GraphicSymbol? graphicSymbol = item.Tag as GraphicSymbol;
                if (graphicSymbol != null)
                {
                    labelSymbolType.Text = "Symbol: " + graphicSymbol.Name;
                    numericX.Value = graphicSymbol.X1;
                    numericY.Value = graphicSymbol.Y1;
                    numericWidth.Value = graphicSymbol.X2;
                    numericHeight.Value = graphicSymbol.Y2;
                    buttonColorLine.BackColor = graphicSymbol.foregroundColor;
                    buttonColorFill.BackColor = graphicSymbol.backgroundColor;
                    numericPropertiesLineWeight.Value = graphicSymbol.lineWeight;
                    buttonDeleteSymbol.Tag = graphicSymbol;
                }

            }
            else
            {
                //ClearProperties();
            }
        }

        private void buttonDeleteSymbol_Click(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                GraphicSymbol? gs = buttonDeleteSymbol.Tag as GraphicSymbol;
                if (gs != null)
                {
                    listViewSymbols.Items.Remove(item);
                    gs.Dispose();
                    symbols.Remove(gs);
                }
                listViewSymbols.Update();
                ClearProperties();
                UpdateOverlay();
            }
        }

        private void ClearProperties()
        {
            labelSymbolType.Text = "Symbol: ";
            numericX.Value = 0;
            numericY.Value = 0;
            numericWidth.Value = 1;
            numericHeight.Value = 1;
            buttonColorLine.BackColor = Color.Gray;
            buttonColorFill.BackColor = Color.Gray;
            numericPropertiesLineWeight.Value = 0;
            buttonDeleteSymbol.Tag = null;
        }

        private void numeric_ValueChanged(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                GraphicSymbol? gs = item.Tag as GraphicSymbol;
                if (gs == null) return;

                if (sender == numericX)
                {
                    gs.X1 = (int)numericX.Value;
                    Debug.WriteLine("Updating X1");
                }
                if (sender == numericY)
                {
                    gs.Y1 = (int)numericY.Value;
                    Debug.WriteLine("Updating Y1");
                }
                if (sender == numericWidth)
                {
                    gs.X2 = (int)numericWidth.Value;
                    Debug.WriteLine("Updating Width");
                }
                if (sender == numericHeight)
                {
                    gs.Y2 = (int)numericHeight.Value;
                    Debug.WriteLine("Updating Height");
                }
                if (sender == numericPropertiesLineWeight)
                {
                    gs.lineWeight = (int)numericPropertiesLineWeight.Value;
                    Debug.WriteLine("Updating LineWeight");
                }
            }
            UpdateOverlay();
        }

        private void colorChangeClick(object sender, EventArgs e)
        {
            // TODO
        }
    }
}
