using Godot;
using System;

public class ColorRectBg : ColorRect
{

    [Export]
    public PackedScene zoeScene;
    [Export]
    NodePath _timerPath, _counterPath;

    Node _counter;
    HTTPRequest _http;

    Timer _timer;

    int counter = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = (Timer)GetNode(_timerPath);
        _counter = GetNode(_counterPath);
    }

    void OnTimerTimeout()
    {
        // instance zoe in random position of viewport
        var zoe = (Zoe)zoeScene.Instance();
        AddChild(zoe);
        zoe.Position = new Vector2(
            (float)GD.RandRange(0, GetViewportRect().Size.x), 0
        );

        zoe.Connect("DoCount", _counter, "on_do_count");
    }

}
