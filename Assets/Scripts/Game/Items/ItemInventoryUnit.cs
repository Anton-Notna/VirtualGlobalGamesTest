using System;
using UnityEngine;
using Newtonsoft.Json.Linq;
using ModestTree;

namespace Game.Items
{
    [Serializable]
    public abstract class ItemInventoryUnit : IItemInventoryUnit
    {
        [SerializeField]
        private ItemType _type;

        private JObject _subData = new JObject();
        private Action<ItemInventoryUnit> _changed;

        public virtual string SubDataInfo => null;

        public int ID { get; private set; }

        public ItemType Type => _type;

        public RawItemInventoryUnit AsRaw 
        {
            get
            {
                OnBeforeSerialize();
                return new RawItemInventoryUnit(ID, Type, ClassType, _subData);
            }
        } 

        protected abstract Type ClassType { get; }

        protected ItemInventoryUnit() { }

        public static ItemInventoryUnit FromRaw(RawItemInventoryUnit raw, Action<ItemInventoryUnit> changed)
        {
            if (raw.ClassType.DerivesFrom<ItemInventoryUnit>() == false)
                throw new InvalidOperationException($"Cannot parse type {raw.ClassType}");

            var unit = (ItemInventoryUnit)Activator.CreateInstance(raw.ClassType);

            unit.ID = raw.ID == default ? UnityEngine.Random.Range(1, int.MaxValue) : raw.ID;
            unit._type = raw.Type;
            unit._subData = raw.SubData == null ? new JObject() : raw.SubData;
            unit._changed = changed;

            return unit;
        }

        protected bool ContainsSubData(string key) => _subData.ContainsKey(key);

        protected bool GetSubData<T>(string key, out T data)
        {
            if (ContainsSubData(key) == false)
            {
                data = default;
                return false;
            }

            JToken rawData = _subData[key];
            data = rawData.ToObject<T>();
            return true;
        }

        protected void SetSubData<T>(string key, T data)
        {
            _subData[key] = JToken.FromObject(data);
            _changed?.Invoke(this);
        }

        protected virtual void OnBeforeSerialize() { }
    }
}