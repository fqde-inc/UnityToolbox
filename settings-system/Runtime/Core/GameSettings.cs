using System;
using System.Collections.Generic;

namespace Fqde.SettingsSystem.Core
{
    [Serializable]
    public class GameSettings
    {
        public GraphicsSettings graphics = GraphicsSettings.Default();
        public AudioSettings audio = AudioSettings.Default();

        // Game stuff
        public string playerName = "Player";
        public string preferredLanguage = "en";

        // Metadata
        public Dictionary<string, string> metadata = new(); // free-form game-specific data

        public static GameSettings Default()
        {
            return new GameSettings
            {
                graphics = GraphicsSettings.Default(),
                audio = AudioSettings.Default(),
                playerName = "Player",
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
            sb.AppendLine($"{tab}Metadata:");
            foreach (var kvp in metadata)
                sb.AppendLine($"{tab}{tab}{kvp.Key}: {kvp.Value}");

            sb.AppendLine($"{graphics}");

            sb.AppendLine($"{audio}");

            return sb.ToString();
        }
    }
}