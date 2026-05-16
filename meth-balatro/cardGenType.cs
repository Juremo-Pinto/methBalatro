using Godot;
using System;

public partial class cardGenType : Control
{
	[Export]
	public bool varNum;
	[Export]
	public bool varOp;
	public int valCardNum {get; private set;}
	public int valCardOp {get; private set;}
	public int valCardPrk {get; private set;}
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rng.Randomize();

		if (varNum)
			valCardNum = _rng.RandiRange(0,9);
		else if (varOp)
			valCardOp = _rng.RandiRange(0,5);
		else
			valCardPrk = _rng.RandiRange(0,6);
			
		var child = GetNode<RandomCardGen>("Sprite");
		child.Initialize();
	}
}
