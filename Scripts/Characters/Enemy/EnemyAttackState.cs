using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;
    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);

        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();

        targetPosition = target.GlobalPosition;

        characterNode.AnimationPlayer.AnimationFinished += AnimationPlayer_AnimationFinished;

    }
    protected override void ExitState() {
        characterNode.AnimationPlayer.AnimationFinished -= AnimationPlayer_AnimationFinished;

    }

    private void AnimationPlayer_AnimationFinished(StringName animName) {
        characterNode.ToggleHitbox(true);
        
        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null) {
            Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();

            if (chaseTarget == null)
            {
                characterNode.stateMachine.SwitchState<EnemyReturnState>();
                return;
            }

            characterNode.stateMachine.SwitchState<EnemyChaseState>();
            return;
        }

        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.Sprite3D.FlipH = direction.X < 0;
    }

    //called in animation player
    private void PerformHit() {

        characterNode.ToggleHitbox(false);
        characterNode.HitBoxNode.GlobalPosition = targetPosition;

    }
}
