using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.BeginSceneTransition))]
    public static class SceneEventPatch
    {
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
                //TODO rewrite
                //HandleCoralJudgeArena();
            }
            //force lace to appear
            Logger.LogInfo($"{SceneNames.Bone_East_12}");
            if (sceneName.Equals(SceneNames.Bone_East_12))
            {
                ForceLaceNotLeftDocks();
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
    }
}
