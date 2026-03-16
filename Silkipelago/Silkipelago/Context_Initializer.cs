using Silkipelago.Archipelago;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Archipelago.UI;
using Silkipelago.context;
using Silkipelago.HarmonyPatches;

namespace Silkipelago
{
    public partial class ArchipelagoPlugin
    {
        private void initializeApp()
        {
            _patcherInitializer = new PatchInitializer();
            _patcherInitializer.InitializeEarlyPatches(_logger, _harmony);
            var archipelagoContext = initializeArchipelagoContext();
            var settingsContext = initializeSettingsContext();
            var uiContext = initializeUIContext();
            _patcherInitializer.InitializeEarlyPatchesWithArchipelagoData(_logger, _harmony, archipelagoContext._archipelago, archipelagoContext._locationChecker);
            SceneEventPatch.Initialize(_logger);
            SceneEventPatch.addSceneEvent();
            App = new RandomizerApp(archipelagoContext, settingsContext, uiContext, _logger, _harmony);
        }

        private ArchipelagoContext initializeArchipelagoContext()
        {
            var archipelagoClient = new SilksongArchipelagoClient(_logger, OnItemReceived);
            var locationChecker = new SilksongLocationChecker(_logger, archipelagoClient, []);
            var itemManager = new SilksongItemManager(_logger, archipelagoClient, []);
            return new ArchipelagoContext(archipelagoClient, locationChecker, itemManager);
        }

        private SettingsContext initializeSettingsContext()
        {
            return new SettingsContext();
        }

        private UIContext initializeUIContext()
        {
            var archipelagoMenuUI = new ArchipelagoMenuUI(_logger);
            var connectionHandler = new ArchipelagoConnectionHandler(_logger);
            return new UIContext(archipelagoMenuUI, connectionHandler);

        }
    }
}
