using Godot;
using System;

public partial class PerkGen : GeneratorBase
{
	// Called when the node enters the scene tree for the first time.
	public Perks PerkValue;
	public override void _Ready()
	{
		CardAmount = Enum.GetValues<Perks>().Length - 1;

		OnReady();
	}
    public override void Initialize()
    {
        base.Initialize();
		PerkValue = (Perks)ValCard;
    }
}


public enum Perks
{
	BalanceCoin,
	ChaoticArtifact,
	DifferentlyOdd,
	DivideTalisman,
	EquallyEven,
	InateSum,
	MultiIchor
}