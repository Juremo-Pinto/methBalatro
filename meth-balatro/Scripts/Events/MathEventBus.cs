using Godot;
using System;

public partial class MathEventBus : Node
{
	public static MathEventBus Instance { get; private set; }

	[Signal] public delegate void MathErrorEventHandler();
	[Signal] public delegate void LostGameEventHandler();
	[Signal] public delegate void WonGameEventHandler();
	[Signal] public delegate void MathSuccessEventHandler(string result);

	public int numTentativas { get; private set; } = 0;
	public int valRodada { get; private set; } = 1;

	public string[] numAlvo { get; private set; }
	public string[] numAtual { get; private set; }

	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	public override void _EnterTree()
	{
		if (Instance != null && Instance != this)
		{
			QueueFree();
			return;
		}

		Instance = this;

		ResetAtual();
		gerarNumeroAlvo();
	}

	public override void _ExitTree()
	{
		if (Instance == this)
			Instance = null;
	}

	public void ResetAtual()
	{
		numAtual = new string[4];
		for (int i = 0; i < 4; i++)
			numAtual[i] = "0";
	}

	public void EmitError()
	{
		EmitSignal(SignalName.MathError);
	}

	public void GameLost()
	{
		numTentativas = 0;
		valRodada = 1;
		ResetAtual();
		gerarNumeroAlvo();
		EmitSignal(SignalName.LostGame);
	}

	public void GameWon()
	{
		numTentativas = 0;
		valRodada++;
		ResetAtual();
		gerarNumeroAlvo();
		EmitSignal(SignalName.WonGame);
	}

	public void EmitSuccess(string result)
	{
		numTentativas++;

		if (numTentativas > 5)
		{
			GameLost();
			return;
		}

		UpdateAtual(result);
		EmitSignal(SignalName.MathSuccess, result);

		CheckWin();
	}

	private void UpdateAtual(string resultado)
	{
		string atualTexto = string.Join("", numAtual);

		if (atualTexto.StartsWith(","))
			atualTexto = "0" + atualTexto;

		atualTexto = atualTexto.Replace(",", ".");

		double atual = Convert.ToDouble(atualTexto);
		double novoValor = Convert.ToDouble(resultado);

		double soma = atual + novoValor;

		double valor = Math.Floor(soma * 1000) / 1000;

		string texto = valor.ToString("0.###");

		texto = texto.Replace(".", ",");

		if (texto.StartsWith("0,"))
			texto = texto.Substring(1);

		string[] novo = new string[4];

		int j = texto.Length - 1;

		for (int i = 3; i >= 0; i--)
		{
			if (j >= 0)
			{
				novo[i] = texto[j].ToString();
				j--;
			}
			else
			{
				novo[i] = "0";
			}
		}

		numAtual = novo;
	}

	private void CheckWin()
	{
		string atual = string.Join("", numAtual);
		string alvo = string.Join("", numAlvo);

		if (atual == alvo)
			GameWon();
	}

	public void gerarNumeroAlvo()
	{
		_rng.Randomize();

		numAlvo = new string[4];
		bool operadorGerado = false;

		for (int i = 0; i < 4; i++)
		{
			if (i != 3 && !operadorGerado)
			{
				int chance = _rng.RandiRange(1, 100);

				if (chance <= 15)
				{
					int op = _rng.RandiRange(0, 1);
					numAlvo[i] = (op == 0) ? "√" : ",";
					operadorGerado = true;
					continue;
				}
			}

			numAlvo[i] = _rng.RandiRange(0, 9).ToString();
		}
	}
}