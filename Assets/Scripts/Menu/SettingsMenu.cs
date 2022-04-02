using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [Header("Settings elements")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    protected override void Awake()
    {
        base.Awake();
        InitSettings();
    }

    void InitSettings()
    {
        musicSlider.value = userSettings.musicVolume * 10f;
        soundSlider.value = userSettings.soundVolume * 10f;
    }

    public void OnChangeMusicVolume(TextMeshProUGUI tmp)
    {
        userSettings.musicVolume = musicSlider.value * 0.1f;
        tmp.text = musicSlider.value.ToString();
    }

    public void OnChangeSoundVolume(TextMeshProUGUI tmp)
    {
        userSettings.soundVolume = soundSlider.value * 0.1f;
        tmp.text = soundSlider.value.ToString();
    }
}