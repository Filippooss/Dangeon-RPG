using Godot;
using System;

public partial class SceneManager : Node
{
    [Export] public PackedScene Gameplay { get; private set; }
    [Export] private Node3D current_gameplay;
    private Node root;
    public override void _Ready()
    {
        root = GetTree().Root.GetNode("Main");

        GameEvents.OnRestartGame += HandleRestartGame;
    }

    private void HandleRestartGame()
    {
        UnloadScene();
        LoadScene(Gameplay);
    }

    public void LoadScene(PackedScene newScene)
    {
        current_gameplay = newScene.Instantiate<Node3D>();
        root.AddChild(current_gameplay);
    }

    public void UnloadScene()
    {
        current_gameplay.QueueFree();
    }
}
