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

    [SerializeField] AudioSource[] musicTracks;
    [SerializeField] SoundEffect[] soundEffects;

    [SerializeField] Transform soundEffectParent;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;

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
        for (int i = 0; i < musicTracks.Length; i++)
        {
            musicTracks[i].volume = playerSettings.musicVolume.Value;
        }
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

        while (musicTracks[0].volume != targetVolume)
        {
            for (int i = 0; i < musicTracks.Length; i++)
            {
                float lerpProgress = currentLerpTime / fadeDuration;
                musicTracks[i].volume = Mathf.Lerp(startVolume, targetVolume, lerpProgress);
                print($"volume of {musicTracks[i].name}: {musicTracks[i].volume}");
                yield return EndOfFrame;
                currentLerpTime += Time.deltaTime;
            }
        }
    }

    IEnumerator _FadePitch(float targetPitch, float fadeDuration)
    {
        float currentLerpTime = 0f;
        float startPitch = musicTracks[0].pitch;

        while (musicTracks[0].pitch != targetPitch)
        {
            for (int i = 0; i < musicTracks.Length; i++)
            {
                float lerpProgress = currentLerpTime / fadeDuration;
                musicTracks[i].pitch = Mathf.Lerp(startPitch, targetPitch, lerpProgress);

                yield return EndOfFrame;
                currentLerpTime += Time.deltaTime;
            }
        }
    }

    void Start()
    {
        FadeMusicVolume(playerSettings.musicVolume.Value, fadeDuration: 1f);
    }
}