using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource birdsSinging;
    [SerializeField] AudioSource soundsSource;

    public AudioClip mainMenuMusic;
    public AudioClip backgroundMusic;
    public AudioClip buttonClick;
    public AudioClip birdsSing;
    public AudioClip lose;
    public AudioClip win;
    public AudioClip winApplause;
    public AudioClip egg;
    public AudioClip digging;
    public AudioClip fenceCreak;
    public AudioClip walkGrass;
    public AudioClip walkGround;
    public AudioClip walkWood;
    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
        birdsSinging.clip = birdsSing;
        birdsSinging.Play();
    }
    public void PlaySound(AudioClip sound)
    {
        soundsSource.PlayOneShot(sound);
    }
}
