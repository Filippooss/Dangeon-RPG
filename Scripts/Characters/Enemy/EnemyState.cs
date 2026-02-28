using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;
    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(E_Stat.Health).OnZero += HandleZeroHealth;
    }
    protected Vector3 GetPointGlovalPosition(int index)
    {

        Vector3 localPos = characterNode.FollowPath.Curve.GetPointPosition(index);

        Vector3 globalPos = characterNode.FollowPath.GlobalPosition;

        return localPos + globalPos;
    }
    protected void Move()
    {
        characterNode.NavigationAgent.GetNextPathPosition();

        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);

        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        characterNode.stateMachine.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        characterNode.stateMachine.SwitchState<EnemyDeathState>();
    }
}

