using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

namespace Fqde.SettingsSystem.Core
{
    [Serializable]
    public class GraphicsSettings
    {
        // Resolution & performance
        public int width = 1920;
        public int height = 1080;
        public bool fullscreen = true;
        public bool vSync = true;
        public int msaaSamples = 4;
        public int qualityLevel = 2;
        public float renderScale = 1f;

        // Post-processing & tone mapping
        [Range(0f, 2f)]
        public float brightness = 1f;

        [Range(0f, 2f)]
        public float contrast = 1f;

        [Range(0.1f, 5f)]
        public float gamma = 2.2f;

        [Range(-5f, 5f)]
        public float exposure = 0f; // stops offset

        public TonemappingMode tonemapping = TonemappingMode.None;

        // Metadata
        public Dictionary<string, string> metadata = new();

        public static GraphicsSettings Default() => new()
        {
            width = 1920,
            height = 1080,
            fullscreen = true,
            vSync = true,
            msaaSamples = 4,
            qualityLevel = 2,
            renderScale = 1f,
            brightness = 1f,
            contrast = 1f,
            gamma = 2.2f,
            exposure = 0f,
            tonemapping = TonemappingMode.None
        };

        public override string ToString()
        {
            var tab = "\t";
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("Graphics Settings:");
            sb.AppendLine($"{tab}Resolution: {width}x{height}");
            sb.AppendLine($"{tab}MSAA Samples: {msaaSamples}");
            sb.AppendLine($"{tab}Quality Level: {qualityLevel}");
            sb.AppendLine($"{tab}Fullscreen: {fullscreen}");
            sb.AppendLine($"{tab}VSync: {vSync}");
            sb.AppendLine($"{tab}Render Scale: {renderScale:F2}");
            sb.AppendLine($"{tab}Brightness: {brightness:F2}");
            sb.AppendLine($"{tab}Contrast: {contrast:F2}");
            sb.AppendLine($"{tab}Gamma: {gamma:F2}");
            sb.AppendLine($"{tab}Exposure: {exposure:F2}");
            sb.AppendLine($"{tab}Tonemapping: {tonemapping}");

            sb.AppendLine($"{tab}Metadata:");
            foreach (var kvp in metadata)
                sb.AppendLine($"{tab}{tab}{kvp.Key}: {kvp.Value}");

            return sb.ToString();
        }
    }

    public enum TonemappingMode
    {
        None,
        ACES,
        Neutral,
        Reinhard,
        Custom
    }

    public static class GraphicsSettingsHandler
    {
        /// <summary>
        /// Applies the given graphics settings. Optionally takes a URP Volume instance for post-processing.
        /// </summary>
        /// <param name="settings">Graphics settings to apply</param>
        /// <param name="volume">Optional URP Volume to apply post-processing changes to</param>
        /// <returns>True if applied successfully</returns>
        public static bool Apply(GraphicsSettings settings, Volume volume = null)
        {
            if (settings == null)
                return false;

            // System-level
            Screen.SetResolution(settings.width, settings.height, settings.fullscreen);
            Screen.SetMSAASamples(settings.msaaSamples);

            QualitySettings.SetQualityLevel(settings.qualityLevel);
            QualitySettings.vSyncCount = settings.vSync ? 1 : 0;

            if (volume?.profile != null)
            {
                if (volume.profile.TryGet<UnityEngine.Rendering.Universal.ColorAdjustments>(out var colorAdjustments))
                {
                    colorAdjustments.postExposure.value = settings.exposure;
                    colorAdjustments.contrast.value = (settings.contrast - 1f) * 100f;
                    colorAdjustments.colorFilter.value = Color.white * settings.brightness;
                }

                if (volume.profile.TryGet<UnityEngine.Rendering.Universal.Tonemapping>(out var tonemapping))
                {
                    tonemapping.mode.value = settings.tonemapping switch
                    {
                        TonemappingMode.ACES => UnityEngine.Rendering.Universal.TonemappingMode.ACES,
                        TonemappingMode.Neutral => UnityEngine.Rendering.Universal.TonemappingMode.Neutral,
                        _ => UnityEngine.Rendering.Universal.TonemappingMode.None
                    };
                }

                Debug.Log("Volume settings applied.");
            }

            return true;
        }
    }
}
