using Archipelago.MultiClient.Net.Helpers;
using BepInEx;
using BepInEx.Configuration;
using GlobalEnums;
using HarmonyLib;
using Newtonsoft.Json.UnityConverters;
using Silkipelago.Archipelago;
using Silkipelago.Archipelago.UI;
using Silkipelago.HarmonyPatches;
using Silkipelago.Items;
using Silkipelago.Logging;
using Silkipelago.Settings;
using Silkipelago.Utils;
using System.IO;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class ArchipelagoPlugin : BaseUnityPlugin
    {

        public static ArchipelagoPlugin Instance;

        private static ILogger _logger;
        private static ConfigEntry<KeyCode>? _addMoneyKey;
        private static PatchInitializer _patcherInitializer;
        private static Harmony _harmony;
        private static SilksongArchipelagoClient _archipelago;
        private static SilksongLocationChecker _locationChecker;
        private static SilksongItemManager _itemManager;
        private static SaveSettingsData _saveSettingsData;



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

            InitializeBeforeConnection();
            //refresh settings so that patch will apply from json
            UnityConverterInitializer.RefreshSettingsFromConfig();

            _logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void InitializeBeforeConnection()
        {
            SaveSettings.initialize(_logger);
            _patcherInitializer = new PatchInitializer();
            _patcherInitializer.InitializeEarlyPatches(_logger, _harmony);
            SilksongArchipelagoClient.Instance = new SilksongArchipelagoClient(_logger, OnItemReceived);
            _archipelago = SilksongArchipelagoClient.Instance;
            SilksongLocationChecker.Instance = new SilksongLocationChecker(_logger, _archipelago, []);
            _locationChecker = SilksongLocationChecker.Instance;
            SilksongItemManager.Instance = new SilksongItemManager(_logger, _archipelago, []);
            _itemManager = SilksongItemManager.Instance;
            _patcherInitializer.InitializeEarlyPatchesWithArchipelagoData(_logger, _harmony, _archipelago, _locationChecker);
        }

        private void OnItemReceived(ReceivedItemsHelper receivedItemsHelper)
        {
            if (_archipelago == null || _itemManager == null || !_archipelago.IsConnected || !GameManager.instance.IsGameplayScene())
            {
                return;
            }

            _itemManager.ReceiveAllNewItems();
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
                var location = "Cradle_03";
                var entry = "left1";
                var gateLocation = GatePosition.left;
                Logger.LogInfo("about to teleport");
                SceneLoader.LoadScene(location, entry, gateLocation);
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                _logger.LogInfo("Show UI button");
                ArchipelagoMenuUI.Toggle();
                return;
            }
            return;
        }
    }
}
