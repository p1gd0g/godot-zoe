using Godot;
using System;

public class LabelTimer : Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    DateTime _dateTime = new DateTime();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        _dateTime = _dateTime.AddSeconds(1638207420).ToLocalTime();
    }


    public override void _Process(float delta)
    {
        var ts = DateTime.Now.Subtract(_dateTime);
        this.Text = string.Format("{0:%d} d, {0:%h} h, {0:%m} m, {0:%s} s", ts);

    }

}
