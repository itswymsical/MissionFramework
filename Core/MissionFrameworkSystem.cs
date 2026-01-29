using MissionFramework.Core.Framework;

namespace MissionFramework.Core;

public class MissionFrameworkSystem : ModSystem
{
    public static MissionFrameworkSystem Instance => ModContent.GetInstance<MissionFrameworkSystem>();
    public static ModKeybind FFDialogueKeybind { get; private set; }
    public static ModKeybind SkipCutsceneKeybind { get; private set; }

    public override void Load()
    {
        FFDialogueKeybind = KeybindLoader.RegisterKeybind(Mod, "Fast-Forward Dialogue", "V");
        SkipCutsceneKeybind = KeybindLoader.RegisterKeybind(Mod, "Skip Cutscene", "Q");

    }
    public override void Unload()
    {
        FFDialogueKeybind = null;
        SkipCutsceneKeybind = null;
    }

    public override void PostUpdateWorld()
    {
        base.PostUpdateWorld();
        if (Main.LocalPlayer?.active == true && !Main.gameMenu)
        {
            var missionPlayer = Main.LocalPlayer.GetModPlayer<MissionPlayer>();
            foreach (var mission in missionPlayer.ActiveMissions())
            {
                mission.Update();
            }
        }
    }
}
