using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System.Collections.Generic;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public class SilksongItemManager : ItemManager
    {
        public static int _itemToReceive = 0;
        private ILogger _logger;


        public SilksongItemManager(ILogger logger, SilksongArchipelagoClient archipelago,
            IEnumerable<ReceivedItem> itemsAlreadyProcessed) : base(archipelago, itemsAlreadyProcessed)
        {
            _logger = logger;
            PlayerDataHandler.Init(logger);
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
                    _logger.LogWarning($"Unrecognised Item name: {itemName}");
                    return;
                }

                if (PlayerDataStrings.ABILITIES.Contains(inGameName) || PlayerDataStrings.KEYS.Contains(inGameName) || PlayerDataStrings.MELODIES.Contains(inGameName))
                    PlayerDataHandler.ChangeBooleanValue(inGameName, true);
                else if (ToolsStrings.SILK_ABILITIES.Contains(inGameName))
                    ToolItemHandler.unlockTool(inGameName);
                else if (CollectablesStrings.TOOLCRESTUPGRADE.Contains(inGameName) || CollectablesStrings.COLLECTABLESKEYS.Contains(inGameName))
                    CollectiblesHandler.addOneCollectible(inGameName);
                else if (PlayerDataStrings.SHRINES.Contains(inGameName))
                    ShrineBellHandler.addBell(inGameName);
                else
                    _logger.LogWarning($"No handler for item: {inGameName}");
            }
        }
    }
}
