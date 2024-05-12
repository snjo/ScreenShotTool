using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace ScreenShotTool.Classes;
[SupportedOSPlatform("windows")]

public partial class DataGridSpecial : DataGridView
{
    public DataGridSpecial()
    {
        InitializeComponent();
        base.CellMouseUp += CheckboxEndEdit;
    }

    private void CheckboxEndEdit(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex < 0 || e.ColumnIndex >= Columns.Count) return;
        if (Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
        {
            Debug.WriteLine($"Ending edit {sender} {e.ColumnIndex}");
            EndEdit();
        }
        else
        {
            Debug.WriteLine($"Dont' end edit {sender} {e.ColumnIndex}");
        }
    }

    public DataGridSpecial(IContainer container)
    {
        container.Add(this);

        InitializeComponent();
    }
}
