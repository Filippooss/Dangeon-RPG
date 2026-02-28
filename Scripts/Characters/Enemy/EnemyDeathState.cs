using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        GD.Print($"enemy death | name: {characterNode.Name}");
        characterNode.HurtBoxNode.SetDeferred("monitoring", false);
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimationPlayer.AnimationFinished += AnimationPlayer_AnimationFinished;
    }

    private void AnimationPlayer_AnimationFinished(StringName animName)
    {
        characterNode.FollowPath.QueueFree();
    }
}
