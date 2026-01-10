using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Extensions;
using Newtonsoft.Json;
using Silkipelago.HarmonyPatches.FsmGarbage;
using Silkipelago.HarmonyPatches.Item;
using Silkipelago.HarmonyPatches.Steam;
using Silkipelago.Logging;
using Silkipelago.Serialization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        private ILogger _logger;
        private ConfigEntry<KeyCode>? _addMoneyKey;
        //private PatchInitializer _patcherInitializer;
        private Harmony _harmony;
        //private SilksongArchipelagoClient _archipelago;
        private ArchipelagoConnectionInfo APConnectionInfo { get; set; }
        private LocationChecker _locationChecker;
        //private SilksongItemManager _itemManager;

      
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
                SteamValidationPatch.Initialize(_logger);
                PlayerDataPatch.Initialize(_logger);
                FsmPatcher.Initialize(_logger);
            }
            catch (FileNotFoundException fnfe)
            {
                _logger.LogError($"Cannot load {MyPluginInfo.PLUGIN_GUID}: A Necessary Dependency is missing [{fnfe.FileName}]");
                throw;
            }

            //InitializeBeforeConnection();
            //ConnectToArchipelago();
            //InitializeAfterConnection();

            _logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            //PlaySoundsAsync(1000).FireAndForget();
        }

        //private async Task PlaySoundsAsync(int numberOfSounds)
        //{
        //    for (var i = 0; i < numberOfSounds; i++)
        //    {
        //        await Task.Run(() => Thread.Sleep(1000));
        //        _logger.LogInfo($"Debug Thread #{i}");
        //    }
        //}

        private void InitializeBeforeConnection()
        {
            //_patcherInitializer = new PatchInitializer();
            //_archipelago = new SilksongArchipelagoClient(_logger, OnItemReceived);
        }

        private void InitializeAfterConnection()
        {
            //_locationChecker = new LocationChecker(_logger, _archipelago, new List<string>());
            //_itemManager = new SilksongItemManager(_logger, _archipelago, new List<ReceivedItem>());

            //_locationChecker.VerifyNewLocationChecksWithArchipelago();
            //_locationChecker.SendAllLocationChecks();
            //_itemManager.UpdateItemsAlreadyProcessed();
            //_patcherInitializer.InitializeAllPatches(_logger, _harmony, _archipelago, _locationChecker);
        }

        private void ConnectToArchipelago()
        {
            ReadPersistentArchipelagoData();

            var errorMessage = "";
            //if (APConnectionInfo != null && !_archipelago.IsConnected)
            //{
            //    _archipelago.Connect(APConnectionInfo, out errorMessage);
            //}

            //if (!_archipelago.IsConnected)
            //{
            //    APConnectionInfo = null;
            //    var userMessage = $"Could not connect to archipelago.{Environment.NewLine}Message: {errorMessage}{Environment.NewLine}Please verify the connection file ({Persistency.CONNECTION_FILE}) and that the server is available.{Environment.NewLine}";
            //    Logger.LogError(userMessage);
            //    const int timeUntilClose = 10;
            //    Logger.LogError($"The Game will close in {timeUntilClose} seconds");
            //    Thread.Sleep(timeUntilClose * 1000);
            //    Application.Quit();
            //    return;
            //}

            //Logger.LogMessage($"Connected to Archipelago as {_archipelago.SlotData.SlotName}.");
            //WritePersistentArchipelagoData();
            // PatcherInitializer.InitializeEarly(Logger, _archipelago);
        }

        private void ReadPersistentArchipelagoData()
        {
            if (!File.Exists(Persistency.CONNECTION_FILE))
            {
                var defaultConnectionInfo = new ArchipelagoConnectionInfo("archipelago.gg", 38281, "Name", false);
                WritePersistentData(defaultConnectionInfo, Persistency.CONNECTION_FILE);
            }

            var jsonString = File.ReadAllText(Persistency.CONNECTION_FILE);
            var connectionInfo = JsonConvert.DeserializeObject<ArchipelagoConnectionInfo>(jsonString);
            if (connectionInfo == null)
            {
                return;
            }

            APConnectionInfo = connectionInfo;
        }

        private void WritePersistentArchipelagoData()
        {
            WritePersistentData(APConnectionInfo, Persistency.CONNECTION_FILE);
        }

        private void WritePersistentData(object data, string path)
        {
            var jsonObject = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, jsonObject);
        }

        private void OnItemReceived()
        {
            //if (_archipelago == null || _itemManager == null)
            //{
            //    return;
            //}

            //_itemManager.ReceiveAllNewItems();
        }

        public void Update()
        {
            if (!Input.GetKeyDown(_addMoneyKey!.Value))
            {
                return;
            }
            Logger.LogInfo("Testing after keypress");        
                    var playerData = PlayerData.instance;
                    playerData.AddGeo(1000);
                    playerData.GetAllPowerups();
                    return;
         }
    }
}
