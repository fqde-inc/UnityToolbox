using System;

namespace Fqde.SettingsSystem.Core
{
    public class SettingsManager
    {
        private readonly ISettingsStorage _storage;
        private readonly ISerializer _serializer;

        public event Action<string> OnSettingChanged; // key

        public SettingsManager(ISettingsStorage storage, ISerializer serializer)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public T LoadSettings<T>(string key, Func<T> defaultFactory)
        {
            var json = _storage.Load(key, null);
            if (string.IsNullOrEmpty(json))
                return defaultFactory();

            return _serializer.Deserialize<T>(json);
        }

        public void SaveSettings<T>(string key, T settings)
        {
            var json = _serializer.Serialize(settings);
            _storage.Save(key, json);
            OnSettingChanged?.Invoke(key);
        }
    }
}