using Godot;
using System;

public partial class Drag : Area2D
{
	private Vector2 OriginalPos;
	private Vector2 TargetPos;
	private bool Hold;


	private Sprite2D sprite;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("../Sprite");
		OriginalPos = sprite.Position;
		TargetPos = OriginalPos;

		InputEvent += OnMouseInput;
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
			GlobalPosition = GetViewport().GetMousePosition();
		}

		sprite.GlobalPosition = sprite.GlobalPosition.Slerp(GlobalPosition, .14f);
	}
}
