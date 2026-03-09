using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Items;

namespace Silkipelago.context
{
    public class RandomizerApp
    {
        public ILogger Logger { get; }
        public Harmony Harmony { get; }
        public ArchipelagoContext ArchipelagoContext { get; }
        public SettingsContext SettingsContext { get; }
        public UIContext UIContext { get; }

        // quick access for most used fields
        public SilksongArchipelagoClient ArchipelagoClient => ArchipelagoContext._archipelago;
        public SilksongLocationChecker LocationChecker => ArchipelagoContext._locationChecker;
        public SilksongItemManager ItemManager => ArchipelagoContext._itemManager;

        public RandomizerApp(ArchipelagoContext archipelagoContext, SettingsContext settings, UIContext uI, ILogger logger, Harmony harmony)
        {
            ArchipelagoContext = archipelagoContext;
            SettingsContext = settings;
            UIContext = uI;
            Logger = logger;
            Harmony = harmony;
        }
    }
}
