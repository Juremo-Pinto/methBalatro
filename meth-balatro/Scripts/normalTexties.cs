using Godot;
using System;

public partial class normalTexties : Sprite2D
{
	[Export]
	public Texture2D[] valNumerosText;
	[Export]
	public bool placarPontos;
	[Export]
	public bool placarAtual;
	[Export]
	public bool placarTentativas;
	[Export]
	public int intPos; // 0, 1 ou 2
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MathEventBus.Instance.MathSuccess += OnMathSuccess;
		MathEventBus.Instance.MathError += OnMathFail;

		if(placarTentativas)
		Texture = valNumerosText[0];
	}

	private void OnMathSuccess(string resultado)
	{
		if(placarTentativas)
		Texture = valNumerosText[MathEventBus.Instance.numTentativas];
	}

	private void OnMathFail()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
