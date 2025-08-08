using System;

namespace Fqde.SettingsSystem.Core
{
    public interface ISettingsStorage
    {
        void Save(string key, string json);
        string Load(string key, string defaultJson);
    }
}