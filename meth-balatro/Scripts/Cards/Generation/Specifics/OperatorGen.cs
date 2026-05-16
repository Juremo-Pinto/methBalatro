using Godot;
using System;

public partial class OperatorGen : GeneratorBase
{
	// Called when the node enters the scene tree for the first time.
	public Operators OperatorValue;
	public override void _Ready()
	{
		CardAmount = Enum.GetValues<Operators>().Length - 1;

		OnReady();
	}

    public override void Initialize()
    {
        base.Initialize();
		OperatorValue = (Operators)ValCard;
    }
}

public enum Operators
{
	Division,
	Power,
	Subtraction,
	Multiplication,
	Addition,
	SquareRoot
}