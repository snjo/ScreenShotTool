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

    public ColorDialogAlpha()
    {
        InitializeComponent();
        colors = GetAllColors(false);
        SwatchSize.Width = (panelSwatches.Width / SwatchesHorizontal);
        SwatchSize.Height = (panelSwatches.Height / SwatchesVertical);
        CreateColorSwatches();
        Debug.WriteLine($"Swatch size: {SwatchSize}, in panelSwatches size: {panelSwatches.Size}");
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
                panelSwatches.Controls.Add(swatchButton);
                Debug.WriteLine($"Added color swatch {swatchCount}: {newColor.Name}");
                swatchCount++;
            }
        }
        Debug.WriteLine($"Added {swatchCount} swatches");
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
        MoveToList(colorList, rearrangedColors, Color.Pink);
        MoveToList(colorList, rearrangedColors, Color.Purple);
        MoveColorByName(colorList, rearrangedColors, "Red");
        MoveColorByName(colorList, rearrangedColors, "Green");
        MoveColorByName(colorList, rearrangedColors, "Lime");
        MoveColorByName(colorList, rearrangedColors, "Blue");
        MoveColorByName(colorList, rearrangedColors, "Yellow");
        MoveColorByName(colorList, rearrangedColors, "Orange");
        MoveColorByName(colorList, rearrangedColors, "Pink");
        MoveColorByName(colorList, rearrangedColors, "Red");
        MoveColorByName(colorList, rearrangedColors, "Brown");
        MoveColorByName(colorList, rearrangedColors, "Purple");
        MoveColorByName(colorList, rearrangedColors, "Gray");
        rearrangedColors.AddRange(colorList);
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
        }
        
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
