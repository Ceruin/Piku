using Godot;
using System;

public partial class Main : Node
{
    // Don't forget to rebuild the project so the editor knows about the new export variable.

    [Export]
    public PackedScene MobScene { get; set; }

    public override void _Ready()
    {
        GetNode<Control>("UserInterface/Retry").Hide();
    }

    // We also specified this function name in PascalCase in the editor's connection window
    private void OnMobTimerTimeout()
    {
        // Create a new instance of the Mob scene.
        companion mob = MobScene.Instantiate<companion>();

        // Choose a random location on the SpawnPath.
        // We store the reference to the SpawnLocation node.
        var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
        // And give it a random offset.
        mobSpawnLocation.ProgressRatio = GD.Randf();

        Vector3 playerPosition = GetNode<player>("Player").Position;
        mob.Initialize(mobSpawnLocation.Position, playerPosition);

        // Spawn the mob by adding it to the Main scene.
        AddChild(mob);

        // We connect the mob to the score label to update the score upon squashing one.
        mob.Squashed += GetNode<ScoreLabel>("UserInterface/ScoreLabel").OnMobSquashed;
    }

    // We also specified this function name in PascalCase in the editor's connection window
    private void OnPlayerHit()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Control>("UserInterface/Retry").Show();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept") && GetNode<Control>("UserInterface/Retry").Visible)
        {
            // This restarts the current scene.
            GetTree().ReloadCurrentScene();
        }
    }
}
