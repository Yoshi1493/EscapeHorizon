using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffect
    {
        public AudioClip clip;

        [HideInInspector] public AudioSource source;
        [HideInInspector] public string name;
    }

    [SerializeField] PlayerSettings playerSettings;

    [Space]

    [SerializeField] Transform soundEffectParent;
    [SerializeField] AudioSource music;
    [SerializeField] SoundEffect[] soundEffects;

    void Awake()
    {
        //init.sound effects
        foreach (var sound in soundEffects)
        {
            sound.source = soundEffectParent.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.name = sound.source.clip.name;
        }
    }

    public void UpdateMusicVolume()
    {
        music.volume = playerSettings.musicVolume.Value;
    }

    public void PlaySound(string name)
    {
        var sound = System.Array.Find(soundEffects, s => s.name == name);
        if (sound == null) return;

        sound.source.volume = playerSettings.soundVolume.Value;

        if (!sound.source.isPlaying)
        {
            sound.source.Play();
        }
    }

    public void FadeMusicVolume(float targetVolume, float fadeDuration = 1f)
    {
        StartCoroutine(_FadeVolume(targetVolume, fadeDuration));
    }

    public void FadeMusicPitch(float targetPitch, float fadeDuration = 1f)
    {
        StartCoroutine(_FadePitch(targetPitch, fadeDuration));
    }

    IEnumerator _FadeVolume(float targetVolume, float fadeDuration)
    {
        float currentLerpTime = 0f;
       float startVolume = playerSettings.musicVolume.Value;

        while (music.volume != targetVolume)
        {
            float lerpProgress = currentLerpTime / fadeDuration;
            music.volume = Mathf.Lerp(startVolume, targetVolume, lerpProgress);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    IEnumerator _FadePitch(float targetPitch, float fadeDuration)
    {
        float currentLerpTime = 0f;
        float startPitch = playerSettings.soundVolume.Value;

        while (music.pitch != targetPitch)
        {
            float lerpProgress = currentLerpTime / fadeDuration;
            music.pitch = Mathf.Lerp(startPitch, targetPitch, lerpProgress);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    void Start()
    {
        FadeMusicVolume(playerSettings.musicVolume.Value, fadeDuration: 1f);
    }
}