using ScreenShotTool.Classes;
using ScreenShotTool.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ScreenShotTool.Forms;
[SupportedOSPlatform("windows")]

public partial class TagView : Form
{
    Tagging tagging;
    //BindingList<InfoTag> tags;
    BindingSource bindingSource;
    BindingList<InfoTag> bindingList;
    List<InfoTagCategory> categoryList = new();
    
    public TagView(Tagging parent)
    {
        InitializeComponent();
        this.tagging = parent;
        bindingList = new BindingList<InfoTag>(parent.CaptureTags);
        bindingSource = new BindingSource(bindingList, null);
        dataGridView1.DataSource = bindingList;

        UpdateCategories();
        //CheckListCategory.DataSource = categoryList;
        //CheckListCategory.DisplayMember = "DisplayName";
        //CheckListCategory.ValueMember = "Enabled";

        if (dataGridView1.Columns.Count > 1)
        {
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 220;
        }
        dataGridView1.AutoGenerateColumns = false;
        Debug.WriteLine($"Multiselect setting: {Settings.Default.TagMultiSelect}");
        checkBoxMultiSelect.Checked = Settings.Default.TagMultiSelect;
        dataGridView1.AllowCheckboxMultiSelect = Settings.Default.TagMultiSelect;
        Debug.WriteLine($"Grid multiselect: {dataGridView1.AllowCheckboxMultiSelect}");


    }

    private void buttonAddTag_Click(object sender, EventArgs e)
    {
        //tagging.CaptureTags.Add(new InfoTag(false, ""));
        int selectedTag = dataGridView1.SelectedCells[0].RowIndex + 1;
        if (selectedTag >= 0 && selectedTag < dataGridView1.Rows.Count)
        {
            tagging.CaptureTags.Insert(selectedTag, new InfoTag(false, ""));
        }
        else
        {
            tagging.CaptureTags.Add(new InfoTag(false, ""));
        }
        RefreshGrid();
        dataGridView1.CurrentCell = dataGridView1.Rows[selectedTag].Cells[1];
        dataGridView1.Select();
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
        //CheckListCategory.DisplayMember = "Name";
        //CheckListCategory.ValueMember = "IsChecked";
        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        {
            dataGridView1.Columns[i].Width = columnWidths[i];
        }
    }

    private void UpdateCategories()
    {
        Debug.WriteLine($"Updating categories");
        foreach (InfoTag tag in bindingList)
        {
            Debug.WriteLine($"   Checking tag {tag.Category}");
            if (CategoryExists(tag.Category))
            {
                Debug.WriteLine($"      Found duplicate category {tag.Category}");
            }
            else
            {
                Debug.WriteLine($"      Found new category {tag.Category}");
                categoryList.Add(new InfoTagCategory(tag.Enabled, tag.Category, tag.Description));
            }
        }
        //CheckListCategory.DataSource = null;
        //CheckListCategory.DataSource = categoryList;
        ////CheckListCategory.Update();
        //CheckListCategory.Refresh();
    }

    private bool CategoryExists(string name)
    {
        foreach (InfoTagCategory cat in categoryList)
        {
            Debug.WriteLine($"Check category {cat.Name} against {name}");
            if (cat.Name == name)
            {
                Debug.WriteLine("   HIT on category search");
                return true;
            }
        }
        Debug.WriteLine("   MISS on category search");
        return false;
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
        dataGridView1.CurrentCell = dataGridView1.Rows[newIndex].Cells[0];
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

    private void AllowMultiSelect_Click(object sender, EventArgs e)
    {
        Settings.Default.TagMultiSelect = checkBoxMultiSelect.Checked;
        Settings.Default.Save();
        dataGridView1.AllowCheckboxMultiSelect = checkBoxMultiSelect.Checked;
    }

    private void GridHotkeyCheck(object sender, KeyEventArgs e)
    {
        if (e.Modifiers == Keys.Control)
        { 
            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                Debug.WriteLine("Add tag hotkey");
                buttonAddTag_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                buttonDelete_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Oemcomma)
            {
                ButtonMoveUp_Click(sender, e);
            }
            else if (e.KeyCode == Keys.OemPeriod)
            {
                ButtonMoveDown_Click(sender, e);
            }
            else if (e.KeyCode == Keys.S)
            {
                buttonSaveTags_Click(sender, e);
            }
            else if (e.KeyCode == Keys.D)
            {
                DeselectTags();
            }
            else if (e.KeyCode == Keys.Q)
            {
                Close();
            }
        }
    }

    private void DeselectTags()
    {
        foreach (InfoTag tag in tagging.CaptureTags)
        {
            tag.Enabled = false;
            RefreshGrid();
        }
    }
}
