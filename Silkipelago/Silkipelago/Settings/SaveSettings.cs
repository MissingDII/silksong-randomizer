using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json;
using System;
using IO = System.IO;

namespace Silkipelago.Settings
{
    public static class SaveSettings
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public static void createSaveDataDirectory(int saveSlot)
        {
            IO.Directory.CreateDirectory(DataPaths.SaveSlotDir(saveSlot));
        }

        public static void saveGlobalSaveDataSettings(int saveSlot)
        {
            createSaveDataDirectory(saveSlot);
            var dataPath = DataPaths.SaveDataPath(saveSlot);
            saveDataSettings(dataPath, ArchipelagoPlugin.App.SettingsContext.saveSettingsData);
        }

        private static void saveDataSettings(string path, SaveSettingsData data)
        {
            try
            {


                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                IO.File.WriteAllText(path, json);


                IO.File.WriteAllText(path, json);
            }
            catch (Exception err)
            {
                Logger.LogError($"Error saving SaveSettingsData: {err}");
            }
        }

        public static SaveSettingsData LoadSaveDataSettings(int saveSlot)
        {
            try
            {
                var path = DataPaths.SaveDataPath(saveSlot);

                if (!IO.File.Exists(path))
                    return new SaveSettingsData();

                var json = IO.File.ReadAllText(path);

                return JsonConvert.DeserializeObject<SaveSettingsData>(json);

            }
            catch (Exception err)
            {
                Logger.LogError($"Error loading SaveSettingsData: {err}");
                return new SaveSettingsData();
            }
        }

        public static void ClearSaveData(int saveSlot)
        {
            try
            {
                var path = DataPaths.SaveDataPath(saveSlot);


                IO.File.Delete(path);

            }
            catch (Exception err)
            {
                Logger.LogError($"Error Deleting SaveSettingsData: {err}");
            }
        }

    }
}
