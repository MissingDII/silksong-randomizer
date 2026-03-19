using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System.Collections.Generic;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public class SilksongItemManager : ItemManager
    {
        public static int ItemToReceive = 0;
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;


        public SilksongItemManager(SilksongArchipelagoClient archipelago,
            IEnumerable<ReceivedItem> itemsAlreadyProcessed) : base(archipelago, itemsAlreadyProcessed)
        {
        }

        protected override void ProcessItem(ReceivedItem receivedItem, bool immediatelyIfPossible)
        {
            if (!ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedItems.Contains(receivedItem))
            {
                ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedItems.Add(receivedItem);

                var itemName = receivedItem.ItemName;

                if (itemName.EndsWith("Rosaries")) { PlayerDataHandler.addRosary(itemName); return; }
                if (itemName.EndsWith("Shell Shards")) { PlayerDataHandler.addShards(itemName); return; }

                var inGameName = ArchipelagoItemIds.GetInGameName(itemName);
                if (inGameName == null)
                {
                    Logger.LogWarning($"Unrecognised Item name: {itemName}");
                    return;
                }

                if (PlayerDataIds.ABILITIES.Contains(inGameName) ||
                    PlayerDataIds.KEYS.Contains(inGameName) ||
                    PlayerDataIds.MELODIES.Contains(inGameName) ||
                    PlayerDataIds.CREST.Contains(inGameName))
                    PlayerDataHandler.ChangeBooleanValue(inGameName, true);
                else if (ToolsIds.SILK_ABILITIES.Contains(inGameName))
                    ToolItemHandler.unlockTool(inGameName);
                else if (CrestIds.CRESTS.Contains(inGameName))
                    CrestHandler.UnlockCrest(inGameName);
                else if (CollectablesIds.TOOLCRESTUPGRADE.Contains(inGameName) ||
                    CollectablesIds.COLLECTABLESKEYS.Contains(inGameName) ||
                    CollectablesIds.ITEMS.Contains(inGameName))
                    CollectiblesHandler.addOneCollectible(inGameName);
                else if (PlayerDataIds.SHRINES.Contains(inGameName))
                    ShrineBellHandler.addBell(inGameName);
                else
                    Logger.LogWarning($"No handler for item: {inGameName}");
            }
        }
    }
}
