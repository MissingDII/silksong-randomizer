using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KaitoKid.Utilities.Interfaces;

namespace Silkipelago.IdTables
{
    internal class SilksongLocationLoader : IArchipelagoLoader<ArchipelagoLocation>
    {
        private ILogger _logger;
        private IJsonLoader _jsonLoader;

        public SilksongLocationLoader(ILogger logger, IJsonLoader jsonLoader)
        {
            _logger = logger;
            _jsonLoader = jsonLoader;
        }

        public IEnumerable<ArchipelagoLocation> LoadAll(params string[] path)
        {
            var fullPath = Path.Combine(path);
            var data = _jsonLoader.DeserializeFile(fullPath);
            var locations = data["locations"];
            foreach (var keyValuePair in locations)
            {
                yield return Load(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public ArchipelagoLocation Load(string locationName, JToken locationJson)
        {
            var id = locationJson[(object)"code"].Value<long>();
            return new ArchipelagoLocation(locationName, id);
        }
    }
}
