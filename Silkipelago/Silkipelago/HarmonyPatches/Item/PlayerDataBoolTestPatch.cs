using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(PlayerDataBoolTest))]
    [HarmonyPatch(nameof(PlayerDataBoolTest.OnEnter))]
    public class PlayerDataBoolTestPatch
    {
        public static bool Prefix(PlayerDataBoolTest __instance)
        {
            return BasePatch.SafeExecute(() => HandleTest(__instance), nameof(CollectableItemPatch), nameof(Prefix));
        }

        private static bool HandleTest(PlayerDataBoolTest instance)
        {
            if (PlayerDataIds.ALL_FLEAS.Contains(instance.boolName.Name))
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                var archipelagoName = ArchipelagoLocationIds.GetArchipelagoName(instance.boolName.Name);
                if (!locationChecker.IsLocationChecked(archipelagoName))
                {
                    instance.Fsm.Event(instance.isFalse);
                    instance.Finish();
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
