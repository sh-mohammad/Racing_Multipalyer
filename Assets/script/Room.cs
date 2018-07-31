
using UnityEngine;

public class Room
{

    public Sprite Icon;
    public string Name, Description;

    public Room(Sprite icon, string name, string description)
    {
        Icon = icon;
        Name = name;
        Description = description;
    }
}
