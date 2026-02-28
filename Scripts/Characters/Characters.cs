using Godot;
using System;
using System.Linq;

public abstract partial class Characters : CharacterBody3D
{
    [Export] private StatResource[] statArray;
    [ExportGroup("Player Nodes")]
    [Export] private AnimationPlayer animationPlayer;
    [Export] private Sprite3D sprite3D;
    [Export] public StateMachine stateMachine;
    [Export] public Area3D HurtBoxNode { get; private set; }
    [Export] public Area3D HitBoxNode { get; private set; }
    [Export] public CollisionShape3D HitboxShapeNode { get; private set; }
    //Propeties
    public AnimationPlayer AnimationPlayer => animationPlayer;

    [ExportGroup("Ai Nodes")]
    [Export] public Path3D FollowPath { get; private set; }
    [Export] public NavigationAgent3D NavigationAgent { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }
    [Export] private Timer timer;
    public Sprite3D Sprite3D => sprite3D;
    public Vector2 direction = Vector2.Zero;
    private ShaderMaterial shader;

    public override void _Ready()
    {
        shader = Sprite3D.MaterialOverlay as ShaderMaterial;

        HurtBoxNode.AreaEntered += HurtBoxNode_AreaEntered;
        sprite3D.TextureChanged += Sprite3D_TextureChanged;
        timer.Timeout += Timer_Timeout;
    }

    private void Timer_Timeout()
    {
        shader.SetShaderParameter("active", false);
    }

    private void Sprite3D_TextureChanged()
    {
        shader.SetShaderParameter("tex", Sprite3D.Texture);
    }

    private void HurtBoxNode_AreaEntered(Area3D area)
    {

        if (area is not IHitBox hitBox) return;

        StatResource health = GetStatResource(E_Stat.Health);

        float damage = hitBox.GetDamage();

        health.StatValue -= damage;
        shader.SetShaderParameter("active", true);
        timer.Start();

        GD.Print(health.StatValue);
    }

    public StatResource GetStatResource(E_Stat stat)
    {
        return statArray.Where((element) => element.StatType == stat).FirstOrDefault();

    }

    public void Flip()
    {
        bool isNotMovingHorizontal = Velocity.X == 0;

        if (isNotMovingHorizontal) return;

        bool isMovingLeft = Velocity.X < 0;
        sprite3D.FlipH = isMovingLeft;
    }
    /// <summary>
    /// set hitbox on/off , if true hitbox is disabled
    /// </summary>
    /// <param name="flag"></param>
    public void ToggleHitbox(bool flag)
    {
        HitboxShapeNode.Disabled = flag;
    }
}
