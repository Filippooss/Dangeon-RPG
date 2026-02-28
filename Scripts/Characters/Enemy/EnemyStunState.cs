using Godot;
using System;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimationPlayer.Play(GameConstants.ANIM_STUN);

        characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {

        characterNode.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
    }


    private void HandleAnimationFinished(StringName animName)
    {
        if (characterNode.AttackAreaNode.HasOverlappingBodies())
        {
            characterNode.stateMachine.SwitchState<EnemyAttackState>();
        }
        else if (characterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            characterNode.stateMachine.SwitchState<EnemyChaseState>();
        }
        else
        {
            characterNode.stateMachine.SwitchState<EnemyIdleState>();
        }
    }
}
