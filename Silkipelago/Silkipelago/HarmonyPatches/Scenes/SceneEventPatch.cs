using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
using System.Linq;

namespace Silkipelago.HarmonyPatches.Scenes
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.BeginSceneTransition))]
    public static class SceneEventPatch
    {
        private const int MAX_BELLS = 5;
        public static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void Prefix(GameManager.SceneLoadInfo info)
        {
            BasePatch.SafeExecuteVoid(() => HandleSceneLoading(info), nameof(SceneEventPatch), nameof(Prefix));
        }

        private static void HandleSceneLoading(GameManager.SceneLoadInfo sceneInfo)
        {
            var sceneName = sceneInfo.SceneName;
            Logger.LogInfo($"Loading scene for {sceneName}");
            if (sceneName == "Coral_Judge_Arena")
            {
                HandleCoralJudgeArena();
            }
            Logger.LogInfo($"{SceneNames.Bone_East_12}");
            if (sceneName.Equals(SceneNames.Bone_East_12))
            {
                ForceLaceNotLeftDocks();
            }
        }

        private static void ForceMoorwingToAppear()
        {
            var location = ArchipelagoLocationIds.GetArchipelagoName(PlayerDataIds.VAMPIRE_GNAT_BOSS_DEFEATED);
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            if (!locationChecker.IsLocationChecked(location))
            {
                PlayerData.instance.visitedBellhart = false;
                PlayerData.instance.visitedBellhartHaunted = false;
                PlayerData.instance.visitedBellhartSaved = false;

            }
        }

        private static void ForceLaceNotLeftDocks()
        {
            var location = ArchipelagoLocationIds.GetArchipelagoName(PlayerDataIds.LACE_DEFEATED);
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            if (!locationChecker.IsLocationChecked(location))
            {
                PlayerData.instance.laceLeftDocks = false;
                PlayerData.instance.defeatedLace1 = false;
                PlayerData.instance.encounteredLace1Grotto = false;
                PlayerData.instance.visitedCitadel = false;
            }
        }

        private static void HandleCoralJudgeArena()
        {
            var bellcount = ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount("Grand Gate Bell");
            var i = 1;
            foreach (var shrineName in PlayerDataIds.SHRINES)
            {
                if (i <= bellcount)
                {
                    SilksongItemManager.ItemToReceive++;
                    PlayerData.instance.SetBool(shrineName, true);
                }
                else
                {
                    PlayerData.instance.SetBool(shrineName, false);
                }
                i++;
            }
        }
        private static int CountReceivedBells()
        {
            return PlayerDataIds.SHRINES
                .Select(bell => ArchipelagoItemIds.GetArchipelagoName(bell))
                .Count(itemId => ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount(itemId) > 0);
        }

        private static void UpdateBellQuestCompletion()
        {
            var bellCount = CountReceivedBells();
            var quest = QuestManager.GetQuest(QuestIds.GRAND_GATE_BELLSHRINES);
            var completion = quest.Completion;

            if (completion.IsAccepted)
            {
                if (bellCount >= MAX_BELLS)
                {
                    completion.SetCompleted();
                }
            }
            else
            {
                completion.WasEverCompleted = false;
                completion.IsCompleted = false;
            }
            quest.Completion = completion;
        }
    }
}
