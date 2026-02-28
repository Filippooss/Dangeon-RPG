using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimationPlayer.AnimationFinished += AnimationPlayer_AnimationFinished;
    }

    private void AnimationPlayer_AnimationFinished(StringName animName) {
        GameEvents.RaiseOnEndGame();

        characterNode.QueueFree();
    }
}
