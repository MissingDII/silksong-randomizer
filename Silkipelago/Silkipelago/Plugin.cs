using Archipelago.MultiClient.Net.Helpers;
using BepInEx;
using BepInEx.Configuration;
using GlobalEnums;
using HarmonyLib;
using Silkipelago.Archipelago;
using Silkipelago.HarmonyPatches;
using Silkipelago.Logging;
using Silkipelago.Utils;
using System.IO;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        private static ILogger _logger;
        private static ConfigEntry<KeyCode>? _addMoneyKey;
        private static PatchInitializer _patcherInitializer;
        private static Harmony _harmony;
        //private static SilksongArchipelagoClient _archipelago;
        //private static LocationChecker _locationChecker;
        //private static SilksongItemManager _itemManager;


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

            _logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }




        private void InitializeBeforeConnection()
        {
            _patcherInitializer = new PatchInitializer();
            _patcherInitializer.InitializeEarlyPatches(_logger, _harmony);
            //SilksongArchipelagoClient.Instance = new SilksongArchipelagoClient(_logger, OnItemReceived);
            //_archipelago = SilksongArchipelagoClient.Instance;
            //SilksongLocationChecker.Instance = new SilksongLocationChecker(_logger, _archipelago, []);
            //_locationChecker = SilksongLocationChecker.Instance;
            //SilksongItemManager.Instance = new SilksongItemManager(_logger, _archipelago, []);
            //_itemManager = SilksongItemManager.Instance;
        }

        private void OnItemReceived(ReceivedItemsHelper receivedItemsHelper)
        {
            //if (_archipelago == null || _itemManager == null || !_archipelago.IsConnected)
            //{
            //    return;
            //}

            // _itemManager.ReceiveAllNewItems();
        }

        public void Update()
        {

            if (Input.GetKeyDown(_addMoneyKey!.Value))
            {
                Logger.LogInfo("Enable PowerUp");
                var playerData = PlayerData.instance;
                playerData.GetAllPowerups();
                Logger.LogInfo("here with playerInstance");
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Logger.LogInfo("Disable PowerUp");
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
                Logger.LogInfo("Teleport somewhere");
                var playerData = PlayerData.instance;
                SceneLoader.LoadScene("Bone_East_04b", "top1", GatePosition.top);
                Logger.LogInfo("here with playerInstance");
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                Logger.LogInfo("Show UI button");
                ArchipelagoMenuUI.Init(Logger);
                ArchipelagoMenuUI.Toggle();
                return;
            }
            return;
        }
    }
}
