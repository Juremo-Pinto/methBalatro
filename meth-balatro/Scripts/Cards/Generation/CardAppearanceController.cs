using Godot;
using System;

public partial class CardAppearanceController : Sprite2D
{
	[Export]
	public Texture2D[] textCards;


	public void Initialize()
	{
		var parentCard = GetParent<GeneratorBase>();

		Texture = textCards[parentCard.ValCard];
	}
}
