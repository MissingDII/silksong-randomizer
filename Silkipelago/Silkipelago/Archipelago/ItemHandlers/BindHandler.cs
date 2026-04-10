namespace Silkipelago.Archipelago.ItemHandlers
{
    public class BindHandler
    {

        public static void unlockBind()
        {
            var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
            saveData.Bind = true;
        }
    }
}
