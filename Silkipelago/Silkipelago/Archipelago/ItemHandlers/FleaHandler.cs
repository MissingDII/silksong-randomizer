namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class FleaHandler
    {

        public static void AddFlea(string fleaName)
        {

            var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
            saveData.SavedFleas++;
            /**
            if (fleaName.Equals("Kratt"))
            {
                PlayerData.instance.CaravanLechSaved = true;
                return;
            }
            if (fleaName.Equals("Giant Lost Flea"))
            {
                // to find
                // PlayerData.instance.SavedFlea_ = true;
                return;
            }
            if (fleaName.Equals("Vog"))
            {
                // find vog
                // PlayerData.instance. = true;
                return;
            }
            if (saveData.SavedFleas <= PlayerDataIds.BASIC_FLEAS.Count)
            {
                var flea = PlayerDataIds.BASIC_FLEAS[saveData.SavedFleas];
                SilksongItemManager.ItemToReceive++;
                PlayerData.instance.SetBool(flea, true);
                saveData.SavedFleas++;
            }
            **/
        }
    }
}
