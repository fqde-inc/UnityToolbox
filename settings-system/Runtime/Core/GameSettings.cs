using System;
using System.Collections.Generic;

namespace Fqde.SettingsSystem.Core
{
    [Serializable]
    public class GameSettings
    {
        public GraphicsSettings graphics = GraphicsSettings.Default();

        // Game stuff
        public string playerName = "Player";
        public string preferredLanguage = "en";

        // Sound
        public float masterVolume = 1.0f;
        public float musicVolume = 0.8f;
        public float sfxVolume = 0.8f;

        // Metadata
        public Dictionary<string, string> metadata = new(); // free-form game-specific data

        public static GameSettings Default()
        {
            return new GameSettings
            {
                graphics = GraphicsSettings.Default(),
                playerName = "Player",
                masterVolume = 1.0f,
                musicVolume = 0.8f,
                sfxVolume = 0.8f,
                preferredLanguage = "en",
            };
        }

        public override string ToString()
        {
            var tab = "\t";
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("GameSettings:");
            sb.AppendLine($"{tab}Player Name: {playerName}");
            sb.AppendLine($"{tab}Preferred Language: {preferredLanguage}");
            sb.AppendLine($"{tab}Master Volume: {masterVolume}");
            sb.AppendLine($"{tab}Music Volume: {musicVolume}");
            sb.AppendLine($"{tab}SFX Volume: {sfxVolume}");
            sb.AppendLine($"{tab}Graphics: {graphics}");
            sb.AppendLine($"{tab}Metadata:");
            foreach (var kvp in metadata)
                sb.AppendLine($"{tab}{tab}{kvp.Key}: {kvp.Value}");

            return sb.ToString();
        }
    }
}