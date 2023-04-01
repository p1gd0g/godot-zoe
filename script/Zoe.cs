using Godot;
using System;

public class Zoe : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    [Export]
    public NodePath _timerPath;


    public Timer _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = GetNode<Timer>(_timerPath);
        _timer.Connect("timeout", this, "OnTimerTimeout");


    }

    void OnTimerTimeout()
    {
        QueueFree();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
