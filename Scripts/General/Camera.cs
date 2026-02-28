using Godot;
using System;

public partial class Camera : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionFromTarget;
 
    public override void _Ready() {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += GameEvents_OnEndGame;
    }

    private void GameEvents_OnEndGame() {
        Reparent(GetTree().CurrentScene);
    }

    private void HandleStartGame() {
        Reparent(target);

        Position = positionFromTarget;
    }
}
