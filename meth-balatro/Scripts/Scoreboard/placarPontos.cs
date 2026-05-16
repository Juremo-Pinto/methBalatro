using Godot;
using System;

public partial class placarPontos : Sprite2D
{
	[Export]
	public bool isCurrent;

	[Export]
	public int intPos;

	[Export]
	public Texture2D[] valNumerosText;

	[Export]
	public Texture2D[] valOpText;

	public double numAtualSalvo = 0;

	public override void _Ready()
	{
		MathEventBus.Instance.MathSuccess += OnMathSuccess;

		if (!isCurrent)
		{
			string valor = MathEventBus.Instance.numAlvo[intPos];

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

	private void OnMathSuccess(string resultado)
	{
		numAtualSalvo += Convert.ToDouble(resultado);

		if (numAtualSalvo > 9999)
			numAtualSalvo = 9999;

		string texto = ((int)numAtualSalvo).ToString("0000");
		char c = texto[intPos];

		if (isCurrent)
		{
			Texture = valNumerosText[c - '0'];
		}
	}
}