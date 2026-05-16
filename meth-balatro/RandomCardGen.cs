using Godot;
using System;

public partial class RandomCardGen : Sprite2D
{
	[Export]
	public Texture2D[] textCardsOp;
	[Export]
	public Texture2D[] textCardsNum;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
	}

	public void Initialize()
	{
		var parentCard = GetParent<cardGenType>();

		if (parentCard.varNonOp)
			Texture = textCardsNum[parentCard.valCardNum];
		else
			Texture = textCardsOp[parentCard.valCardOp];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
