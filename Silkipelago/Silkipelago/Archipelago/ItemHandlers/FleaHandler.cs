namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class FleaHandler
    {

        public static void AddFlea()
        {
            var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
            if (saveData != null)
            {
                saveData.SavedFleas++;
            }
            else
            {
                saveData.SavedFleas = 0;
                saveData.SavedFleas++;
            }
        }
    }
}
