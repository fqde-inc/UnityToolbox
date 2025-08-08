#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Fqde.SettingsSystem.Core;
using Fqde.SettingsSystem.UnityAdapter;

public class SettingsWindow : EditorWindow
{
    private SettingsManager _manager;
    private GraphicsSettings _settings;

    [MenuItem("Window/Settings/System Settings")]
    public static void Open() => GetWindow<SettingsWindow>("System Settings");

    void OnEnable()
    {
        _manager = new SettingsManager(new PlayerPrefsStorage(), new JsonUtilitySerializer());
        _settings = _manager.LoadSettings<GraphicsSettings>("graphics", GraphicsSettings.Default);
    }

    void OnGUI()
    {
        if (_settings == null) _settings = GraphicsSettings.Default();

        _settings.width = EditorGUILayout.IntField("Width", _settings.width);
        _settings.height = EditorGUILayout.IntField("Height", _settings.height);
        _settings.fullscreen = EditorGUILayout.Toggle("Fullscreen", _settings.fullscreen);
        _settings.qualityLevel = EditorGUILayout.IntField("Quality Level", _settings.qualityLevel);
        _settings.renderScale = EditorGUILayout.FloatField("Render Scale", _settings.renderScale);

        if (GUILayout.Button("Apply (Play Mode only)"))
        {
            _manager.SaveSettings("graphics", _settings);
            if (Application.isPlaying)
                UnityGraphicsApplier.Apply(_settings);
        }
    }
}
#endif