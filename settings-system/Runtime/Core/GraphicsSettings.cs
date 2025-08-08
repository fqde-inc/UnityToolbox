using System;

namespace Fqde.SettingsSystem.Core
{
    [Serializable]
    public class GraphicsSettings
    {
        public int width = 1920;
        public int height = 1080;
        public bool fullscreen = true;
        public int qualityLevel = 2; // map to Unity quality levels
        public float renderScale = 1f; // SRP-specific

        public static GraphicsSettings Default() => new GraphicsSettings {
            width = 1920, height = 1080, fullscreen = true, qualityLevel = 2, renderScale = 1f
        };
    }
}