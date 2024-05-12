namespace ScreenShotTool.Classes;

public class InfoTag
{
    public InfoTag(bool enabled, string name, string description = "")//, string image = "")
    {
        Enabled = enabled;
        Name = name;
        Description = description;
        //Image = image;
    }

    public bool Enabled { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    //public string Image { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
