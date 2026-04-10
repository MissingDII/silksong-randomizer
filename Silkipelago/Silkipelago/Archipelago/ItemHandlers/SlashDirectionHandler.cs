using KaitoKid.Utilities.Interfaces;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public class SlashDirectionHandler
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void unlockSlashDirection(string slashDirection)
        {
            var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
            switch (slashDirection)
            {
                case "Upslash":
                    saveData.UpSlash = true;
                    break;
                case "Downslash":
                    saveData.DownSlash = true;
                    break;
                case "Leftslash":
                    saveData.LeftSlash = true;
                    break;
                case "Rightslash":
                    saveData.RightSlash = true;
                    break;
                default:
                    Logger.LogWarning($"Unknown slash direction {slashDirection}");
                    break;
            }
        }
    }
}
