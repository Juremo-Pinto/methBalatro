using Godot;
using System;

public partial class ViewportSizeController : SubViewportContainer
{
	// Called when the node enters the scene tree for the first time.

	SubViewport viewport;

	public override void _Ready()
	{
		viewport = GetChild(0) as SubViewport;
		GetTree().Root.SizeChanged += OnWindowChange;
	}

	void OnWindowChange()
	{
		viewport.Size = GetTree().Root.Size;
	}
}
