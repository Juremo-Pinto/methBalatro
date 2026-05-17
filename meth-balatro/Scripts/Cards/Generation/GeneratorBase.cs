using Godot;
using System;

public partial class GeneratorBase : Node
{

	public int ValCard {get; private set;}
	
	protected int CardAmount;

	private RandomNumberGenerator _rng = new();

	protected void OnReady()
	{
		MathEventBus.Instance.MathSuccess += OnMathSuccess;
		MathEventBus.Instance.LostGame += onGameLost;
		MathEventBus.Instance.WonGame += onGameWon;
		Initialize();
	}
	private void OnMathSuccess(string a)
	{
		Initialize();
	}
	private void onGameLost()
	{
		Initialize();
	}
	private void onGameWon()
	{
		Initialize();
	}
	
	public virtual void Initialize()
	{
		_rng.Randomize();

		int num = _rng.RandiRange(0,CardAmount);

		ValCard = num;

		var child = GetNode<CardAppearanceController>("Sprite");
		child.Initialize();
	}
}
