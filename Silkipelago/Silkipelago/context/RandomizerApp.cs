using KaitoKid.Utilities.Interfaces;

namespace Silkipelago.context
{
    public class RandomizerApp
    {
        public ILogger _logger { get; }
        public ArchipelagoContext _archipelagoContext { get; }
        public SettingsContext _settingsContext { get; }
        public UIContext _uiContext { get; }
        public RandomizerApp(ArchipelagoContext archipelagoContext, SettingsContext settings, UIContext uI, ILogger logger)
        {
            _archipelagoContext = archipelagoContext;
            _settingsContext = settings;
            _uiContext = uI;
            _logger = logger;
        }
    }
}
