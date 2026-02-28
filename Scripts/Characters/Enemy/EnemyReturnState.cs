using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlovalPosition(0);
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        characterNode.NavigationAgent.TargetPosition = destination;

        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;

    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.NavigationAgent.IsNavigationFinished())
        {
            //GD.Print("reached");

            characterNode.stateMachine.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
}
