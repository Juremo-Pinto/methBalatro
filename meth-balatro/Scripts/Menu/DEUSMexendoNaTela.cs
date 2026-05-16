using Godot;
using System;

public partial class DEUSMexendoNaTela : AnimatedSprite2D
{
	private Vector2 OriginalPos;
	float shift_amount = 0.5f;
	float float_speed = 1.0f;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rng.Randomize();
		OriginalPos = GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float offsetX = (_rng.RandiRange(-1,1) * float_speed) * shift_amount;
		float offsetY = (_rng.RandiRange(-1,1) * float_speed) * shift_amount;
		Vector2 offset = new Vector2(offsetX, offsetY);

		GlobalPosition = GlobalPosition.Slerp(offset, 0.14f);
		GlobalPosition = GlobalPosition.Slerp(OriginalPos, 0.14f);
	}
}
