using Godot;
using System;

public class RigidBody2D_floor : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    NodePath joystickLeftPath;
    [Export]
    float speed = 100;

    VirtualJoystick joystickLeft;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        joystickLeft = (VirtualJoystick)GetNode(joystickLeftPath);


        // this.TweenRight();
    }

    public override void _Process(float delta)
    {
        // 	var move := Vector2.ZERO
        Vector2 move = new Vector2(0, 0);
        // move.x = Input.get_axis("ui_left", "ui_right")
        move.x = Input.GetAxis("ui_left", "ui_right");
        // move.y = Input.get_axis("ui_up", "ui_down")
        move.y = Input.GetAxis("ui_up", "ui_down");
        // position += move * speed * delta
        Position += move * speed * delta;

    }


    // tween self to right in 1s
    public void TweenRight()
    {

        var v = this.Position;

        Tween tween = new Tween();
        AddChild(tween);
        tween.InterpolateProperty(this, "position", v, new Vector2(100, v.y), 1, Tween.TransitionType.Linear, Tween.EaseType.InOut);

        tween.Repeat = true;
        tween.Start();
    }



}
