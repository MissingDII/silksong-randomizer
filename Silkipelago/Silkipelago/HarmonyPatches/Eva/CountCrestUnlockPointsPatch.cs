using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using Silkipelago.HarmonyPatches.FSM;
using System;
using UnityEngine;

namespace Silkipelago.HarmonyPatches.Eva
{
    [HarmonyPatch(typeof(CountCrestUnlockPoints))]
    [HarmonyPatch(nameof(CountCrestUnlockPoints.OnEnter))]
    public class CountCrestUnlockPointsPatch
    {
        // public override void OnEnter()
        public static void Prefix(CountCrestUnlockPoints __instance)
        {
            BasePatch.SafeExecuteVoid(() => HandleCountCrest(__instance), nameof(CountCrestUnlockPoints), nameof(Prefix));
        }

        private static bool HandleCountCrest(CountCrestUnlockPoints __instance)
        {
            var locationchecker = ArchipelagoPlugin.App.LocationChecker;
            if (locationchecker.LocationExists(LocationConstants.EvaUpgradeLocation))
            {
                var unlockedSlots = CountUnlockedCrestSlots();

                for (var i = 0; i <= unlockedSlots; i++)
                {
                    ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation($"{LocationConstants.EvaSlotLocationPrefix}{i} Slots");
                }
                __instance.StoreCurrentPoints = 0;
                __instance.StoreMaxPoints = 10000;
                //SetCrestUpgradeShrineState();
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static void SetCrestUpgradeShrineState()
        {
            try
            {
                var crestShrineGO = GameObject.Find("Crest Upgrade Shrine");
                if (crestShrineGO != null)
                {
                    var playMakerFsm = crestShrineGO.GetComponent<PlayMakerFSM>();
                    if (playMakerFsm != null && playMakerFsm.FsmName == "Dialogue")
                    {
                        playMakerFsm.SetState("End");
                        BasePatch.Logger.LogInfo($"[CountCrestUnlockPointsPatch] Set Crest Upgrade Shrine Control FSM to 'End'");
                    }
                    else
                    {
                        BasePatch.Logger.LogWarning($"[CountCrestUnlockPointsPatch] Could not find 'Control' FSM on Crest Upgrade Shrine GameObject");
                    }
                }
                else
                {
                    BasePatch.Logger.LogWarning($"[CountCrestUnlockPointsPatch] Crest Upgrade Shrine GameObject not found");
                }
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(CountCrestUnlockPointsPatch), nameof(SetCrestUpgradeShrineState), ex);
            }
        }

        private static int CountUnlockedCrestSlots()
        {
            try
            {
                var unlockedCount = 0;
                var toolCrestList = ToolItemManager.GetAllCrests();

                if (toolCrestList == null)
                    return 0;

                foreach (var crest in toolCrestList)
                {
                    // Match base game logic: filter hidden, non-base versions, and upgraded crests
                    if (crest.IsHidden || !crest.IsBaseVersion || crest.IsUpgradedVersionUnlocked)
                        continue;

                    // Only count if crest is unlocked
                    if (!crest.IsUnlocked)
                        continue;

                    // Count unlocked slots for this crest
                    var slots = crest.Slots;
                    var saveData = crest.SaveData;

                    if (slots == null || saveData.Slots == null)
                        continue;

                    for (var i = 0; i < slots.Length; i++)
                    {
                        // Increment if slot is not locked OR if savedata slot is unlocked
                        if (!slots[i].IsLocked || (i < saveData.Slots.Count && saveData.Slots[i].IsUnlocked))
                            unlockedCount++;
                    }
                }

                return unlockedCount;
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(FSMUtilityPatch), nameof(CountUnlockedCrestSlots), ex);
                return 0;
            }
        }
    }
}
