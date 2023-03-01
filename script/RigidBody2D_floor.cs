using Godot;
using System;

public class RigidBody2D_floor : RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.TweenRight();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//
	//  }


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
