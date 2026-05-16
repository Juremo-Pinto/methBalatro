using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

public partial class detectingCardiesYippee : Area2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("verificaCarta"))
		{
			takeCardsPos();
		}
	}

	public void takeCardsPos()
	{
		List<string> cardList = new List<string>();

		var cards = GetOverlappingAreas();
		var sortedCards = cards.OrderBy(area => area.GlobalPosition.X).ToList();

		foreach (Node2D card in sortedCards)
		{
			var cardValue = card.GetParent<GeneratorBase>();

			if (cardValue is NumberGen)
			{
				cardList.Add(Convert.ToString(cardValue.ValCard));
			}
			else if (cardValue is OperatorGen op)
			{
				cardList.Add(op.OperatorValue switch
				{
					Operators.Division => "/",
					Operators.Power => "^",
					Operators.Subtraction => "-",
					Operators.Multiplication => "*",
					Operators.Addition => "+",
					Operators.SquareRoot => "√",

					_ => throw new ArgumentOutOfRangeException()
				});
			}
		}

		try
		{
			// valida expressão antes de processar
			if (!IsValidExpression(cardList))
			{
				GD.PrintErr("Conta inválida.");
				MathEventBus.Instance.EmitError();
				return;
			}

			string strResult = BuildExpression(cardList);

			DataTable dt = new DataTable();

			var valResultMath = dt.Compute(strResult, "");

			GD.Print($"{strResult} = {valResultMath}");
			MathEventBus.Instance.EmitSuccess(Convert.ToString(valResultMath));
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Erro matemático: {ex.Message}");
		}
	}

	private bool IsValidExpression(List<string> tokens)
	{
		if (tokens.Count == 0)
		{
			GD.PrintErr("Expressão vazia.");
			return false;
		}

		string[] operators = { "+", "-", "*", "/", "^" };

		for (int i = 0; i < tokens.Count - 1; i++)
		{
			bool currentIsNumber = double.TryParse(tokens[i], out _);
			bool nextIsNumber = double.TryParse(tokens[i + 1], out _);

			// dois operadores juntos
			bool currentIsOperator = operators.Contains(tokens[i]);
			bool nextIsOperator = operators.Contains(tokens[i + 1]);

			if (currentIsOperator && nextIsOperator)
			{
				GD.PrintErr("Dois operadores seguidos.");
				return false;
			}

			if (currentIsNumber && tokens[i + 1] == "√")
			{
				GD.PrintErr("Número seguido de raiz sem operador.");
				return false;
			}

			// divisão por zero
			if (tokens[i] == "/")
			{
				if (double.TryParse(tokens[i + 1], out double divisor))
				{
					if (divisor == 0)
					{
						GD.PrintErr("Divisão por zero.");
						return false;
					}
				}
			}
		}

		return true;
	}

	private string BuildExpression(List<string> tokens)
	{
		for (int i = 0; i < tokens.Count; i++)
		{
			// POTÊNCIA
			if (tokens[i] == "^")
			{
				// segurança
				if (i == 0 || i == tokens.Count - 1)
				{
					throw new Exception("Potência mal formada.");
				}

				string left = tokens[i - 1];
				string right = tokens[i + 1];

				double leftValue = Convert.ToDouble(left);
				double rightValue = Convert.ToDouble(right);

				double result = Math.Pow(leftValue, rightValue);

				tokens[i - 1] = result.ToString();

				tokens.RemoveAt(i);
				tokens.RemoveAt(i);

				i--;
			}

			// RAIZ
			else if (tokens[i] == "√")
			{
				// precisa existir algo depois
				if (i == tokens.Count - 1)
				{
					throw new Exception("Raiz sem valor.");
				}

				string right = tokens[i + 1];

				double number = Convert.ToDouble(right);

				// raiz negativa
				if (number < 0)
				{
					throw new Exception("Raiz de número negativo.");
				}

				double result = Math.Sqrt(number);

				tokens[i] = result.ToString();

				tokens.RemoveAt(i + 1);
			}
		}

		return string.Join("", tokens);
	}
}