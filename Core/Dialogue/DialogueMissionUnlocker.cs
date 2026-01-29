using MissionFramework.Core.Framework;

namespace MissionFramework.Core.Dialogue;

/// <summary>
/// Unlocks missions based on completed dialogues
/// </summary>
public class DialogueMissionUnlocker : ModSystem
{
    public override void PostSetupContent()
    {
        DialogueManager.OnDialogueEnd += HandleDialogueEnd;
    }

    public override void Unload()
    {
        DialogueManager.OnDialogueEnd -= HandleDialogueEnd;
    }

    private static void HandleDialogueEnd(string dialogueKey)
    {
        var missionPlayer = Main.LocalPlayer.GetModPlayer<MissionPlayer>();

        switch (dialogueKey)
        {
            case "JourneysBegin.Crash":
                missionPlayer.UnlockMission(MissionID.JourneysBegin);
                break;
        }
    }
}