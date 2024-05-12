using ScreenShotTool.Classes;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace ScreenShotTool.Forms;
[SupportedOSPlatform("windows")]

public partial class TagView : Form
{
    MainForm mainForm;
    //BindingList<InfoTag> tags;
    BindingSource bindingSource;
    BindingList<InfoTag> bindingList;
    public TagView(MainForm parent)
    {
        InitializeComponent();
        mainForm = parent;
        bindingList = new BindingList<InfoTag>(parent.CaptureTags);
        bindingSource = new BindingSource(bindingList, null);
        dataGridView1.DataSource = bindingList;
        if (dataGridView1.Columns.Count > 1)
        {
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 220;
        }
    }

    private void buttonAddTag_Click(object sender, EventArgs e)
    {
        mainForm.CaptureTags.Add(new InfoTag(false, ""));
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
        mainForm.SaveTagData();
    }
}
