using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Interfaces;
using KaitoKid.Utilities.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Silkipelago.IdTables
{
    internal class SilksongLocationLoader : IArchipelagoLoader<ArchipelagoLocation>
    {
        private ILogger Logger => ArchipelagoPlugin.App.Logger;
        private IJsonLoader _jsonLoader;

        public SilksongLocationLoader(IJsonLoader jsonLoader)
        {
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
            var id = locationJson["code"].Value<long>();
            return new ArchipelagoLocation(locationName, id);
        }
    }
}
