using Godot;
using System;
using System.Reflection;

public partial class Drag : Area2D
{
	private Vector2 OriginalPos;
	private Vector2 TargetPos;
	private Vector2 CurrentPos;
	private bool Hold;


	private Sprite2D sprite;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OriginalPos = sprite.Position;
		CurrentPos = sprite.Position;
		TargetPos = OriginalPos;
		InputEvent += OnMouseInput;

		sprite = GetParent<Sprite2D>();
		GD.Print(sprite);
	}

	private void OnMouseInput(Node viewport, InputEvent @event, long shapeIdx)
	{
		if(@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left)
		{
			Hold = mouseButton.Pressed;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Hold)
		{
			CurrentPos = GetViewport().GetMousePosition();
		}

		sprite.Position = sprite.Position.Slerp(CurrentPos, .14f);
	}
}
