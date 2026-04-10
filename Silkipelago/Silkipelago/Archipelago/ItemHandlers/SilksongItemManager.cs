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

                // Display item notification
                ArchipelagoPlugin.App.UIContext.ItemNotification.ShowItemNotification(itemName);

                // Try special item handlers first
                if (TryHandleSpecialItem(itemName))
                    return;

                var inGameName = ArchipelagoItemIds.GetInGameName(itemName);
                if (inGameName == null)
                {
                    Logger.LogWarning($"Unrecognised Item name: {itemName}");
                    return;
                }

                // Try generic item handlers
                if (!TryHandleItemByType(inGameName))
                    Logger.LogWarning($"No handler for item: {inGameName}");
            }
        }

        /// <summary>
        /// Handles special items that don't need the ArchipelagoItemIds lookup.
        /// </summary>
        private bool TryHandleSpecialItem(string itemName)
        {
            if (itemName.Equals("Lost Flea") || itemName.Equals("Kratt") || itemName.Equals("Giant Lost Flea") || itemName.Equals("Vog"))
            {
                FleaHandler.AddFlea();
                return true;
            }
            if (itemName.EndsWith("Rosaries"))
            {
                PlayerDataHandler.AddRosary(itemName);
                return true;
            }

            if (itemName.EndsWith("Shell Shards"))
            {
                PlayerDataHandler.addShards(itemName);
                return true;
            }

            if (itemName.EndsWith("slash"))
            {
                SlashDirectionHandler.unlockSlashDirection(itemName);
                return true;
            }

            if (itemName.Equals("Bind"))
            {
                BindHandler.unlockBind();
                return true;
            }


            return false;
        }

        /// <summary>
        /// Routes items to appropriate handlers based on their type.
        /// </summary>
        private bool TryHandleItemByType(string inGameName)
        {
            if (IsPlayerDataItem(inGameName))
            {
                PlayerDataHandler.ChangeBooleanValue(inGameName, true);
                return true;
            }

            if (IsSilkHeart(inGameName))
            {
                PlayerDataHandler.AddToIntValue(inGameName);
                return true;
            }

            if (IsToolItem(inGameName))
            {
                ToolItemHandler.unlockTool(inGameName);
                return true;
            }

            if (IsCrest(inGameName))
            {
                CrestHandler.UnlockCrest(inGameName);
                return true;
            }

            if (IsCollectible(inGameName))
            {
                CollectiblesHandler.addOneCollectible(inGameName);
                return true;
            }

            if (IsShrine(inGameName))
            {
                ShrineBellHandler.AddBell(inGameName);
                return true;
            }

            return false;
        }

        private bool IsPlayerDataItem(string inGameName) =>
            PlayerDataIds.ABILITIES.Contains(inGameName) ||
            PlayerDataIds.KEYS.Contains(inGameName) ||
            PlayerDataIds.MELODIES.Contains(inGameName) ||
            PlayerDataIds.EVA_UPGRADES.Contains(inGameName) ||
            PlayerDataIds.STATIONS.Contains(inGameName) ||
            PlayerDataIds.TUBES.Contains(inGameName);

        private bool IsSilkHeart(string inGameName) =>
            PlayerDataIds.SILK_HEART.Equals(inGameName);

        private bool IsToolItem(string inGameName) =>
            ToolsIds.SILK_ABILITIES.Contains(inGameName) ||
            ToolsIds.TOOLs.Contains(inGameName);

        private bool IsCrest(string inGameName) =>
            CrestIds.CRESTS.Contains(inGameName);

        private bool IsCollectible(string inGameName) =>
            CollectablesIds.TOOLCRESTUPGRADE.Contains(inGameName) ||
            CollectablesIds.COLLECTABLESKEYS.Contains(inGameName) ||
            CollectablesIds.ITEMS.Contains(inGameName);

        private bool IsShrine(string inGameName) =>
            PlayerDataIds.SHRINES.Contains(inGameName);
    }
}
