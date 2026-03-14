using Silkipelago.Constants;
using Silkipelago.Items;
using UnityEngine.SceneManagement;

namespace Silkipelago.HarmonyPatches
{
    public static class SceneEvent
    {
        public static void addSceneEvent()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "Coral_Judge_Arena")
                return;

            var bellcount = ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount("Grand Gate Bell");
            var i = 1;
            foreach (var shrineName in PlayerDataStrings.SHRINES)
            {
                if (i <= bellcount)
                {
                    SilksongItemManager._itemToReceive++;
                    PlayerData.instance.SetBool(shrineName, true);
                }
                else
                {
                    PlayerData.instance.SetBool(shrineName, false);
                }
                i++;
            }
        }
    }
}
