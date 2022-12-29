using Godot;
using System;

public class ColorRectBg : ColorRect
{

    [Export]
    public PackedScene zoeScene;


    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }

    public override void _Input(InputEvent @event)
    {

        if (@event is InputEventMouseButton button)
        {
            if (button.IsPressed())
            {
                GD.Print("xxx Mouse Clicked");

                var zoe = (Zoe)zoeScene.Instance();
                AddChild(zoe);

                zoe.Position = button.Position;
            }
        }


        // // Mouse in viewport coordinates.
        // if (@event is InputEventMouseButton eventMouseButton)
        //     GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
        // else if (@event is InputEventMouseMotion eventMouseMotion)
        //     GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

        // // Print the size of the viewport.
        // GD.Print("Viewport Resolution is: ", GetViewportRect().Size);
    }

}
