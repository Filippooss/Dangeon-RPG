using Godot;
using System;

public partial class Main : Node3D
{
    [Export] public SceneManager SceneManager { get; private set; }
    public override void _Ready()
    {
        GetTree().Paused = true;
    }
}
