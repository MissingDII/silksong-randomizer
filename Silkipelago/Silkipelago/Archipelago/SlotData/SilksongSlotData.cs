using KaitoKid.ArchipelagoUtilities.Net.Interfaces;
using KaitoKid.Utilities.Interfaces;
using System;
using System.Collections.Generic;

namespace Silkipelago.Archipelago.SlotData
{
    public class SilksongSlotData : ISlotData
    {
        private const string GOAL_KEY = "goal";
        private const string DEATH_LINK_KEY = "death_link";
        private const string SEED_KEY = "seed";
        private const string MULTIWORLD_VERSION_KEY = "multiworld_version";
        private const string SLASH_RANDOMIZED = "starting_slashes";
        private const string STARTING_CREST_RANDOMIZED = "random_starting_crests";
        private const string STARTING_BIND = "starting_bind";
        private const string COMBAT_ABILITIES_RANDOMIZED = "randomize_combat_abilities";
        private const string FLEAS_RANDOMIZED = "randomize_lost_fleas";

        private Dictionary<string, object> _slotDataFields;
        private ILogger _logger;

        public string SlotName { get; private set; }
        public Goal Goal { get; private set; }
        public bool DeathLink { get; private set; }
        public int Seed { get; private set; }
        public string MultiworldVersion { get; private set; }

        public RandomizeSlash SlashRandomized { get; private set; }
        public bool StartingBind { get; private set; }
        public bool StartingCrestRandomized { get; private set; }

        public bool CombatAbilitiesRandomized { get; private set; }

        public bool FleasRandomized { get; private set; }


        public SilksongSlotData(string slotName, Dictionary<string, object> slotDataFields, ILogger logger)
        {
            SlotName = slotName;
            _slotDataFields = slotDataFields;
            _logger = logger;

            Goal = GetSlotSetting(GOAL_KEY, Goal.GrandMotherSilk);
            DeathLink = GetSlotSetting(DEATH_LINK_KEY, false);
            Seed = GetSlotSetting(SEED_KEY, 0);
            SlashRandomized = GetSlotSetting(SLASH_RANDOMIZED, RandomizeSlash.All);
            StartingBind = GetSlotSetting(STARTING_BIND, true);
            StartingCrestRandomized = GetSlotSetting(STARTING_CREST_RANDOMIZED, true);
            CombatAbilitiesRandomized = GetSlotSetting(COMBAT_ABILITIES_RANDOMIZED, false);
            FleasRandomized = GetSlotSetting(FLEAS_RANDOMIZED, false);
            MultiworldVersion = GetSlotSetting(MULTIWORLD_VERSION_KEY, MyPluginInfo.PLUGIN_VERSION);
        }

        private int GetSlotSetting(IEnumerable<string> keys, int defaultValue)
        {
            foreach (var key in keys)
            {
                var value = GetSlotSetting(key, defaultValue);
                if (value != defaultValue)
                {
                    return value;
                }
            }

            return defaultValue;
        }

        private T GetSlotSetting<T>(string key, T defaultValue) where T : struct, Enum, IConvertible
        {
            return _slotDataFields.ContainsKey(key) ? (T)Enum.Parse(typeof(T), _slotDataFields[key].ToString(), true) : GetSlotDefaultValue(key, defaultValue);
        }

        private string GetSlotSetting(string key, string defaultValue)
        {
            return _slotDataFields.ContainsKey(key) ? _slotDataFields[key].ToString() : GetSlotDefaultValue(key, defaultValue);
        }

        private int GetSlotSetting(string key, int defaultValue)
        {
            return _slotDataFields.ContainsKey(key) ? (int)(long)_slotDataFields[key] : GetSlotDefaultValue(key, defaultValue);
        }

        private bool GetSlotSetting(string key, bool defaultValue)
        {
            if (_slotDataFields.ContainsKey(key) && _slotDataFields[key] != null)
            {
                if (_slotDataFields[key] is bool boolValue)
                {
                    return boolValue;
                }

                if (_slotDataFields[key] is long longValue)
                {
                    return longValue != 0;
                }

                if (_slotDataFields[key] is int intValue)
                {
                    return intValue != 0;
                }
            }

            return GetSlotDefaultValue(key, defaultValue);
        }

        private T GetSlotDefaultValue<T>(string key, T defaultValue)
        {
            _logger.LogWarning($"SlotData did not contain expected key: \"{key}\"");
            return defaultValue;
        }
    }
}
