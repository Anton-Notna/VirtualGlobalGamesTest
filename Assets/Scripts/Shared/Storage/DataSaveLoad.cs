using System;
using System.Collections.Generic;

namespace Shared.Storage
{
    public class DataSaveLoad
    {
        private readonly IStorage _storage;
        private readonly Dictionary<string, IUnit> _units = new Dictionary<string, IUnit>();
        private readonly Dictionary<Type, object> _defaults = new Dictionary<Type, object>();

        public DataSaveLoad(IStorage storage)
        {
            _storage = storage;
        }

        public void Register<T>(string key, ISerializableObject<T> serializableObject)
        {
            if (_units.ContainsKey(key))
                throw new Exception($"Already contains {key} key");

            _units.Add(key, new Unit<T>(key, serializableObject, _storage, FindDefaultSource<T>));
        }

        public void UnregisterAll()
        {
            _units.Clear();
        }

        public void Unregister(string key) => _units.Remove(key);

        public void AddDefaultValue<T>(Func<T> source) => _defaults.Add(typeof(T), source);

        public void RemoveDefaultValue<T>() => _defaults.Remove(typeof(T));

        public void RemoveAllDefaultValues() => _defaults.Clear();

        public void SaveAll()
        {
            foreach (var unit in _units.Values)
                unit.Save();
        }

        public void LoadAll()
        {
            foreach (var unit in _units.Values)
                unit.Load();
        }

        private Func<T> FindDefaultSource<T>()
        {
            if (_defaults.TryGetValue(typeof(T), out var source))
                return source as Func<T>;

            return null;
        }

        private class Unit<T> : IUnit
        {
            private readonly string _key;
            private readonly ISerializableObject<T> _serializableObject;
            private readonly IStorage _storage;
            private readonly Func<Func<T>> _default;

            public Unit(string key, ISerializableObject<T> serializableObject, IStorage storage, Func<Func<T>> @default)
            {
                _key = key;
                _serializableObject = serializableObject;
                _storage = storage;
                _default = @default;
            }

            public void Load()
            {
                if (_storage.Load(_key, out T data) == false)
                {
                    var source = _default.Invoke();
                    if (source == null)
                        throw new NullReferenceException($"There is no save and default value for key:{_key}, type: {typeof(T)}");

                    data = source.Invoke();
                }

                _serializableObject.SetData(data);
            }

            public void Save() => _storage.Save(_key, _serializableObject.GetData());
        }

        private interface IUnit
        {
            public void Save();

            public void Load();
        }
    }
}