using Archipelago.MultiClient.Net.Helpers;
using BepInEx;
using BepInEx.Configuration;
using GlobalEnums;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using Silkipelago.context;
using Silkipelago.HarmonyPatches;
using Silkipelago.Logging;
using Silkipelago.Utils;
using System.IO;
using System.Linq;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public partial class ArchipelagoPlugin : BaseUnityPlugin
    {

        private static ConfigEntry<KeyCode>? _addMoneyKey;
        private ILogger _logger;
        private Harmony _harmony;
        private static PatchInitializer _patcherInitializer;
        public static RandomizerApp App { get; private set; }



        private void Awake()
        {

            // Plugin startup logic
            Logger.LogInfo($"Loading {MyPluginInfo.PLUGIN_GUID}...");
            _addMoneyKey = this.Config.Bind<KeyCode>("KeyCode", "addMoneyKey", KeyCode.Keypad0, "key to add money and unlock abilities");
            try
            {
                _logger = new LogHandler(Logger);
                _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
                _harmony.PatchAll();

            }
            catch (FileNotFoundException fnfe)
            {
                _logger.LogError($"Cannot load {MyPluginInfo.PLUGIN_GUID}: A Necessary Dependency is missing [{fnfe.FileName}]");
                throw;
            }

            initializeApp();
            //refresh settings so that patch will apply from json
            UnityConverterInitializer.RefreshSettingsFromConfig();

            _logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnItemReceived(ReceivedItemsHelper receivedItemsHelper)
        {
            if (App.ArchipelagoClient == null || App.ItemManager == null || !App.ArchipelagoClient.IsConnected || !GameManager.instance.IsGameplayScene())
            {
                return;
            }

            App.ItemManager.ReceiveAllNewItems();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                var d = PlayerData.instance.Collectables.GetData("Ward Key");
                d.Amount = 1;
                PlayerData.instance.Collectables.SetData("Ward Key", d);
                var test = ToolItemManager.Instance;
                var test2 = test.toolItems;
                _logger.LogInfo("List each tool name");
                foreach (var item in test2.list)
                {
                    _logger.LogInfo(item.name);
                }
                _logger.LogInfo("test with playerData");
                return;
            }

            if (Input.GetKeyDown(_addMoneyKey!.Value))
            {
                _logger.LogInfo("Enable PowerUp");
                var playerData = PlayerData.instance;
                playerData.GetAllPowerups();
                Logger.LogInfo("here with playerInstance");
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                _logger.LogInfo("Disable PowerUp");
                var playerData = PlayerData.instance;
                playerData.hasDash = false;
                playerData.hasBrolly = false;
                playerData.hasWalljump = false;
                playerData.hasDoubleJump = false;
                Logger.LogInfo("here with playerInstance");
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                _logger.LogInfo("Teleport somewhere");
                var playerData = PlayerData.instance;
                var location = "Bone_05";
                var entry = "left1";
                var gateLocation = GatePosition.left;
                Logger.LogInfo("about to teleport");
                SceneLoader.LoadScene(location, entry, gateLocation);
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                _logger.LogInfo("Show UI button");
                App.UIContext.MenuUI.Toggle();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                var items = QuestManager._allFullQuests
                    .Select(x => x.name)
                    .ToList();
                var json = JsonConvert.SerializeObject(items, Formatting.Indented);
                var filePath = System.IO.Path.Combine(Paths.PluginPath, "quests.json");
                System.IO.File.WriteAllText(filePath, json);
                _logger.LogInfo($"quests exported to: {filePath}");
                return;
            }
            return;
        }
    }
}
