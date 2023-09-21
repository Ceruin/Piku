using Godot;
using System;

public partial class Main : Node
{
    // Don't forget to rebuild the project so the editor knows about the new export variable.

    [Export]
    public PackedScene MobScene { get; set; }

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
    }
}
