using Godot;
using System;

/// <summary>
/// Responsible for keeping track of the enemies
/// </summary>
public partial class EnemiesContainer : Node3D
{
    private int totalEnemies = 0;
    public override void _Ready()
    {
        totalEnemies = GetChildCount();

        GameEvents.RaiseNewEnemyCount(totalEnemies);

        ChildExitingTree += EnemiesContainer_ChildExitingTree;
    }

    private void EnemiesContainer_ChildExitingTree(Node node)
    {
        totalEnemies--;
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        if (totalEnemies == 0)
        {
            GameEvents.RaiseVictory();
        }
    }

    public override void _ExitTree()
    {
        ChildExitingTree -= EnemiesContainer_ChildExitingTree;
    }
}
