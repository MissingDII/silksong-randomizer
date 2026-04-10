using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using UnityEngine.SceneManagement;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.ReportToolUnlocked), typeof(ToolItemType))]
    public class PlayerDataSetToolDataPatch
    {
        //  public static void ReportToolUnlocked(ToolItemType type)
        static bool Prefix(ToolItemType type)
        {
            var currentScene = SceneManager.GetActiveScene().name;
            if (SceneNames.Organ_01.Equals(currentScene))
            {
                var parryToolSaveData = ToolItemManager.GetToolByName(ToolsIds.CROSS_STITCH).SavedData;
                parryToolSaveData.IsUnlocked = false;
                parryToolSaveData.HasBeenSeen = false;
                ToolItemManager.GetToolByName(ToolsIds.CROSS_STITCH).alternateUnlockedTest = new PlayerDataTest();
                PlayerData.instance.SetToolData(ToolsIds.CROSS_STITCH, parryToolSaveData);
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
