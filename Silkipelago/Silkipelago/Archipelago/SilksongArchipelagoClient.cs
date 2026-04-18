using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Interfaces;
using KaitoKid.ArchipelagoUtilities.Net.Json;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago.SlotData;
using Silkipelago.HarmonyPatches.Hero;
using Silkipelago.IdTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Silkipelago.Archipelago
{
    public class SilksongArchipelagoClient : ArchipelagoClient
    {
        private static IJsonLoader _jsonLoader = new NewtonsoftJsonLoader();
        public bool _shouldDoInitialLoad = false;

        public override string GameName => "Silksong";
        public override string ModName => "Silkipelago";
        public override string ModVersion => MyPluginInfo.PLUGIN_VERSION;

        public SilksongSlotData SlotData => (SilksongSlotData)_slotData;

        public SilksongArchipelagoClient(ILogger logger, Action<ReceivedItemsHelper> itemReceivedFunction) :
            base(logger, new DataPackageCache(new ArchipelagoItemLoader(_jsonLoader), new SilksongLocationLoader(_jsonLoader), "silksong", "BepInEx", "plugins", "Silkipelago", "IdTables"), itemReceivedFunction)
        {
        }

        protected override void InitializeSlotData(string slotName, Dictionary<string, object> slotDataFields)
        {
            _slotData = new SilksongSlotData(slotName, slotDataFields, Logger);
        }

        protected override void OnPacketReceived(ArchipelagoPacketBase packet)
        {

        }

        protected override void OnMessageReceived(LogMessage message)
        {
            var fullMessage = string.Join(" ", message.Parts.Select(str => str.Text));
            Logger.LogInfo(fullMessage);
        }

        protected override void KillPlayerDeathLink(DeathLink deathLinkOptions)
        {
            var heroController = HeroController.instance;
            if (heroController == null)
            {
                Logger.LogError("HeroController instance not found!");
                return;
            }

            Logger.LogInfo($"Receiving Death Link from {deathLinkOptions.Source} ({deathLinkOptions.Cause})");

            // Kill the player by dealing massive damage through the proper damage system
            // This ensures all game logic (health reduction, death detection, animations, etc.) is triggered
            try
            {
                DiePatch.receivedDeathLink = true;
                // Use reflection to call TakeDamage with the correct parameters
                var takeDamageMethod = typeof(HeroController).GetMethod("TakeDamage");
                if (takeDamageMethod != null)
                {
                    takeDamageMethod.Invoke(heroController, new object[] { 9999, null, 0, null, -1 });
                }
                else
                {
                    // Fallback: directly set health and trigger death
                    PlayerData.instance.health = 0;
                    heroController.StartCoroutine(heroController.Die(false, false));
                    Logger.LogInfo("Used fallback death method");
                }
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(SilksongArchipelagoClient), nameof(KillPlayerDeathLink), ex);
                // Fallback if something goes wrong
                PlayerData.instance.health = 0;
                heroController.StartCoroutine(heroController.Die(false, false));
            }
        }
    }
}
