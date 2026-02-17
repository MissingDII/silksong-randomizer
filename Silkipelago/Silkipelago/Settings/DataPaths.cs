namespace Silkipelago.Settings
{
    public static class DataPaths
    {
        private static string ModdedDir(string subdir)
        {
            // Other platforms are not relevant for modding.
            var platform = (DesktopPlatform)Platform.Current;
            return System.IO.Path.Combine(platform.saveDirPath, "Modded", subdir);
        }

        internal static string SaveSlotDir(int saveSlot) => ModdedDir($"user{saveSlot}");


        public static string SaveDataPath(int slot) =>
            System.IO.Path.Combine(SaveSlotDir(slot), $"ArchipelagoInfo.json");
    }
}
