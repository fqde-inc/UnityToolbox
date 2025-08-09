#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Fqde.SettingsSystem.Core;

using GraphicsSettings = Fqde.SettingsSystem.Core.GraphicsSettings;

public class SettingsWindow : EditorWindow
{
    private GameSettings _settings;

    [MenuItem("Window/Settings/System Settings")]
    public static void Open() => GetWindow<SettingsWindow>("System Settings");

    private void OnEnable()
    {
        SettingsManager.Initialize(new PlayerPrefsStorage(), new JsonUtilitySerializer());
        _settings = SettingsManager.LoadSettings("Default", GameSettings.Default);
    }

    private void OnGUI()
    {
        if (_settings == null)
            _settings = GameSettings.Default();

        EditorGUI.BeginChangeCheck();

        _settings.graphics.width = EditorGUILayout.IntField("Width", _settings.graphics.width);
        _settings.graphics.height = EditorGUILayout.IntField("Height", _settings.graphics.height);
        _settings.graphics.fullscreen = EditorGUILayout.Toggle("Fullscreen", _settings.graphics.fullscreen);
        _settings.graphics.qualityLevel = EditorGUILayout.IntSlider("Quality Level", _settings.graphics.qualityLevel, 0, QualitySettings.names.Length - 1);
        _settings.graphics.renderScale = EditorGUILayout.Slider("Render Scale", _settings.graphics.renderScale, 0.1f, 2f);

        if (EditorGUI.EndChangeCheck())
        {
            SettingsManager.SaveSettings("graphics", _settings);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Apply Settings (Play Mode Only)"))
        {
            if (Application.isPlaying)
            {
                Debug.Log("Game settings applied.");
            }
            else
            {
                Debug.LogWarning("Can only apply graphics settings during Play Mode.");
            }
        }
    }
}
#endif