using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotTool.Forms;
#pragma warning disable CA1416 // Validate platform compatibility
public partial class ColorDialogAlpha : Form
{
    int SwatchesHorizontal = 15;
    int SwatchesVertical = 10;
    Size SwatchSize = new(20, 20);
    int swatchPadding = 3;
    public Color Color = Color.White;
    List<Color> colors;

    public ColorDialogAlpha(Color startColor)
    {
        
        InitializeComponent();
        this.Color = startColor;
        colors = GetAllColors(false);
        SwatchSize.Width = (panelSwatches.Width / SwatchesHorizontal);
        SwatchSize.Height = (panelSwatches.Height / SwatchesVertical);
        UpateColor(this.Color);
        CreateColorSwatches();
        //Debug.WriteLine($"Swatch size: {SwatchSize}, in panelSwatches size: {panelSwatches.Size}");
    }

    private void CreateColorSwatches()
    {
        int swatchCount = 0;
        for (int sX = 0; sX < SwatchesHorizontal; sX++)
        {
            for (int sY = 0; sY < SwatchesVertical; sY++)
            {
                Button swatchButton = new()
                {
                    Parent = panelSwatches,
                    Location = new Point((sX * SwatchSize.Width) + swatchPadding, (sY * (SwatchSize.Height) + swatchPadding)),
                    Size = SwatchSize,
                    FlatStyle = FlatStyle.Flat,
                };
                swatchButton.Click += ClickSwatch;
                Color newColor = Color.Black;
                if (swatchCount < colors.Count)
                {
                    newColor = colors[swatchCount];
                }
                swatchButton.BackColor = newColor;
                if (newColor == Color.Transparent) swatchButton.Text = "T";
                panelSwatches.Controls.Add(swatchButton);
                //Debug.WriteLine($"Added color swatch {swatchCount}: {newColor.Name}");
                swatchCount++;
            }
        }
        //Debug.WriteLine($"Added {swatchCount} swatches");
    }

    private static List<Color> GetAllColors(bool includeUIColors)
    {
        List<Color> colorList = new List<Color>();
        ColorConverter converter = new ColorConverter();
        int colorCount = 0;
        foreach (Color color in converter.GetStandardValues())
        {
            if (includeUIColors == false)
            {
                if (color.Name.Contains("Active")) continue;
                if (color.Name.Contains("Inactive")) continue;
                if (color.Name.Contains("Control")) continue;
                if (color.Name.Contains("Button")) continue;
                if (color.Name.Contains("Gradient")) continue;
                if (color.Name.Contains("Text")) continue;
                if (color.Name.Contains("Menu")) continue;
                if (color.Name.Contains("Desktop")) continue;
                if (color.Name.Contains("Window")) continue;
            }
            colorList.Add(color);
            colorCount++;
        }
        List<Color> rearrangedColors = new List<Color>();
        MoveToList(colorList, rearrangedColors, Color.Transparent);
        MoveToList(colorList, rearrangedColors, Color.Black);
        MoveToList(colorList, rearrangedColors, Color.White);
        MoveToList(colorList, rearrangedColors, Color.LightGray);
        MoveToList(colorList, rearrangedColors, Color.Gray);
        MoveToList(colorList, rearrangedColors, Color.DarkGray);
        MoveToList(colorList, rearrangedColors, Color.Red);
        MoveToList(colorList, rearrangedColors, Color.Green);
        MoveToList(colorList, rearrangedColors, Color.Blue);
        MoveToList(colorList, rearrangedColors, Color.Yellow);
        MoveToList(colorList, rearrangedColors, Color.Orange);
        rearrangedColors.AddRange(OrderColorByHue(colorList));
        return rearrangedColors;
    }

    private static void MoveColorByName(List<Color> oldList, List<Color> newList, string name)
    {
        for (int i = oldList.Count-1; i > -1; i--)
        {
            Color color = oldList[i];
            if (color.Name.Contains(name))
            {
                MoveToList(oldList, newList, color);
            }
        }
    }

    private static List<Color> OrderColorByHue(List<Color> colors)
    {
        // https://stackoverflow.com/questions/62203098/c-sharp-how-do-i-order-a-list-of-colors-in-the-order-of-a-rainbow
        var orderedColorList = colors
            .OrderBy(color => color.GetHue())
            .ThenBy(o => o.R * 3 + o.G * 2 + o.B * 1);
        return orderedColorList.ToList();
    }

    private static void MoveToList(List<Color> oldList, List<Color> newList, Color moveColor)
    {
        oldList.Remove(moveColor);
        newList.Add(moveColor);
    }

    private void ClickSwatch(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            Debug.WriteLine(button.BackColor);
            UpateColor(button.BackColor);
        }
    }

    private void TrackbarColorChanged(object sender, EventArgs e)
    {
        if (sender is TrackBar trackBar)
        {
            if (trackBar == trackBarAlpha)
            {
                numericAlpha.Value = trackBarAlpha.Value;
            }
            if (trackBar == trackBarRed)
            {
                numericRed.Value = trackBarRed.Value;
            }
            if (trackBar == trackBarBlue)
            {
                numericBlue.Value = trackBarBlue.Value;
            }
            if (trackBar == trackBarGreen)
            {
                numericGreen.Value = trackBarGreen.Value;
            }
        }
    }

    private void NumericColorChanged(object sender, EventArgs e)
    {
        UpateColor(Color.FromArgb((int)numericAlpha.Value, (int)numericRed.Value, (int)numericGreen.Value, (int)numericBlue.Value));
    }

    private void UpateColor(Color newColor)
    {
        //Debug.WriteLine($"Setting color: {this.Color}");
        panelColorSampleSolid.BackColor = newColor;
        if (newColor == Color.Transparent)
        {
            panelColorSampleAlpha.BackColor = Color.Transparent;
        }
        else
        {
            panelColorSampleAlpha.BackColor = Color.FromArgb(trackBarAlpha.Value, newColor);
        }
        trackBarAlpha.Value = newColor.A;
        trackBarRed.Value = newColor.R;
        trackBarGreen.Value = newColor.G;
        trackBarBlue.Value = newColor.B;
        numericAlpha.Value = newColor.A;
        numericRed.Value = newColor.R;
        numericGreen.Value = newColor.G;
        numericBlue.Value = newColor.B;
        Color = newColor;
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        DialogResult= DialogResult.Cancel;
    }
}
