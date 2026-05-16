using Godot;
using System;

public partial class Spritescript : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	Vector2 mouse;
	Viewport thing;

	public override void _Ready()
	{
		thing = GetViewport();
		mouse = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mouse = thing.GetMousePosition();
		Position = Position.Slerp(mouse, .15f);
		GD.Print(mouse);
	}
}
