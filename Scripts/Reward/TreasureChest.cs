using Godot;
using System;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;
    [Export] private RewardResource rewardResource;

    public override void _Ready()
    {
        areaNode.BodyEntered += (body) => spriteNode.Visible = true;
        areaNode.BodyExited += (body) => spriteNode.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (!areaNode.Monitoring || !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) || !areaNode.HasOverlappingBodies())
        {
            return;
        }

        areaNode.Monitoring = false;

        GameEvents.RaiseReward(rewardResource);
    }

}
