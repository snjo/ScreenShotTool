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
        dataGridView1.DataSource = null;
        dataGridView1.DataSource = bindingList;
    }

    private void buttonSaveTags_Click(object sender, EventArgs e)
    {
        mainForm.SaveTagData();
    }
}
