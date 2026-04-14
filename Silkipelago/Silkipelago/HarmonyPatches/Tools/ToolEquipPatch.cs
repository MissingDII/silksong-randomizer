using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;
using UnityEngine;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.AutoEquip), typeof(ToolItem))]
    public static class ToolEquipPatch
    {
        static bool Prefix(ToolItem tool)
        {
            return BasePatch.SafeExecute(() => HandleAutoEquip(tool), nameof(ToolEquipPatch), nameof(Prefix));
        }

        private static bool HandleAutoEquip(ToolItem tool)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
            BasePatch.Logger.LogInfo($"[ToolItemManager] AutoEquip called for: {tool.name}");

            if (ToolsIds.CROSS_STITCH.Equals(tool.name) && archipelagoClient.SlotData.CombatAbilitiesRandomized)
            {
                var archipelagoName = ArchipelagoItemIds.GetArchipelagoName(ToolsIds.CROSS_STITCH);
                var hasReceivedCrossStitch = ArchipelagoPlugin.App.ArchipelagoClient.HasReceivedItem(archipelagoName);
                if (!hasReceivedCrossStitch)
                {
                    //lock cross stitch again if not unlocked
                    var parryToolSaveData = ToolItemManager.GetToolByName(ToolsIds.CROSS_STITCH).SavedData;
                    parryToolSaveData.IsUnlocked = false;
                    parryToolSaveData.HasBeenSeen = false;
                    ToolItemManager.GetToolByName(ToolsIds.CROSS_STITCH).alternateUnlockedTest = new PlayerDataTest();
                    PlayerData.instance.SetToolData(ToolsIds.CROSS_STITCH, parryToolSaveData);
                }
                SetPhantomFsmState();
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            if (ShouldBlockEquip(tool, locationChecker))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static void SetPhantomFsmState()
        {
            try
            {
                var phantomGO = GameObject.Find("Phantom");
                if (phantomGO != null)
                {
                    var playMakerFsm = phantomGO.GetComponent<PlayMakerFSM>();
                    if (playMakerFsm != null && playMakerFsm.FsmName == "Control")
                    {
                        playMakerFsm.SetState("End Pause");
                        BasePatch.Logger.LogInfo($"[SkillMessagePatch] Set Phantom Control FSM to 'End Pause'");
                    }
                    else
                    {
                        BasePatch.Logger.LogWarning($"[SkillMessagePatch] Could not find 'Control' FSM on Phantom GameObject");
                    }
                }
                else
                {
                    BasePatch.Logger.LogWarning($"[SkillMessagePatch] Phantom GameObject not found");
                }
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(SkillMessagePatch), nameof(SetPhantomFsmState), ex);
            }
        }

        private static bool ShouldBlockEquip(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsSilkAbility(tool))
                return true;

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsIds.SILK_ABILITIES.Contains(tool.name);
    }
}
