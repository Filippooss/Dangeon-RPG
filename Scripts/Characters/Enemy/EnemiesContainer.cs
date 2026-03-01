using Godot;
using System;

/// <summary>
/// Responsible for keeping track of the enemies
/// </summary>
public partial class EnemiesContainer : Node3D
{
    private int totalEnemies = 0;
    private bool hasEnded = false;
    public override void _Ready()
    {
        totalEnemies = GetChildCount();
        GD.Print($"total enemies {totalEnemies}");
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        ChildExitingTree += EnemiesContainer_ChildExitingTree;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleEndGame()
    {
        ChildExitingTree -= EnemiesContainer_ChildExitingTree;
        hasEnded = true;
    }

    private void EnemiesContainer_ChildExitingTree(Node node)
    {
        totalEnemies--;
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        if (totalEnemies == 0)
        {
            GD.Print($"total enemies {totalEnemies}");
            GameEvents.RaiseVictory();
        }
    }

    public override void _ExitTree()
    {
        if (!hasEnded)
            ChildExitingTree -= EnemiesContainer_ChildExitingTree;
        GameEvents.OnEndGame -= HandleEndGame;

    }
}
