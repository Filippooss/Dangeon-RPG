using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
	[Export] private Timer dashTimer;
    [Export] private Timer cooldownTimerNode; 
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10;
    [Export] private PackedScene bombScene;

    public override void _Ready()
    {
        base._Ready();

        dashTimer.Timeout += HandleDashTimeout;
        //cannot transition to this state if the timer is running
        CanTransition = () => cooldownTimerNode.IsStopped();
    }


    public override void _PhysicsProcess(double delta) {

        characterNode.MoveAndSlide();
        characterNode.Flip();


    }
    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new Vector3(characterNode.direction.X, 0, characterNode.direction.Y);

        if (characterNode.Velocity == Vector3.Zero) {
            characterNode.Velocity = characterNode.Sprite3D.FlipH ? Vector3.Left : Vector3.Right;
        }

        characterNode.Velocity *= speed;
        dashTimer.Start();

        Node3D bomb = bombScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = characterNode.Position;
    }

    private void HandleDashTimeout() {
        cooldownTimerNode.Start();
		characterNode.Velocity = Vector3.Zero;
		characterNode.stateMachine.SwitchState<PlayerIdleState>();
    }
}
