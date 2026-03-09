using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;
using System.Collections.Generic;

namespace Silkipelago.Items
{
    public class SilksongItemManager : ItemManager
    {
        public static int _itemToReceive = 0;
        private ILogger _logger;
        private static SilksongItemManager _instance;

        public static SilksongItemManager Instance
        {
            set => _instance = value;
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("SilksongItemManager not initialized. Set instance first.");
                }
                return _instance;
            }
        }

        public SilksongItemManager(ILogger logger, SilksongArchipelagoClient archipelago,
            IEnumerable<ReceivedItem> itemsAlreadyProcessed) : base(archipelago, itemsAlreadyProcessed)
        {
            _logger = logger;
            PlayerDataManager.Init(logger);
        }

        protected override void ProcessItem(ReceivedItem receivedItem, bool immediatelyIfPossible)
        {
            ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedItems.Add(receivedItem);
            if (receivedItem.ItemName.Contains("Rosaries"))
            {
                PlayerDataManager.addRosary(receivedItem.ItemName);
                return;
            }
            if (receivedItem.ItemName.Contains("Shell Shards"))
            {
                PlayerDataManager.addShards(receivedItem.ItemName);
                return;
            }
            var inGameName = ArchipelagoIds.GetInGameName(receivedItem.ItemName);
            if (inGameName == null)
            {
                _logger.LogWarning($"Unrecognised Item name: {receivedItem.ItemName}");
                return;
            }
            if (PlayerDataStrings.ABILITIES.Contains(inGameName))
            {
                // must be an ability to modifiy on playerData
                PlayerDataManager.ChangeBooleanValue(inGameName, true);
                return;
            }
            if (CollectablesStrings.TOOLCRESTUPGRADE.Contains(inGameName))
            {
                CollectiblesManager.addOneCollectible(inGameName);
                return;
            }

            //if (TryHandleReceivedPerk(receivedItem))
            //{
            //    return;
            //}

            //if (_trapExecutor.TryHandleReceivedTrap(receivedItem))
            //{
            //    return;
            //}
        }
    }
}
