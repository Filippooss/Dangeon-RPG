using Godot;
using System;
using Godot.Collections;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    private CharacterBody3D target;
    [Export] private Timer timer;
    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;

        timer.Timeout += Timer_Timeout;
        characterNode.AttackAreaNode.BodyEntered += AttackAreaNode_BodyEntered;
        characterNode.ChaseAreaNode.BodyExited += ChaseAreaNode_BodyExited;
    }


    protected override void ExitState() {
        timer.Timeout -= Timer_Timeout;
        characterNode.AttackAreaNode.BodyEntered -= AttackAreaNode_BodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= ChaseAreaNode_BodyExited;
    }

    public override void _PhysicsProcess(double delta) {
        Move();
    }

    private void Timer_Timeout() {
        destination = target.GlobalPosition;

        characterNode.NavigationAgent.TargetPosition = destination;
    }
    private void ChaseAreaNode_BodyExited(Node3D body) {
        characterNode.stateMachine.SwitchState<EnemyReturnState>();
    }
    private void AttackAreaNode_BodyEntered(Node3D body) {
        characterNode.stateMachine.SwitchState<EnemyAttackState>();

    }


}
