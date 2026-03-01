using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    [Export] private Main main;
    private Dictionary<ContainerType, UIContainer> containers;

    private bool canPause;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer).Cast<UIContainer>().ToDictionary((element) => element.Container);

        containers[ContainerType.Start].Show();
        containers[ContainerType.Start].ButtonNode.Pressed += HundleButtonStartPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePauseButtonPressed;
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardButtonPressed;
        containers[ContainerType.Victory].ButtonNode.Pressed += HandleVictoryButtonPressed;
        containers[ContainerType.Defeat].ButtonNode.Pressed += HandleDefeatButtonPressed;

        GameEvents.OnEndGame += GameEvents_OnEndGame;

        GameEvents.OnVictory += GameEvents_OnVictory;

        GameEvents.OnReward += GameEvents_OnReward;
    }

    public override void _Input(InputEvent @event)
    {

        if (!canPause)
        {
            return;
        }
        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }

        containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;

    }

    private void GameEvents_OnVictory()
    {
        GD.Print("UI victory event");
        canPause = false;

        containers[ContainerType.Stats].Hide();
        containers[ContainerType.Victory].Show();

        GetTree().Paused = true;
    }

    private void GameEvents_OnEndGame()
    {
        canPause = false;

        containers[ContainerType.Defeat].Show();

        GetTree().Paused = true;
    }

    private void HandlePauseButtonPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Pause].Visible = false;
    }

    private void HundleButtonStartPressed()
    {
        canPause = true;

        containers[ContainerType.Start].Hide();
        containers[ContainerType.Stats].Show();

        GameEvents.RaiseStartGame();
    }
    private void GameEvents_OnReward(RewardResource obj)
    {
        canPause = false;

        GetTree().Paused = true;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Reward].Visible = true;

        containers[ContainerType.Reward].TextureNode.Texture = obj.SpriteTexture;
        containers[ContainerType.Reward].LabelNode.Text = obj.Description;
    }
    private void HandleRewardButtonPressed()
    {
        canPause = true;

        GetTree().Paused = false;

        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Reward].Visible = false;
    }

    private void HandleVictoryButtonPressed()
    {
        canPause = false;
        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Victory].Visible = false;

        GameEvents.RaiseRestartGame();
    }

    private void HandleDefeatButtonPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Defeat].Hide();
        GameEvents.RaiseRestartGame();
        GameEvents.RaiseStartGame();
    }
}
