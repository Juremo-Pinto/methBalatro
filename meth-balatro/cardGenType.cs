using Godot;
using System;

public partial class cardGenType : Control
{
	[Export]
	public bool varNonOp;
	public int valCardNum {get; private set;}
	public Operators valCardOp {get; private set;}
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rng.Randomize();

		if (varNonOp)
			valCardNum = _rng.RandiRange(0,9);
		else
			valCardOp = (Operators)_rng.RandiRange(0,5);
			
		var child = GetNode<RandomCardGen>("Sprite");
		child.Initialize();
	}
}


public enum Operators
{
	Division,
	Power,
	Subtract,
	Multiply,
	Add,
	SquareRoot
}