using Godot;
using System;

public partial class NumberGen : GeneratorBase
{
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		CardAmount = 9;

		OnReady();
	}
}
