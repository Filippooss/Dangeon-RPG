using Godot;
using System;

public partial class Main : Node3D
{
    [Export] public SceneManager SceneManager { get; private set; }
    public override void _Ready()
    {
        GetTree().Paused = true;

        GameEvents.OnStartGame += HandleStartGame;
    }

    private void HandleStartGame()
    {
        StartGame();
    }

    public void StartGame()
    {
        GetTree().Paused = false;
    }
    public override void _ExitTree()
    {
        GameEvents.OnStartGame -= HandleStartGame;

    }
}
