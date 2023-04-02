using Godot;
using System;

public class ColorRectBg : ColorRect
{

    [Export]
    public PackedScene zoeScene;
    [Export]
    NodePath _timerPath;

    [Export]
    NodePath _counterPath;

    public Label _counter;


    Timer _timer;

    int counter = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = (Timer)GetNode(_timerPath);
        _counter = (Label)GetNode(_counterPath);

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


    public void doCount()
    {
        counter++;
        _counter.Text = string.Format("你接住了 {0} 个曾晴！", counter);
    }


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
