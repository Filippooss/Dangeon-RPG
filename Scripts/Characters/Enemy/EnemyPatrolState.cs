using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    [Export(PropertyHint.Range, "0,20,0.1")] private float maxTimerTime;
    [Export] private Timer idleTimer;
    private int pointIndex = 0;

    protected override void EnterState() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;

        destination = GetPointGlovalPosition(pointIndex);
        characterNode.NavigationAgent.TargetPosition = destination;

        characterNode.NavigationAgent.NavigationFinished += NavigationAgent_NavigationFinished;
        idleTimer.Timeout += IdleTimer_Timeout;

        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState() {
        characterNode.NavigationAgent.NavigationFinished -= NavigationAgent_NavigationFinished;
        idleTimer.Timeout -= IdleTimer_Timeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;

    }


    public override void _PhysicsProcess(double delta) {
        if (!idleTimer.IsStopped()) {
            return;
        }

        Move();
    }
    private void NavigationAgent_NavigationFinished() {

        characterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new RandomNumberGenerator();
        idleTimer.WaitTime = rng.RandfRange(0, maxTimerTime);

        idleTimer.Start();


    }
    private void IdleTimer_Timeout() {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.FollowPath.Curve.PointCount);

        destination = GetPointGlovalPosition(pointIndex);
        characterNode.NavigationAgent.TargetPosition = destination;
    }
}
