using Godot;
using System;

public partial class Lightning : Ability
{
    public override void _Ready() {
        animationPlayer.AnimationFinished += (animName) => QueueFree();
    }

}
