using Godot;
using Godot.Collections;
using System;

public partial class Drag : Area2D
{
	private Vector2 OriginalPos;
	private Vector2 TargetPos;
	private bool Hold;
	public bool MouseOver = false;

	private Sprite2D sprite;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("../Sprite");
		OriginalPos = sprite.Position;
		TargetPos = OriginalPos;

		InputEvent += OnMouseInput;
	}

	private void OnMouseInput(Node viewport, InputEvent @event, long shapeIdx)
	{
		if(@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left)
		{
			if (!mouseButton.Pressed)
			{
				Hold = false;
				return;
			}
			Array<Area2D> areas = GetOverlappingAreas();

			foreach (Area2D area in areas)
			{
				if (area is Drag darea && !darea.MouseOver)
				{
					continue;
				}
				if(!IsGreaterThan(area) && area.IsVisibleInTree())
				{
					return;
				}
			}

			Hold = true;
		}
	}


    public override void _MouseEnter()
    {
		MouseOver = true;
        base._MouseEnter();
    }

    public override void _MouseExit()
    {
		MouseOver = false;
        base._MouseExit();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Hold)
		{
			GlobalPosition = GetViewport().GetMousePosition();
		}

		sprite.GlobalPosition = sprite.GlobalPosition.Slerp(GlobalPosition, .14f);
	}
}
