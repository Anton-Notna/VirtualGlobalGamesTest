using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Items
{
    public struct RawItemsInventory
    {
        [JsonProperty("items")]
        private readonly List<RawItemInventoryUnit> _items;

        public RawItemsInventory(int? selectedId, IEnumerable<RawItemInventoryUnit> items)
        {
            SelectedId = selectedId;
            _items = new List<RawItemInventoryUnit>(items);
        }

        [JsonProperty("selectedId")]
        public int? SelectedId { get; private set; }

        [JsonIgnore]
        public IReadOnlyList<RawItemInventoryUnit> Items => _items;
    }
}