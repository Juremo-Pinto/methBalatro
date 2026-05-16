using Godot;
using System;

public partial class openMenuClick : Button
{
    [Export]
    public PackedScene menuScene;

    public override void _Ready()
    {
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        Control instance = menuScene.Instantiate<Control>();
        CanvasLayer canvasLayer = GetParent<CanvasLayer>();

        canvasLayer.AddChild(instance);

        instance.CallDeferred(nameof(CenterControl), instance);
    }

    private void CenterControl(Control control)
    {
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;

        control.Position = (screenSize - control.Size) / 2;
    }
}