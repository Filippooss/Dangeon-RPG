using Godot;
using System;

public partial class PlayerIdleState : PlayerState {



    public override void _PhysicsProcess(double delta) {
        if (characterNode.direction != Vector2.Zero) {
            characterNode.stateMachine.SwitchState<PlayerMovingState>();
        }

    }

    public override void _Input(InputEvent @event) {

        CheckForAttackInput();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            characterNode.stateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
