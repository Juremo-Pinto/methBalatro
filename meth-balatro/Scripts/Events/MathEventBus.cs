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

	public override void _Ready()
	{
		if (Instance != null && Instance != this)
		{
			QueueFree();
			return;
		}

		Instance = this;
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
}