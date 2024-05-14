using ScreenShotTool.Forms;
using ScreenShotTool.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool.Classes;

[SupportedOSPlatform("windows")]

public class Tagging
{
    MainForm mainForm;
    TagView? tagView;
    public List<InfoTag> CaptureTags = [];

    public Tagging(MainForm parent)
    {
        this.mainForm = parent;
    }



    public void LoadTagData()
    {
        CaptureTags.Clear();
        string tagDataFolder = Environment.ExpandEnvironmentVariables(Settings.Default.TagDataFolder);
        if (Directory.Exists(tagDataFolder) == false)
        {
            try
            {
                Debug.WriteLine($"Couldn't create find Tag Data Folder, creating the folder: {tagDataFolder}");
                Directory.CreateDirectory(tagDataFolder);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Couldn't create Tag Data Folder {tagDataFolder}\n: {e.Message}");
            }
        }
        // Folder should exist now
        if (Directory.Exists(tagDataFolder))
        {
            string tagFile = Path.Join(tagDataFolder, "tags.txt");
            if (File.Exists(tagFile) == false)
            {
                Debug.WriteLine($"Can't find tag file: {tagFile}");
            }
            else
            {
                string[] tagLines = File.ReadAllLines(tagFile);
                foreach (string line in tagLines)
                {
                    if (line.Length < 3)
                    {
                        Debug.WriteLine($"Invalid data on line, too short, aborting");
                        break;
                    }
                    else
                    {
                        bool enabled = false;
                        string[] sections = line.Split(";");
                        if (sections.Length > 2)
                        {
                            if (sections[0] == "True")
                                enabled = true;
                            InfoTag tag = new InfoTag(enabled, sections[1], sections[2]);
                            CaptureTags.Add(tag);
                            Debug.WriteLine($"Added tag: {enabled}, {sections[1]} ({sections[2]}");
                        }
                        else
                        {
                            Debug.WriteLine("Too few sections on line");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.WriteLine("Couldn't load tag data folder");
        }
        if (CaptureTags.Count == 0)
        {
            CaptureTags.Add(new InfoTag(false, ""));
        }
    }

    public void SaveTagData()
    {
        string tagDataFolder = Environment.ExpandEnvironmentVariables(Settings.Default.TagDataFolder);
        if (Directory.Exists(tagDataFolder) == false)
        {
            try
            {
                Debug.WriteLine($"Couldn't create find Tag Data Folder, creating the folder: {tagDataFolder}");
                Directory.CreateDirectory(tagDataFolder);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Couldn't create Tag Data Folder {tagDataFolder}\n: {e.Message}");
            }
        }
        // Folder should exist now
        if (Directory.Exists(tagDataFolder))
        {
            string tagFile = Path.Join(tagDataFolder, "tags.txt");
            StringBuilder sb = new StringBuilder();
            foreach (InfoTag tag in CaptureTags)
            {
                sb.Append(tag.Enabled);
                sb.Append($";{tag.Name}");
                sb.AppendLine($";{tag.Description}");
            }
            try
            {
                Debug.WriteLine($"Saving to tag file: {tagFile}");
                File.WriteAllText(tagFile, sb.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error saving to tag file {tagFile}:\n{e.Message}");
            }
        }
    }

    public InfoTag? GetTag(int index)
    {
        if (index < CaptureTags.Count)
        {
            return CaptureTags[index];
        }
        else
        {
            return null;
        }
    }

    public string GetTagsText()
    {
        StringBuilder sb = new StringBuilder();
        foreach (InfoTag tag in CaptureTags)
        {
            if (tag.Enabled)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(tag.Name);
            }
        }
        return sb.ToString();
    }

    public void ShowTagView()
    {
        if (tagView == null || tagView.IsDisposed)
        {
            tagView = new TagView(this);
        }

        tagView.Show();
        // bring it to front
        if (tagView.TopMost == false)
        {
            tagView.TopMost = true;
            tagView.TopMost = false;
        }
        else
        {
            tagView.TopMost = false;
            tagView.TopMost = true;
        }
    }
}
