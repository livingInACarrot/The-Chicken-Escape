using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource birdsSource;
    [SerializeField] AudioSource soundsSource;

    public AudioClip mainMenuMusic;
    public AudioClip backgroundMusic;
    public AudioClip buttonClick;
    public AudioClip birdsSing;
    public AudioClip cicadas;
    public AudioClip lose;
    public AudioClip win;
    public AudioClip winApplause;
    public AudioClip egg;
    public AudioClip digging;
    public AudioClip fenceCreak;
    public AudioClip walkGrass;
    public AudioClip walkGround;
    public AudioClip walkWood;

    private float musicVolume;

    private void Start()
    {
        musicVolume = musicSource.volume;
        //DontDestroyOnLoad(gameObject);
        musicSource.clip = backgroundMusic;
        musicSource.Play();
        birdsSource.clip = birdsSing;
        birdsSource.Play();
    }
    private void Update()
    {
        SwitchTime();
    }
    void SwitchTime()
    {
        switch (TimerClock.Hours())
        {
            case 4:
                PlayBirds(birdsSing);
                break;
            case 7:
                StartCoroutine(RaiseAudioSource(musicSource, TimerClock.dayLength * 60 / 24 - 1, musicVolume));
                break;
            case 22:
                PlayBirds(cicadas);
                StartCoroutine(FadeOutAudioSource(musicSource, TimerClock.dayLength * 60 / 24 - 2));
                break;
        }
    }
    IEnumerator FadeOutAudioSource(AudioSource audioSource, float duration)
    {
        float currentTime = 0;
        float volume = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volume, 0, currentTime / duration);
            yield return null;
        }
    }
    IEnumerator RaiseAudioSource(AudioSource audioSource, float duration, float resultVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, resultVolume, currentTime / duration);
            yield return null;
        }
    }
    public void PlaySound(AudioClip sound)
    {
        soundsSource.PlayOneShot(sound);
    }
    public void PlayMusic(AudioClip sound)
    {
        musicSource.Stop();
        musicSource.clip = sound;
        musicSource.Play();
    }
    public void PlayBirds(AudioClip sound)
    {
        birdsSource.Stop();
        birdsSource.clip = sound;
        birdsSource.Play();
    }
    public void StopAllMusic()
    {
        musicSource.Stop();
        soundsSource.Stop();
        birdsSource.Stop();
    }
}
