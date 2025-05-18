using Newtonsoft.Json;
using UnityEngine;

namespace Shared.Storage
{
    public class PlayerPrefsJsonStorage : IStorage
    {
        public bool Load<T>(string key, out T data)
        {
            string fullKey = GetKey<T>(key);

            if (PlayerPrefs.HasKey(fullKey) == false)
            {
                data = default;
                return false;
            }

            data = JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(fullKey));
            return true;
        }

        public void Save<T>(string key, T data)
        {
            string json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(GetKey<T>(key), json);
        }

        private static string GetKey<T>(string key) => $"{key}_{typeof(T).Name}";
    }
}