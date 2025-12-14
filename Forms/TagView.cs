using PdfSharp.Pdf.Filters;
using ScreenShotTool.Classes;
using ScreenShotTool.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
        bindingList = parent.FilteredTags(textBoxFilter.Text);//new BindingList<InfoTag>(parent.CaptureTags);
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
            dataGridView1.Columns[3].Width = 150;
        }
        dataGridView1.AutoGenerateColumns = false;
        checkBoxMultiSelect.Checked = Settings.Default.TagMultiSelect;
        dataGridView1.AllowCheckboxMultiSelect = Settings.Default.TagMultiSelect;
    }

    private void buttonAddTag_Click(object sender, EventArgs e)
    {
        int selectedTagIndex = 0;
        if (dataGridView1.SelectedCells.Count > 0)
        {
            InfoTag selectedItem = (InfoTag)dataGridView1.SelectedCells[0].OwningRow.DataBoundItem;
            selectedTagIndex = tagging.CaptureTags.IndexOf(selectedItem);
        }
        InfoTag newTag = new InfoTag(false, "", "", textBoxFilter.Text);
        tagging.CaptureTags.Insert(selectedTagIndex, newTag); // adding filter text to avoid indexing error when new tag is hidden

        RefreshGrid();
        SelectCellContainingInfoTag(newTag);
    }

    private void SelectCellContainingInfoTag(InfoTag tag)
    {
        if (dataGridView1.Rows.Count < 1) return;
        int selectIndex = 0;
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            if (row.DataBoundItem == tag)
            {
                selectIndex = row.Index;
                break;
            }
        }
        dataGridView1.CurrentCell = dataGridView1.Rows[selectIndex].Cells[1];
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
        bindingList = tagging.FilteredTags(textBoxFilter.Text);
        dataGridView1.DataSource = bindingList;
        for (int i = 0; i < dataGridView1.Columns.Count; i++)
        {
            dataGridView1.Columns[i].Width = columnWidths[i];
        }
    }

    private void UpdateCategories()
    {
        foreach (InfoTag tag in bindingList)
        {
            if (CategoryExists(tag.Category) == false)
            {
                categoryList.Add(new InfoTagCategory(tag.Enabled, tag.Category, tag.Description));
            }
        }
    }

    private bool CategoryExists(string name)
    {
        foreach (InfoTagCategory cat in categoryList)
        {
            if (cat.Name == name)
            {
                return true;
            }
        }
        return false;
    }

    private void buttonSaveTags_Click(object sender, EventArgs e)
    {
        tagging.SaveTagData();
    }

    private void ButtonMoveUp_Click(object sender, EventArgs e)
    {
        //if (tagging.CaptureTags.Count != dataGridView1.Rows.Count) return;
        if (dataGridView1.Rows.Count < 2) return;

        InfoTag selectedItem = (InfoTag)dataGridView1.SelectedCells[0].OwningRow.DataBoundItem;
        int selectedTagIndex = tagging.CaptureTags.IndexOf(selectedItem);

        //int currentIndex = dataGridView1.SelectedCells[0].RowIndex;
        if (selectedTagIndex < 1) return;
        if (selectedTagIndex >= tagging.CaptureTags.Count) return;
        MoveItemInList(tagging.CaptureTags, selectedTagIndex, selectedTagIndex - 1);
        RefreshGrid();
        SelectCellContainingInfoTag(selectedItem);
    }

    private void ButtonMoveDown_Click(object sender, EventArgs e)
    {
        //if (tagging.CaptureTags.Count != dataGridView1.Rows.Count) return;
        if (dataGridView1.Rows.Count < 2) return;

        InfoTag selectedItem = (InfoTag)dataGridView1.SelectedCells[0].OwningRow.DataBoundItem;
        int selectedTagIndex = tagging.CaptureTags.IndexOf(selectedItem);

        //int currentIndex = dataGridView1.SelectedCells[0].RowIndex;
        if (selectedTagIndex < 0) return;
        if (selectedTagIndex >= tagging.CaptureTags.Count - 1) return;
        MoveItemInList(tagging.CaptureTags, selectedTagIndex, selectedTagIndex + 1);
        RefreshGrid();
        SelectCellContainingInfoTag(selectedItem);
    }

    private void MoveItemInList(List<InfoTag> list, int oldIndex, int newIndex)
    {
        InfoTag item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedCells.Count < 1) return;
        InfoTag item = (InfoTag)dataGridView1.SelectedCells[0].OwningRow.DataBoundItem;
        Debug.WriteLine($"Deleting tag: {item.Name} / {item.Description} / {item.Category}");
        tagging.CaptureTags.Remove(item);
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

    private void TextBoxFilter_TextChanged(object sender, EventArgs e)
    {
        RefreshGrid();
    }
}
