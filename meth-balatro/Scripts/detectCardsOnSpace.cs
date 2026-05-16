using Godot;
using System;

public partial class detectCardsOnSpace : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InputEvent += OnInputEvent;
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                GD.Print($"Clicked on shape index: {shapeIdx}");
            }
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
