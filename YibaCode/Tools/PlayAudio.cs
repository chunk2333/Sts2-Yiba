using Godot;

namespace Yiba.YibaCode.Tools;

public class PlayAudio
{
    public static void Play(string path)
    {
        var player = new AudioStreamPlayer();

        player.Stream = GD.Load<AudioStream>(path);

        // ⭐ 关键：指定 Bus
        player.Bus = "SFX"; 

        var tree = Engine.GetMainLoop() as SceneTree;
        tree.Root.AddChild(player);

        player.Finished += () => player.QueueFree();

        player.Play();
    }
}