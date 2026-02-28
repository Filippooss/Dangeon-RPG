using Godot;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(E_Stat.Health).OnZero += HandleOnZero;
    }

    protected virtual void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.stateMachine.SwitchState<PlayerAttackState>();
        }
    }

    private void HandleOnZero()
    {
        characterNode.stateMachine.SwitchState<PlayerDeathState>();
    }
}
