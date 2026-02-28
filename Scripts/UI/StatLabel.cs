using Godot;
using System;

public partial class StatLabel : Label
{
    [Export] private StatResource stateResources;

    public override void _Ready() {
        stateResources.OnUpdate += Resources_OnUpdate;

        Text = stateResources.StatValue.ToString();
    }

    private void Resources_OnUpdate() {
        Text = stateResources.StatValue.ToString();
    }
}
