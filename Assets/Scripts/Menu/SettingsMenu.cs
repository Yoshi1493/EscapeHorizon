using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu
{
    [SerializeField] PlayerSettings userSettings;

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
        musicSlider.value = userSettings.musicVolume.Value * 10f;
        soundSlider.value = userSettings.soundVolume.Value * 10f;
    }

    public void OnChangeMusicVolume(TextMeshProUGUI tmp)
    {
        userSettings.musicVolume.Value = musicSlider.value * 0.1f;
        tmp.text = musicSlider.value.ToString();
    }

    public void OnChangeSoundVolume(TextMeshProUGUI tmp)
    {
        userSettings.soundVolume.Value = soundSlider.value * 0.1f;
        tmp.text = soundSlider.value.ToString();
    }
}