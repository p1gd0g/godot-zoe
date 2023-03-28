using Godot;
using System;

public class ColorRectBg : ColorRect
{

    [Export]
    public PackedScene zoeScene;
    [Export]
    NodePath _timerPath;


    Timer _timer;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = (Timer)GetNode(_timerPath);
        _timer.Connect("timeout", this, nameof(OnTimerTimeout));
        _timer.Start();
    }


    void OnTimerTimeout()
    {
        // instance zoe in random position of viewport
        var zoe = (Zoe)zoeScene.Instance();
        AddChild(zoe);
        zoe.Position = new Vector2(
            (float)GD.RandRange(0, GetViewportRect().Size.x), 0
        );
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }

    public override void _Input(InputEvent @event)
    {

        // if (@event is InputEventMouseButton button)
        // {
        //     if (button.IsPressed())
        //     {
        //         GD.Print("xxx Mouse Clicked");

        //         var zoe = (Zoe)zoeScene.Instance();
        //         AddChild(zoe);

        //         zoe.Position = button.Position;
        //     }
        // }


        // // Mouse in viewport coordinates.
        // if (@event is InputEventMouseButton eventMouseButton)
        //     GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
        // else if (@event is InputEventMouseMotion eventMouseMotion)
        //     GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

        // // Print the size of the viewport.
        // GD.Print("Viewport Resolution is: ", GetViewportRect().Size);

    }

}
