using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    private int comboCounter = 1;
    private int maxComboCount = 2;
    [Export] private Timer comboTimerNode;
    [Export] private PackedScene lightningScene;

    public override void _Ready() {
        base._Ready();
        //reset combo
        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK + comboCounter,-1 ,1.5f);

        characterNode.AnimationPlayer.AnimationFinished += AnimationPlayer_AnimationFinished;

        characterNode.HitBoxNode.BodyEntered += HitBoxNode_BodyEntered;
    }

    private void HitBoxNode_BodyEntered(Node3D body) {
        if (comboCounter != maxComboCount) {
            return;
        }
        Node3D lightning = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition;

    }

    protected override void ExitState() {
        characterNode.AnimationPlayer.AnimationFinished -= AnimationPlayer_AnimationFinished;
        characterNode.HitBoxNode.BodyEntered -= HitBoxNode_BodyEntered;

        comboTimerNode.Start();

    }
    private void AnimationPlayer_AnimationFinished(StringName animName) {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);

        characterNode.ToggleHitbox(true);
        //otan teliosi to attack animation alazoume state
        characterNode.stateMachine.SwitchState<PlayerIdleState>();

    }
    //animation call
    private void PerformHit() {

        Vector3 newPosition = characterNode.Sprite3D.FlipH? Vector3.Left:Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;
        //set hitbox position
        characterNode.HitBoxNode.Position = newPosition;
        //we enable the hit box
        characterNode.ToggleHitbox(false);
    }

}
