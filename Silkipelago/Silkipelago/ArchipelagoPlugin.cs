using BepInEx;
using GlobalEnums;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using Silkipelago.context;
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
        private ILogger _logger;
        private Harmony _harmony;
        public static RandomizerApp App { get; private set; }



        private void Awake()
        {

            // Plugin startup logic
            Logger.LogInfo($"Loading {MyPluginInfo.PLUGIN_GUID}...");
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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                var questName = "Courier Delivery Bonebottom";
                _logger.LogInfo("trying to add quest");
                var fullQuestBase = QuestManager.GetQuest(questName);
                var completion = fullQuestBase.Completion;
                if (!completion.IsAccepted)
                {
                    completion.IsAccepted = true;
                    completion.HasBeenSeen = true;
                }
                fullQuestBase.Completion = completion;
                var rewardItem = fullQuestBase.rewardItem;
                rewardItem.Get();
                var test = rewardItem.GetSavedAmount();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                var questName = "Courier Delivery Bonebottom";
                _logger.LogInfo("trying to add quest");
                var fullQuestBase = QuestManager.GetQuest(questName);
                return;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                _logger.LogInfo("Teleport somewhere");
                var playerData = PlayerData.instance;
                var location = "Bellshrine_02";
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
                var items = ToolItemManager.GetAllTools()
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
