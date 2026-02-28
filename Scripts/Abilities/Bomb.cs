using Godot;
using System;

public partial class Bomb : Ability
{

    public override void _Ready() {
        animationPlayer.AnimationFinished += HandleExpandAnimationFinished;
    }

    private void HandleExpandAnimationFinished(StringName animName) {
        if (animName == GameConstants.ANIM_EXPAND) {
            animationPlayer.Play(GameConstants.ANIM_EXPLOSION);
        } else {
            QueueFree();
        }
    }
}
