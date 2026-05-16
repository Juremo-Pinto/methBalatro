using Godot;
using System;
using System.Linq;

public partial class detectingCardiesYippee : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var cards = GetOverlappingAreas();
		var sortedCards = cards.OrderBy(area => area.GlobalPosition.X).ToList();
		foreach(Node2D card in sortedCards)
		{
			var cardValue = card.GetNode<cardGenType>("Control");
			if (cardValue.varNum)
			{
				
			}else if (cardValue.varOp)
			{
				
			}
		}
	}
}
