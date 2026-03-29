using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using Silkipelago.HarmonyPatches.Item;

namespace Silkipelago.HarmonyPatches.Hero
{
    [HarmonyPatch(typeof(HeroController))]
    [HarmonyPatch(nameof(HeroController.AddToMaxSilkRegen))]
    public class AddSilkHeartPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(HeroController __instance, int amount)
        {
            return BasePatch.SafeExecute(() => HandleAddingSilkHeart(__instance, amount), nameof(CollectableItemPatch), nameof(Prefix));
        }

        private static bool HandleAddingSilkHeart(HeroController instance, int amount)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            var archipelagoId = ArchipelagoLocationIds.GetArchipelagoName(BossIds.BELL_BEAST);
            //if bell beast location exist then silk heart are randomized
            if (locationChecker.LocationExists(archipelagoId))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }

    [HarmonyPatch(typeof(HeroController))]
    [HarmonyPatch(nameof(HeroController.SetSilkRegenBlockedSilkHeart))]
    public class BlockSilkHeartPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(HeroController __instance, bool isBlocked)
        {
            return BasePatch.SafeExecute(() => HandleBlockingSilkHeart(__instance, isBlocked), nameof(CollectableItemPatch), nameof(Prefix));
        }

        private static bool HandleBlockingSilkHeart(HeroController instance, bool isBlocked)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            var archipelagoId = ArchipelagoLocationIds.GetArchipelagoName(BossIds.BELL_BEAST);
            //if bell beast location exist then silk heart are randomized
            if (locationChecker.LocationExists(archipelagoId))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
