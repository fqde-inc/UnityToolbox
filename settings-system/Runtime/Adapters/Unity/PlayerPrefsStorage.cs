using UnityEngine;
using Fqde.SettingsSystem.Core;

namespace Fqde.SettingsSystem.UnityAdapter
{
    public class PlayerPrefsStorage : ISettingsStorage
    {
        public void Save(string key, string json)
        {
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public string Load(string key, string defaultJson)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : defaultJson;
        }
    }
}