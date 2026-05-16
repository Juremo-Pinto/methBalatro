using Godot;
using System;

public partial class MathEventBus : Node
{
	public static MathEventBus Instance { get; private set; }

	[Signal]
	public delegate void MathErrorEventHandler();

	[Signal]
	public delegate void MathSuccessEventHandler(string result);
	public int numTentativas {get; private set;} = 1;
	public string[] numAlvo {get; private set;}
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	public override void _EnterTree()
	{
		if (Instance != null && Instance != this)
		{
			QueueFree();
			return;
		}

		Instance = this;
		gerarNumeroAlvo();
	}

	public override void _ExitTree()
	{
		if (Instance == this)
			Instance = null;
	}

	public void EmitError()
	{
		EmitSignal(SignalName.MathError);
	}

	public void EmitSuccess(string result)
	{
		EmitSignal(SignalName.MathSuccess, result);
		numTentativas++;
	}
	public void gerarNumeroAlvo()
	{
		_rng.Randomize();
		bool operadorGerado = false;
		numAlvo = new string[4];
		for(int bananas = 0; bananas < 4; bananas++)
		{
			if(bananas != 3 && !operadorGerado)
			{
				int opChance = _rng.RandiRange(1,100);
				if(opChance <= 25){
					int opGerado = _rng.RandiRange(0,1); // 0 = Virgula, 1 = Raiz
					if(opGerado == 0)
						numAlvo[bananas] = "√";
					else
						numAlvo[bananas] = ",";
					operadorGerado = true;
					continue;
				}
			}
			int numGerado = _rng.RandiRange(0,9);
			numAlvo[bananas] = $"{numGerado}";
		}
	}
}