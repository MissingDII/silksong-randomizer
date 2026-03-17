using Silkipelago.Constants;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class CrestHandler
    {
        public static bool autoEquipCrest = false;
        public static void UnlockCrest(string crestName)
        {
            SilksongItemManager.ItemToReceive++;

            var crestToUnlock = crestName == CrestStrings.HUNTER
                ? GetHunterUpgradeLevel(crestName)
                : crestName;

            UnlockCrestInternal(crestToUnlock);
        }

        private static string GetHunterUpgradeLevel(string crestName)
        {
            var archipelagoId = ArchipelagoItemIds.GetArchipelagoName(crestName);
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
            var crestCount = archipelagoClient.GetReceivedItemCount(archipelagoId);

            return crestCount switch
            {
                1 => CrestStrings.HUNTER,
                2 => CrestStrings.HUNTER_2,
                _ => CrestStrings.HUNTER_3
            };
        }

        private static void UnlockCrestInternal(string crestName)
        {
            var crest = ToolItemManager.GetCrestByName(crestName);
            crest.Unlock();
            if (autoEquipCrest)
            {
                ToolItemManager.SetEquippedCrest(crest.name);
                autoEquipCrest = false;
            }
        }
    }

}
