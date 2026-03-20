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
            SilksongItemManager.ItemToReceive++;
            instance.SetBool(booleanName, newValue);
        }

        public static void AddToIntValue(string intName)
        {
            var instance = PlayerData.instance;
            SilksongItemManager.ItemToReceive++;
            instance.SetInt(intName, instance.GetInt(intName) + 1);
        }

        public static void AddRosary(string rosary)
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

        public static void keepChapelsOpen()
        {
            var instance = PlayerData.instance;
            instance.chapelClosed_beast = true;
            instance.chapelClosed_reaper = true;
            instance.chapelClosed_shaman = true;
            instance.chapelClosed_toolmaster = true;
            instance.chapelClosed_wanderer = true;
            instance.chapelClosed_witch = true;
        }
    }
}
