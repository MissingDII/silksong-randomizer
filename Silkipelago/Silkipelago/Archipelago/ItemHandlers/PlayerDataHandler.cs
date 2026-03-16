using KaitoKid.Utilities.Interfaces;
using System;

namespace Silkipelago.Archipelago.ItemHandlers
{
    public static class PlayerDataHandler
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public static void ChangeBooleanValue(string booleanName, Boolean newValue)
        {
            var instance = PlayerData.instance;
            SilksongItemManager._itemToReceive++;
            instance.SetBool(booleanName, newValue);
        }

        public static void addRosary(string rosary)
        {
            var instance = PlayerData.instance;
            int amount;
            var success = int.TryParse(rosary.Split(" ")[0], out amount);

            if (success)
            {
                instance.AddGeo(amount);
            }
            else
            {
                Logger.LogError($"Could not parse '{rosary.Split(" ")[0]}' to an integer for rosary.");
            }
        }

        public static void addShards(string shards)
        {
            var instance = PlayerData.instance;
            int amount;
            var success = int.TryParse(shards.Split(" ")[0], out amount);

            if (success)
            {
                instance.AddShards(amount);
            }
            else
            {
                Logger.LogError($"Could not parse '{shards.Split(" ")[0]}' to an integer for shards.");
            }
        }
    }
}
