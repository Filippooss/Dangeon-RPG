using Godot;
using System;

public partial class EnemyCountLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GameEvents.OnNewEnemyCount += GameEvents_OnNewEnemyCount;
	}

    private void GameEvents_OnNewEnemyCount(int obj) {
        Text = obj.ToString();
    }

}
