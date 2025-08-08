using UnityEngine;
using Fqde.SettingsSystem.Core;

namespace Fqde.SettingsSystem.UnityAdapter
{
    public static class GraphicsSettingsHandler
    {
        public static void Apply(GraphicsSettings s)
        {
            if (s == null) return;

            Screen.SetResolution(s.width, s.height, s.fullscreen);
            QualitySettings.SetQualityLevel(s.qualityLevel, true);

            // renderScale is SRP-specific (URP/HDRP). Handle in a separate adapter if needed.
            // Example (URP) would require referencing URP package and setting pipeline asset.
        }
    }
}