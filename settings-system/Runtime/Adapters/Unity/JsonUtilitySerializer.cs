using UnityEngine;
using Fqde.SettingsSystem.Core;

namespace Fqde.SettingsSystem.UnityAdapter
{
    public class JsonUtilitySerializer : ISerializer
    {
        public string Serialize<T>(T obj) => JsonUtility.ToJson(obj);

        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return JsonUtility.FromJson<T>(json);
        }
    }
}