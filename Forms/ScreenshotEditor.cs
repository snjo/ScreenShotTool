using ScreenShotTool.Properties;
using System.Diagnostics;
using System.Drawing.Text;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace ScreenShotTool.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class ScreenshotEditor : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #region Constructor ---------------------------------------------------------------------------------
        Image? originalImage;
        Image? overlayImage;
        Graphics? overlayGraphics;
        int arrowWeight = 5;
        int lineWeight = 2;
        int frameRate = Settings.Default.MaxFramerate;
        public static int maxFontSize = 200;
        public static int minimumFontSize = 5;
        public static int startingFontSize = 10;
        List<GraphicSymbol> symbols = new();


        private void SetupEditor()
        {
            pictureBoxOverlay.Parent = pictureBoxOriginal;
            fillFontFamilyBox();
            numericPropertiesFontSize.Maximum = maxFontSize;
            numericPropertiesFontSize.Minimum = minimumFontSize;
            numericPropertiesFontSize.Value = startingFontSize;
            panelSymbolGeneral.Enabled = false;
            panelSymbolShape.Visible = false;
            panelSymbolText.Visible = false;
        }

        public ScreenshotEditor()
        {
            InitializeComponent();
            SetupEditor();

            CreateNewImage(640, 480, Color.White);
        }

        public ScreenshotEditor(string file)
        {
            InitializeComponent();
            SetupEditor();

            LoadImageFromFile(file);
        }

        public ScreenshotEditor(bool fromClipboard)
        {
            InitializeComponent();
            SetupEditor();

            if (fromClipboard)
            {
                LoadImageFromClipboard();
            }
        }

        public ScreenshotEditor(Image loadImage)
        {
            InitializeComponent();
            SetupEditor();
            pictureBoxOverlay.Parent = pictureBoxOriginal;
            LoadImageFromImage(loadImage);
            SetForegroundWindow(this.Handle);
        }

        #endregion

        #region Load and Save -------------------------------------------------------------------------------

        private void SetOriginalImage()
        {
            // used at the end of each Load/Create image
            pictureBoxOriginal.Image = originalImage;
            DisposeAndNull(overlayGraphics);
            DisposeAndNull(overlayImage);
            DeleteAllSymbols();
        }

        private void CreateNewImage(int Width, int Height, Color color)
        {
            DisposeAndNull(originalImage);
            originalImage = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(originalImage);
            g.FillRectangle(new SolidBrush(color), 0, 0, Width, Height);
            SetOriginalImage();
            DeleteAllSymbols();
        }

        private void LoadImageFromClipboard()
        {
            try
            {
                originalImage = Clipboard.GetImage();
            }
            catch
            {
                Debug.WriteLine("Could not load from clipboard");
                return;
            }
            DisposeAndNull(pictureBoxOriginal.Image);
            SetOriginalImage();
        }

        private void LoadImageFromImage(Image image)
        {
            DisposeAndNull(originalImage);
            originalImage = image;
            SetOriginalImage();
        }

        public void LoadImageFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                Debug.WriteLine("Loading file: " + filename);

                try
                {
                    // Using and closing filestream, so the file isn't reserved by the process

                    Image? tempImage = null;
                    using (FileStream stream = new FileStream(filename, FileMode.Open))
                    {
                        tempImage = Image.FromStream(stream);
                    }
                    DisposeAndNull(originalImage);
                    originalImage = tempImage;
                }
                catch
                {
                    Debug.WriteLine("Could not load file");

                }
                DisposeAndNull(pictureBoxOriginal.Image);
                SetOriginalImage();
            }
        }

        public void SaveImage(string filename)
        {
            //originalImage.
            if (originalImage != null)
            {
                Graphics saveGraphic = Graphics.FromImage(originalImage);
                saveGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                saveGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;
                
                DrawElements(saveGraphic);
                originalImage.Save(filename);
                saveGraphic.Dispose();
            }
        }

        private void PictureBoxOriginal_LoadCompleted(object sender, EventArgs e)
        {
            CreateOverlay();
        }

        #endregion

        #region Create and Update overlay -------------------------------------------------------------------
        private void CreateOverlay()
        {
            if (originalImage != null)
            {
                Debug.WriteLine("Creating overlay");
                overlayImage?.Dispose();
                overlayGraphics?.Dispose();
                overlayImage = new Bitmap(originalImage.Width, originalImage.Height);
                overlayGraphics = Graphics.FromImage(overlayImage);


                //overlayGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                //overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; // fixes ugly aliasing on text
                overlayGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                overlayGraphics.TextRenderingHint = TextRenderingHint.AntiAlias; // fixes ugly aliasing on text

                pictureBoxOverlay.Image = overlayImage;
                pictureBoxOverlay.Width = pictureBoxOriginal.Width;
                pictureBoxOverlay.Height = pictureBoxOriginal.Height;
            }
            else
            {
                //Debug.WriteLine("Create overlay failed, original image is null");
            }
        }

        DateTime LastFrame = DateTime.Now;

        private bool UpdateOverlay(GraphicSymbol? temporarySymbol = null, bool forceUpdate = true)
        {
            float MilliSecondsPerFrame = (1f / frameRate) * 1000;
            TimeSpan ts = DateTime.Now - LastFrame;
            if (ts.Milliseconds < MilliSecondsPerFrame && forceUpdate == false)
            {
                return false;
            }

            if (overlayImage == null || overlayGraphics == null)
            {
                CreateOverlay();
            }
            if (overlayImage != null && overlayGraphics != null)
            {
                pictureBoxOverlay.Image.Dispose();
                pictureBoxOverlay.Image = DrawOverlay(temporarySymbol); ;
            }

            LastFrame = DateTime.Now;
            return true;
        }

        private Image DrawOverlay(GraphicSymbol? temporarySymbol = null)
        {
            Image img = new Bitmap(this.Width, this.Height);
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
            temporarySymbol?.DrawSymbol(graphic);
        }

        private static void DisposeAndNull(Image? image)
        {
            if (image != null)
            {
                image.Dispose();
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                image = null;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }
        }

        private static void DisposeAndNull(Graphics? graphics)
        {
            if (graphics != null)
            {
                graphics.Dispose();
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                graphics = null;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            }
        }
        #endregion

        #region Menu items ----------------------------------------------------------------------------------

        private void ItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadImageFromFile(fileDialog.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveImage(fileDialog.FileName);
            }
        }

        private void DeleteOverlayElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteAllSymbols();
        }

        private void DeleteAllSymbols()
        {
            listViewSymbols.Items.Clear();
            foreach (GraphicSymbol symbol in symbols)
            {
                ClearPropertyPanelValues();
                symbol.Dispose();
            }
            symbols.Clear();
            UpdateOverlay();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void ItemLoadFromClipboard_Click(object sender, EventArgs e)
        {
            LoadImageFromClipboard();
        }

        private void PasteIntoThisImage_Click(object sender, EventArgs e)
        {
            PasteIntoImage();
        }

        private void ItemPasteScaled_Click(object sender, EventArgs e)
        {
            PasteIntoImageScaled();
        }

        #endregion

        #region Symbols -------------------------------------------------------------------------------------

        public enum SymbolType
        {
            None,
            MoveSymbol,
            Rectangle,
            Circle,
            Text,
            Arrow,
            Line,
            Image,
            ImageScaled
        }


        private SymbolType newSymbolType = SymbolType.Rectangle;

        private void AddNewSymbolToList(GraphicSymbol symbol)
        {
            if (symbol != null)
            {
                symbols.Add(symbol);
                ListViewItem newItem = listViewSymbols.Items.Add(symbol.Name);
                newItem.Text = symbol.Name;
                newItem.Tag = symbol;
                listViewSymbols.Update();
                if (listViewSymbols.Items.Count > 0)
                {
                    listViewSymbols.Items[listViewSymbols.Items.Count - 1].Focused = true;
                    listViewSymbols.Items[listViewSymbols.Items.Count - 1].Selected = true;
                    listViewSymbols.Items[listViewSymbols.Items.Count - 1].EnsureVisible();
                    listViewSymbols.Select();
                }
            }
        }

        private GraphicSymbol? GetSymbol(object sender, MouseEventArgs e)
        {
            Point dragEnd = new Point(e.X, e.Y);
            int lineWeight = (int)numericNewLineWeight.Value;
            int lineAlpha = (int)numericNewLineAlpha.Value;
            int fillAlpha = (int)numericNewFillAlpha.Value;

            if (dragStarted)
            {
                int dragLeft = Math.Min(dragStart.X, dragEnd.X);
                int dragRight = Math.Max(dragStart.X, dragEnd.X);
                int dragTop = Math.Min(dragStart.Y, dragEnd.Y);
                int dragBottom = Math.Max(dragStart.Y, dragEnd.Y);
                int dragWidth = dragRight - dragLeft;
                int dragHeight = dragBottom - dragTop;
                Point size = new Point(dragWidth, dragHeight);

                dragRect = new Rectangle(dragLeft, dragTop, dragWidth, dragHeight);

                Point upperLeft = new Point(dragLeft, dragTop);
                Point bottomRight = new Point(dragRight, dragBottom);

                Color lineColor = buttonNewColorLine.BackColor;
                Color fillColor = buttonNewColorFill.BackColor;

                /* switch (newSymbolType)
                {
                    case SymbolType.Rectangle:
                        return new GsRectangle(lineColor, fillColor, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight, lineAlpha, fillAlpha);
                    case SymbolType.Circle:
                        return new GsCircle(lineColor, fillColor, dragRect.X, dragRect.Y, dragRect.Width, dragRect.Height, lineWeight, lineAlpha, fillAlpha);
                    case SymbolType.Line:
                        return new GsLine(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight, lineAlpha);
                    case SymbolType.Arrow:
                        return new GsArrow(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY, lineWeight, lineAlpha);
                    case SymbolType.Image:
                        return new GsImage(lineColor, fillColor, dragStartX, dragStartY, dragEndX, dragEndY);
                    case SymbolType.ImageScaled:
                        return new GsImageScaled(lineColor, fillColor, dragLeft, dragTop, dragWidth, dragHeight);
                    default:
                        return null;
                }
                */

                return newSymbolType switch
                {
                    SymbolType.Rectangle => new GsRectangle(lineColor, fillColor, upperLeft, size, lineWeight, lineAlpha, fillAlpha),
                    SymbolType.Circle => new GsCircle(lineColor, fillColor, upperLeft, size, lineWeight, lineAlpha, fillAlpha),
                    SymbolType.Line => new GsLine(lineColor, fillColor, dragStart, dragEnd, lineWeight, lineAlpha),
                    SymbolType.Arrow => new GsArrow(lineColor, fillColor, dragStart, dragEnd, lineWeight, lineAlpha),
                    SymbolType.Image => new GsImage(lineColor, fillColor, dragEnd, new Point(1, 1)),
                    SymbolType.ImageScaled => new GsImageScaled(lineColor, fillColor, upperLeft, size),
                    SymbolType.Text => new GsText(lineColor, fillColor, dragStart, size, lineWeight, lineAlpha),
                    _ => null,
                };
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Symbol toolbar buttons ----------------------------------------------------------------------

        private void ButtonRectangle_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Rectangle;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonCircle_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Circle;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonLine_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Line;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }

        private void ButtonArrow_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Arrow;
            numericNewLineWeight.Value = arrowWeight;
            UpdateOverlay();
        }


        private void ButtonNewText_Click(object sender, EventArgs e)
        {
            newSymbolType = SymbolType.Text;
            numericNewLineWeight.Value = lineWeight;
            UpdateOverlay();
        }


        #endregion

        #region Mouse input ---------------------------------------------------------------------------------

        bool dragStarted = false;
        Point dragStart = new Point(0, 0);
        //int dragStartX = 0;
        //int dragStartY = 0;
        Rectangle dragRect = new();

        private void PictureBoxOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            dragStarted = true;
            dragStart = new Point(e.X, e.Y);
            if (overlayGraphics == null)
            {
                CreateOverlay();
            }
        }

        private void PictureBoxOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            GraphicSymbol? symbol = GetSymbol(sender, e);
            if (symbol != null)
            {
                if (symbol.ValidSymbol)
                {
                    AddNewSymbolToList(symbol);
                }
            }
            UpdateOverlay();
            UpdatePropertiesPanel();
            dragStarted = false;
            //if (newSymbolType == SymbolType.Image || newSymbolType == SymbolType.ImageScaled)
            //{
            //    // don't repeatedly add pasted images
            //    newSymbolType = SymbolType.None;
            //}
            if (newSymbolType != SymbolType.MoveSymbol)
            {
                newSymbolType = SymbolType.None;
            }
        }

        int oldMouseX = 0;
        int oldMouseY = 0;

        enum MoveType
        {
            None,
            Start,
            End,
        }
        MoveType moveType = MoveType.None;

        private void PictureBoxOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;
            GraphicSymbol? tempSymbol = GetSymbol(sender, e);
            if (tempSymbol != null)
            {
                UpdateOverlay(tempSymbol, false);
                tempSymbol.Dispose();
            }
            else if (newSymbolType == SymbolType.MoveSymbol)
            {
                int mouseDeltaX = Cursor.Position.X - oldMouseX;
                int mouseDeltaY = Cursor.Position.Y - oldMouseY;
                if (listViewSymbols.SelectedItems.Count > 0)
                {
                    ListViewItem item = listViewSymbols.SelectedItems[0];
                    //Debug.WriteLine($"Mouse delta {mouseDeltaX} {mouseDeltaY}");
                    if (dragStarted)
                    {
                        GraphicSymbol symbol = (GraphicSymbol)item.Tag;


                        if (moveType == MoveType.None)
                        {
                            Vector2 cursorPosInternal = new Vector2(e.X, e.Y);
                            float distanceToStart = Vector2.Distance(cursorPosInternal, symbol.StartPointV2);
                            float distanceToEnd = Vector2.Distance(cursorPosInternal, symbol.EndPointV2);
                            if (distanceToStart < distanceToEnd || symbol.ScalingAllowed == false)
                            {
                                moveType = MoveType.Start;
                            }
                            else
                            {
                                moveType = MoveType.End;
                            }
                        }
                        if (moveType == MoveType.Start)
                        {
                            int newX = symbol.StartPoint.X + mouseDeltaX;
                            int newY = symbol.StartPoint.Y + mouseDeltaY;
                            newX = Math.Max(-100, newX);
                            newY = Math.Max(-100, newY);
                            newX = Math.Min(newX, originalImage.Width);
                            newY = Math.Min(newY, originalImage.Height);

                            symbol.StartPoint = new Point(newX, newY);
                        }
                        else if (moveType == MoveType.End)
                        {
                            int newX = symbol.EndPoint.X + mouseDeltaX;
                            int newY = Math.Max(0, symbol.EndPoint.Y + mouseDeltaY);
                            newX = Math.Max(0, newX);
                            newY = Math.Max(0, newY);
                            newX = Math.Min(newX, originalImage.Width);
                            newY = Math.Min(newY, originalImage.Height);

                            symbol.EndPoint = new Point(newX, newY);
                        }
                        UpdateOverlay(null, false);
                        oldMouseX = Cursor.Position.X;
                        oldMouseY = Cursor.Position.Y;
                    }
                    else
                    {
                        moveType = MoveType.None;
                        oldMouseX = Cursor.Position.X;
                        oldMouseY = Cursor.Position.Y;
                    }
                }
            }
        }

        private void PictureBoxOverlay_MouseLeave(object sender, EventArgs e)
        {
            //dragStarted = false;
        }


        #endregion

        #region Key input -----------------------------------------------------------------------------------
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
            if ((e.KeyCode == Keys.C && e.Modifiers == Keys.Control))
            {
                CopyToClipboard();
            }
        }
        #endregion

        #region Copy and Paste ------------------------------------------------------------------------------

        public void CopyToClipboard()
        {
            if (originalImage != null)
            {
                Bitmap outImage = new Bitmap(originalImage);
                Graphics saveGraphic = Graphics.FromImage(outImage);
                saveGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                saveGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;
                DrawElements(saveGraphic);
                Clipboard.SetImage(outImage);
                saveGraphic.Dispose();
            }
        }

        private void PasteIntoImage()
        {
            newSymbolType = SymbolType.Image;
            dragStarted = true;
            dragStart = new Point(0, 0);
            UpdateOverlay();
        }

        private void PasteIntoImageScaled()
        {
            newSymbolType = SymbolType.ImageScaled;
            UpdateOverlay();
        }

        #endregion

        #region Selected symbol Properties panel ------------------------------------------------------------

        private void ListViewSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                newSymbolType = SymbolType.MoveSymbol;

            }
            else
            {
                if (newSymbolType == SymbolType.MoveSymbol)
                {
                    newSymbolType = SymbolType.None;
                }
            }
            UpdatePropertiesPanel();
        }

        private void UpdatePropertiesPanel()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                //newSymbolType = SymbolType.MoveSymbol;
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GraphicSymbol graphicSymbol)
                {
                    panelSymbolGeneral.Enabled = true;
                    panelSymbolShape.Visible = true;
                    panelSymbolText.Visible = false;

                    numericWidth.Enabled = true;
                    numericHeight.Enabled = true;
                    buttonPropertiesColorLine.Enabled = true;
                    numericPropertiesLineAlpha.Enabled = true;

                    textBoxSymbolText.Text = "";

                    labelSymbolType.Text = "Symbol: " + graphicSymbol.Name;
                    numericX.Value = graphicSymbol.Left;
                    numericY.Value = graphicSymbol.Top;
                    numericWidth.Value = Math.Clamp(graphicSymbol.Width, numericWidth.Minimum, numericWidth.Maximum);
                    numericHeight.Value = Math.Clamp(graphicSymbol.Height, numericHeight.Minimum, numericHeight.Maximum);
                    buttonPropertiesColorLine.BackColor = graphicSymbol.foregroundColor;
                    buttonPropertiesColorFill.BackColor = graphicSymbol.backgroundColor;
                    numericPropertiesLineWeight.Value = graphicSymbol.lineWeight;
                    buttonDeleteSymbol.Tag = graphicSymbol;

                    if (graphicSymbol is GsImage)
                    {
                        numericWidth.Enabled = false;
                        numericHeight.Enabled = false;

                        buttonPropertiesColorLine.Enabled = false;
                        numericPropertiesLineAlpha.Enabled = false;

                        panelSymbolShape.Visible = false;
                    }

                    if (graphicSymbol is GsImageScaled)
                    {
                        buttonPropertiesColorLine.Enabled = false;
                        numericPropertiesLineAlpha.Enabled = false;

                        panelSymbolShape.Visible = false;
                    }


                    if (graphicSymbol is GsText)
                    {
                        GsText gsText = (GsText)graphicSymbol;

                        panelSymbolText.Visible = true;
                        panelSymbolText.Location = new Point(panelSymbolGeneral.Location.X, panelSymbolGeneral.Bottom + 5);

                        panelSymbolShape.Visible = false;

                        numericWidth.Enabled = false;
                        numericHeight.Enabled = false;

                        textBoxSymbolText.Text = gsText.text;
                        numericPropertiesFontSize.Value = (int)Math.Clamp(gsText.fontEmSize, minimumFontSize, maxFontSize);
                        checkBoxFontBold.Checked = (gsText.fontStyle & FontStyle.Bold) != 0;
                        checkBoxFontItalic.Checked = (gsText.fontStyle & FontStyle.Italic) != 0;
                        checkBoxStrikeout.Checked = (gsText.fontStyle & FontStyle.Strikeout) != 0;
                        checkBoxUnderline.Checked = (gsText.fontStyle & FontStyle.Underline) != 0;
                    }

                    numericPropertiesLineAlpha.Value = graphicSymbol.lineAlpha;
                    numericPropertiesFillAlpha.Value = graphicSymbol.fillAlpha;
                }

            }
            else
            {
                panelSymbolGeneral.Enabled = false;
                panelSymbolShape.Visible = false;
                panelSymbolText.Visible = false;
                ClearPropertyPanelValues();
            }
        }

        private void ButtonDeleteSymbol_Click(object sender, EventArgs e)
        {
            DeleteSelectedSymbol();
        }

        private void DeleteSelectedSymbol()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (buttonDeleteSymbol.Tag is GraphicSymbol gs)
                {
                    listViewSymbols.Items.Remove(item);
                    gs.Dispose();
                    symbols.Remove(gs);
                }
                listViewSymbols.Update();
                ClearPropertyPanelValues();
                UpdateOverlay();
            }
        }

        private void ClearPropertyPanelValues()
        {
            labelSymbolType.Text = "Symbol: ";
            numericX.Value = 0;
            numericY.Value = 0;
            numericWidth.Value = 1;
            numericHeight.Value = 1;
            buttonPropertiesColorLine.BackColor = Color.Gray;
            buttonPropertiesColorFill.BackColor = Color.Gray;
            numericPropertiesLineAlpha.Value = 255;
            numericPropertiesFillAlpha.Value = 255;
            numericPropertiesLineWeight.Value = 1;
            numericPropertiesFontSize.Value = 10;
            buttonDeleteSymbol.Tag = null;
        }

        private void Numeric_ValueChanged(object sender, EventArgs e)
        {
            //if (dragStarted) return;

            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is not GraphicSymbol gs) return;

                if (sender == numericX)
                {
                    gs.Left = (int)numericX.Value;
                }
                if (sender == numericY)
                {
                    gs.Top = (int)numericY.Value;
                }
                if (sender == numericWidth)
                {
                    gs.Width = (int)numericWidth.Value;
                }
                if (sender == numericHeight)
                {
                    gs.Height = (int)numericHeight.Value;
                }
                if (sender == numericPropertiesLineWeight)
                {
                    gs.lineWeight = (int)numericPropertiesLineWeight.Value;
                }
                if (sender == numericPropertiesLineAlpha)
                {
                    gs.lineAlpha = (int)numericPropertiesLineAlpha.Value;
                    gs.UpdateColors();
                    buttonPropertiesColorLine.BackColor = gs.foregroundColor;
                }
                if (sender == numericPropertiesFillAlpha)
                {
                    gs.fillAlpha = (int)numericPropertiesFillAlpha.Value;
                    gs.UpdateColors();
                    buttonPropertiesColorFill.BackColor = gs.backgroundColor;
                }
            }
            UpdateOverlay();
        }

        private void ColorChangeClick(object sender, EventArgs e)
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is not GraphicSymbol gs) return;

                DialogResult result = colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (sender == buttonPropertiesColorLine)
                    {
                        buttonPropertiesColorLine.BackColor = colorDialog1.Color;
                        gs.foregroundColor = colorDialog1.Color;

                    }
                    if (sender == buttonPropertiesColorFill)
                    {
                        buttonPropertiesColorFill.BackColor = colorDialog1.Color;
                        gs.backgroundColor = colorDialog1.Color;
                    }
                }
            }
            UpdateOverlay();
        }

        private void textBoxSymbolText_TextChanged(object sender, EventArgs e)
        {
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                textSymbol.text = textBoxSymbolText.Text;
            }
            UpdateOverlay();
        }

        private void numericPropertiesFontSize_ValueChanged(object sender, EventArgs e)
        {
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                textSymbol.fontEmSize = (float)numericPropertiesFontSize.Value;
                textSymbol.UpdateFont();
            }
            UpdateOverlay();
        }

        private void comboBoxFontFamily_ValueMemberChanged(object sender, EventArgs e)
        {
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                string selectedFont = comboBoxFontFamily.Text;
                if (fontDictionary.ContainsKey(selectedFont))
                {
                    textSymbol.fontFamily = fontDictionary[selectedFont];
                    textSymbol.UpdateFont();
                }
            }
            UpdateOverlay();
        }

        Dictionary<string, FontFamily> fontDictionary = new();
        private void fillFontFamilyBox()
        {
            List<FontFamily> fontList = System.Drawing.FontFamily.Families.ToList();
            List<string> fontNames = new List<string>();

            foreach (FontFamily font in fontList)
            {
                fontNames.Add(font.Name);
                fontDictionary.Add(font.Name, font);
            }
            comboBoxFontFamily.DataSource = fontNames;
        }

        private void fontStyle_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFontStyle();
        }

        private void UpdateFontStyle()
        {
            bool bold = checkBoxFontBold.Checked;
            bool italic = checkBoxFontItalic.Checked;
            bool strikeout = checkBoxStrikeout.Checked;
            bool underline = checkBoxUnderline.Checked;
            GsText? textSymbol = GetSelectedTextSymbol();
            if (textSymbol != null)
            {
                FontStyle fontStyle = FontStyle.Regular;
                if (bold)
                {
                    fontStyle |= FontStyle.Bold;
                }
                if (italic)
                {
                    fontStyle |= FontStyle.Italic;
                }
                if (strikeout)
                {
                    fontStyle |= FontStyle.Strikeout;
                }
                if (underline)
                {
                    fontStyle |= FontStyle.Underline;
                }
                textSymbol.fontStyle = fontStyle;
            }
            UpdateOverlay();
        }

        private GraphicSymbol? GetSelectedSymbol()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GraphicSymbol gs) return gs;
            }
            return null;
        }

        private GsText? GetSelectedTextSymbol()
        {
            if (listViewSymbols.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSymbols.SelectedItems[0];
                if (item.Tag is GsText gs) return gs;
            }
            return null;
        }

        #endregion

        #region Top toolbar, new Symbol settings ------------------------------------------------------------

        private void NumericNewLineWeight_ValueChanged(object sender, EventArgs e)
        {
            if (newSymbolType == SymbolType.Arrow)
            {
                arrowWeight = (int)numericNewLineWeight.Value;
            }
            else
            {
                lineWeight = (int)numericNewLineWeight.Value;
            }
        }

        private void NewColorLine_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonNewColorLine.BackColor = colorDialog1.Color;
            }
        }

        private void NewColorFill_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonNewColorFill.BackColor = colorDialog1.Color;
            }
        }

        private void ItemNewImage_Click(object sender, EventArgs e)
        {
            NewImagePrompt imagePrompt = new();
            DialogResult result = imagePrompt.ShowDialog();
            if (result == DialogResult.OK)
            {
                CreateNewImage(imagePrompt.imageWidth, imagePrompt.imageHeight, imagePrompt.color);
            }
            imagePrompt.Dispose();
        }

        #endregion



        private void listViewSymbols_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedSymbol();
            }
        }

    }
}
