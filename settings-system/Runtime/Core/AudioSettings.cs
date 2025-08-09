using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Fqde.SettingsSystem.Core
{
    [Serializable]
    public class AudioSettings
    {
        public float masterVolume = 1.0f;
        public float musicVolume = 0.8f;
        public float sfxVolume = 0.8f;

        // Metadata
        public Dictionary<string, string> metadata = new(); // free-form game-specific data

        public static AudioSettings Default()
        {
            return new AudioSettings
            {
                masterVolume = 1.0f,
                musicVolume = 0.8f,
                sfxVolume = 0.8f,
            };
        }

        public override string ToString()
        {
            var tab = "\t";
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("AudioSettings:");
            sb.AppendLine($"{tab}Master Volume: {masterVolume}");
            sb.AppendLine($"{tab}Music Volume: {musicVolume}");
            sb.AppendLine($"{tab}SFX Volume: {sfxVolume}");
            sb.AppendLine($"{tab}Metadata:");
            foreach (var kvp in metadata)
                sb.AppendLine($"{tab}{tab}{kvp.Key}: {kvp.Value}");

            return sb.ToString();
        }
    }

    
    public static class AudioSettingsHandler
    {
        public static bool Apply(AudioSettings settings, AudioMixer mixer)
        {
            if (settings == null)
                return false;

            // AudioMixers use decibels, so convert from 0..1 volume
            mixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(settings.masterVolume, 0.0001f)) * 20f);
            mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(settings.musicVolume, 0.0001f)) * 20f);
            mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(settings.sfxVolume, 0.0001f)) * 20f);

            return true;
        }
    }
}