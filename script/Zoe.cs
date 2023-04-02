using Godot;
using System;

public class Zoe : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    [Export]
    public NodePath _timerPath;
    [Export]
    public NodePath _rigidPath;


    public Timer _timer;
    public RigidBody2D _rigid;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = GetNode<Timer>(_timerPath);
        _rigid = GetNode<RigidBody2D>(_rigidPath);
        // _timer.Connect("timeout", this, "OnTimerTimeout");
    }

    void OnTimerTimeout()
    {
        QueueFree();
    }


    void OnCollision(Node body)
    {
        var parent = GetParent<ColorRectBg>();
        parent.doCount();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
