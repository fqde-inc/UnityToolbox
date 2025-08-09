using System;

namespace Fqde.SettingsSystem.Core
{
    public class SettingsManager
    {
        private readonly ISettingsStorage _storage;
        private readonly ISerializer _serializer;

        public SettingsManager(ISettingsStorage storage, ISerializer serializer)
        {
            _storage = storage;
            _serializer = serializer;
        }

        private static SettingsManager _manager;
        private static ISettingsStorage _storageStatic;
        private static ISerializer _serializerStatic;

        public static void Initialize( ISettingsStorage storage = null, ISerializer serializer = null)
        {
            _storageStatic = storage ?? new PlayerPrefsStorage();
            _serializerStatic = serializer ?? new JsonUtilitySerializer();
            _manager = new SettingsManager(_storageStatic, _serializerStatic);
        }

        private static void EnsureInit()
        {
            if (_manager == null)
                Initialize();
        }

        public static T LoadSettings<T>(string key, Func<T> defaultFactory) where T : class
        {
            EnsureInit();
            var json = _storageStatic.Load(key, null);
            if (string.IsNullOrEmpty(json))
                return defaultFactory();

            return _serializerStatic.Deserialize<T>(json);
        }

        public static void SaveSettings<T>(string key, T settings) where T : class
        {
            EnsureInit();
            var json = _serializerStatic.Serialize(settings);
            _storageStatic.Save(key, json);
        }
    }
}