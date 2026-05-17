using Godot;
using System;

public partial class MathEventBus : Node
{
	public static MathEventBus Instance { get; private set; }

	[Signal]
	public delegate void MathErrorEventHandler();

	[Signal]
	public delegate void MathSuccessEventHandler(string result);
	public int NumTentativas { get; private set; } = 1;
	public TargetNumber NumAlvo { get; private set; }
	private RandomNumberGenerator _rng = new();

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
		NumTentativas++;
	}
	public void gerarNumeroAlvo()
	{
		_rng.Randomize();

		int comma = 0;
		int sqrtPart = 0;

		float opChance = _rng.RandfRange(1, 100);
		if (opChance <= 20)
		{
			int op = _rng.RandiRange(0, 1); // 0 = Virgula, 1 = Raiz

			if (op == 0)
				comma = _rng.RandiRange(2,3);
			else
				sqrtPart = _rng.RandiRange(2, 99);
		}

		int number = _rng.RandiRange(1,9999);

		NumAlvo = new TargetNumber(number, sqrtPart, comma);
	}
}


public struct TargetNumber
{
	public int Number;
	public int SqrRootNumber;
	int CommaPos; // 0 Is none and 1 Cannot happen (would result in "000," since 4 digit)


	public TargetNumber(int number, int sqrtPart = 0, int comma = 0)
	{
		Number = number;
		SqrRootNumber = sqrtPart;
		CommaPos = comma; // Comma and sqrt are mutually exclusive

		Check();
		;
	}

	readonly bool HasSqrt => SqrRootNumber > 0;
	readonly bool HasComma => CommaPos > 0;

	private void Check()
	{
		ValidadeSquarePart();
		ClampNumber();
	}

	public readonly float ToFloat()
	{
		if (!HasComma)
			return Number;

		float divisor = MathF.Pow(10, CommaPos - 1);

		return Number / divisor;
	}

	public void ClampNumber() //Clamps the number to 4 digits. A comma and sqrt count as a digit
	{
		SqrRootNumber = Math.Min(SqrRootNumber, 99);
		CommaPos = Math.Min(CommaPos, 4);
		int NumberDigits = 4;

		if (HasSqrt)
			NumberDigits -= (int)Math.Floor(Math.Log10(SqrRootNumber)) + 2; // +1 accounts for sqrt symbol

		if (HasComma)
			NumberDigits -= 1;

		Number %= (int)Math.Pow(10, NumberDigits);
	}

	public void ValidadeSquarePart()
	{
		bool isPerfect = IsPerfectSquare(SqrRootNumber);

		if (isPerfect)
		{
			Number += (int)Math.Sqrt(SqrRootNumber);
			SqrRootNumber = 0;
		}
	}

	public override string ToString()
	{
		Check();

		if (HasSqrt)
			return $"{Number}√{SqrRootNumber}";

		if (HasComma)
			return ToFloat().ToString().Replace('.', ',');

		return Number.ToString();
	}

	private static bool IsPerfectSquare(long number)
	{
		if (number < 0) return false; // Negative numbers cannot be perfect squares

		// Calculate the square root and cast to long to truncate any decimal
		long root = (long)Math.Sqrt(number);

		// Check if the square of the root matches the original number
		return root * root == number;
	}
}