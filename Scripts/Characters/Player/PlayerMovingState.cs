using Godot;
using System;

public partial class PlayerMovingState : PlayerState
{
	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10f;

	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.direction == Vector2.Zero)
		{
			characterNode.stateMachine.SwitchState<PlayerIdleState>();
			return;
        }

        characterNode.Velocity = new Vector3(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= speed;

        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    public override void _Input(InputEvent @event)
	{
		CheckForAttackInput();

		if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
		{
			characterNode.stateMachine.SwitchState<PlayerDashState>();
		}
	}
}

