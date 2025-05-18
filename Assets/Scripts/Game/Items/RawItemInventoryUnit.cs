using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Game.Items
{
    public struct RawItemInventoryUnit
    {
        public RawItemInventoryUnit(int id, ItemType type, Type classType, JObject subData)
        {
            ID = id;
            Type = type;
            ClassType = classType;
            SubData = subData;
        }

        [JsonProperty("id")]
        public int ID { get; private set; }

        [JsonProperty("type")]
        public ItemType Type { get; private set; }

        [JsonProperty("classType")]
        public Type ClassType { get; private set; }

        [JsonProperty("subData")]
        public JObject SubData { get; private set; }
    }
}