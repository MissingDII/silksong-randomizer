namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class NPCHandler
    {

        public static void ActivateNpc(string NPCName)
        {
            if (NPCName.Equals("Progressive Tipp and Pill"))
            {
                var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                if (saveData.Tipp)
                {
                    saveData.Pill = true;
                }
                else
                {
                    saveData.Tipp = true;
                }
            }
        }
    }
}
