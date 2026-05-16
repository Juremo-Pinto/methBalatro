using Godot;
using System;

public partial class menuScreenControls : Button
{
	[Export]
	public bool isCloseButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}

	private void OnButtonPressed()
	{
		Control father = GetParent<Control>();
		if(!isCloseButton)
			father.QueueFree();
		else
			GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
