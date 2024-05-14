using ScreenShotTool.Classes;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace ScreenShotTool.Forms;
[SupportedOSPlatform("windows")]

public partial class TagView : Form
{
    Tagging tagging;
    //BindingList<InfoTag> tags;
    BindingSource bindingSource;
    BindingList<InfoTag> bindingList;
    public TagView(Tagging parent)
    {
        InitializeComponent();
        this.tagging = parent;
        bindingList = new BindingList<InfoTag>(parent.CaptureTags);
        bindingSource = new BindingSource(bindingList, null);
        dataGridView1.DataSource = bindingList;
        if (dataGridView1.Columns.Count > 1)
        {
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 220;
        }
        dataGridView1.AutoGenerateColumns = false;
    }

    private void buttonAddTag_Click(object sender, EventArgs e)
    {
        tagging.CaptureTags.Add(new InfoTag(false, ""));
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        List<int> columnWidths = new List<int>();
        foreach (DataGridViewColumn column in dataGridView1.Columns)
        {
            columnWidths.Add(column.Width);
        }
        dataGridView1.DataSource = null;
        dataGridView1.DataSource = bindingList;
        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        {
            dataGridView1.Columns[i].Width = columnWidths[i];
        }
    }

    private void buttonSaveTags_Click(object sender, EventArgs e)
    {
        tagging.SaveTagData();
    }

    private void ButtonMoveUp_Click(object sender, EventArgs e)
    {
        if (tagging.CaptureTags.Count != dataGridView1.Rows.Count) return;
        if (dataGridView1.Rows.Count < 2) return;
        int currentIndex = dataGridView1.SelectedCells[0].RowIndex;
        if (currentIndex < 1) return;
        if (currentIndex >= tagging.CaptureTags.Count) return;
        MoveItemInList(tagging.CaptureTags, currentIndex, currentIndex - 1);
        RefreshGrid();
    }

    private void ButtonMoveDown_Click(object sender, EventArgs e)
    {
        if (tagging.CaptureTags.Count != dataGridView1.Rows.Count) return;
        if (dataGridView1.Rows.Count < 2) return;
        int currentIndex = dataGridView1.SelectedCells[0].RowIndex;
        if (currentIndex < 0) return;
        if (currentIndex >= tagging.CaptureTags.Count - 1) return;
        MoveItemInList(tagging.CaptureTags, currentIndex, currentIndex + 1);
        RefreshGrid();
    }

    private void MoveItemInList(List<InfoTag> list, int oldIndex, int newIndex)
    {
        InfoTag item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        if (tagging.CaptureTags.Count != dataGridView1.Rows.Count) return;
        if (dataGridView1.Rows.Count < 1) return;
        int currentIndex = dataGridView1.SelectedCells[0].RowIndex;
        if (currentIndex < 0) return;
        if (currentIndex >= tagging.CaptureTags.Count) return;
        tagging.CaptureTags.RemoveAt(currentIndex);
        RefreshGrid();
    }

    private void buttonOnTop_Click(object sender, EventArgs e)
    {
        TopMost = !TopMost;
    }
}
