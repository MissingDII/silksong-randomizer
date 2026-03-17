using Silkipelago.Archipelago;
using Silkipelago.Archipelago.UI;
using Silkipelago.context;
using Silkipelago.HarmonyPatches;

namespace Silkipelago
{
    public partial class ArchipelagoPlugin
    {
        private void initializeApp()
        {
            var archipelagoContext = initializeArchipelagoContext();
            var settingsContext = initializeSettingsContext();
            var uiContext = initializeUIContext();
            SceneEventPatch.addSceneEvent();
            App = new RandomizerApp(archipelagoContext, settingsContext, uiContext, _logger, _harmony);
        }

        private ArchipelagoContext initializeArchipelagoContext()
        {
            return new ArchipelagoContext();
        }

        private SettingsContext initializeSettingsContext()
        {
            return new SettingsContext();
        }

        private UIContext initializeUIContext()
        {
            var archipelagoMenuUI = new ArchipelagoMenuUI();
            var connectionHandler = new ArchipelagoConnectionHandler();
            return new UIContext(archipelagoMenuUI, connectionHandler);

        }
    }
}
