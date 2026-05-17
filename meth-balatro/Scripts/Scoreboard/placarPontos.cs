using Godot;
using System;

public partial class placarPontos : Sprite2D
{
	[Export] public bool isCurrent;
	[Export] public int intPos;
	[Export] public Texture2D[] valNumerosText;
	[Export] public Texture2D[] valOpText;

	public override void _Ready()
	{
		MathEventBus.Instance.MathSuccess += OnUpdate;
		MathEventBus.Instance.LostGame += OnReset;
		MathEventBus.Instance.WonGame += OnReset;

		UpdateVisual();
	}

	private void OnUpdate(string _)
	{
		UpdateVisual();
	}

	private void OnReset()
	{
		UpdateVisual();
	}

	private void UpdateVisual()
	{
		string valor;

		if (isCurrent)
			valor = MathEventBus.Instance.numAtual[intPos];
		else
			valor = MathEventBus.Instance.numAlvo[intPos];

		if (valor == "√")
		{
			Texture = valOpText[0];
		}
		else if (valor == ",")
		{
			Texture = valOpText[1];
		}
		else
		{
			Texture = valNumerosText[int.Parse(valor)];
		}
	}
}