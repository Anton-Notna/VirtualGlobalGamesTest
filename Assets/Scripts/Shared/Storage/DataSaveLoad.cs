using System;
using System.Collections.Generic;

namespace Shared.Storage
{
    public class DataSaveLoad
    {
        private readonly IStorage _storage;
        private readonly Dictionary<string, IUnit> _units = new Dictionary<string, IUnit>();

        public DataSaveLoad(IStorage storage)
        {
            _storage = storage;
        }

        public void Register<T>(string key, ISerializableObject<T> serializableObject, T defaultValue = default)
        {
            if (_units.ContainsKey(key))
                throw new Exception($"Already contains {key} key");

            _units.Add(key, new Unit<T>(key, serializableObject, _storage, defaultValue));
        }

        public void UnregisterAll()
        {
            _units.Clear();
        }

        public void Unregister(string key) => _units.Remove(key);

        public void Save()
        {
            foreach (var unit in _units.Values)
                unit.Save();
        }

        public void Load()
        {
            foreach (var unit in _units.Values)
                unit.Load();
        }

        internal void Reset()
        {
            foreach (var unit in _units.Values)
                unit.Reset();
        }

        private class Unit<T> : IUnit
        {
            private readonly string _key;
            private readonly ISerializableObject<T> _serializableObject;
            private readonly IStorage _storage;
            private readonly T _default;

            public Unit(string key, ISerializableObject<T> serializableObject, IStorage storage, T @default)
            {
                _key = key;
                _serializableObject = serializableObject;
                _storage = storage;
                _default = @default;
            }

            public void Load()
            {
                if (_storage.Load(_key, out T data) == false)
                    data = _default;

                _serializableObject.SetData(data);
            }

            public void Save() => _storage.Save(_key, _serializableObject.GetData());

            public void Reset()
            {
                _serializableObject.SetData(_default);
                _storage.Save(_key, _default);
            }
        }

        private interface IUnit
        {
            public void Save();

            public void Load();

            public void Reset();
        }
    }
}