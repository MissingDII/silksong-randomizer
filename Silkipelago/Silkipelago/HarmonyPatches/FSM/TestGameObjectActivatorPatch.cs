using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;

namespace Silkipelago.HarmonyPatches.FSM
{
    [HarmonyPatch(typeof(TestGameObjectActivator))]
    [HarmonyPatch(nameof(TestGameObjectActivator.Evaluate))]
    public class TestGameObjectActivatorPatch
    {
        public static bool Prefix(TestGameObjectActivator __instance)
        {
            return BasePatch.SafeExecute(() => HandleEvaluate(__instance), nameof(TestGameObjectActivatorPatch), nameof(Prefix));
        }

        private static bool HandleEvaluate(TestGameObjectActivator instance)
        {
            if (instance.gameObject.name.Equals("Couriers States"))
            {
                var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                if (saveData.Pill)
                {
                    instance.activateGameObject.SetActive(true);
                    instance.deactivateGameObject.SetActive(false);
                    EventRegister.SendEvent(instance.activateEventRegister);
                }
                else
                {
                    instance.activateGameObject.SetActive(false);
                    instance.deactivateGameObject.SetActive(true);
                    EventRegister.SendEvent(instance.deactivateEventRegister);
                }
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
