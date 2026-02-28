using Godot;
using System;

public partial class Enemy : Characters
{
    public override void _Ready()
    {
        base._Ready();

        HurtBoxNode.AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area3D area)
    {
        if (area is not IHitBox hitBox) return;

        if (hitBox.CanStun() && GetStatResource(E_Stat.Health).StatValue != 0)
        {
            stateMachine.SwitchState<EnemyStunState>();
        }
    }
}
