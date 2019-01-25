using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public static SoundManager Instance = null;

    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    private void Awake()
    {
        // Enforce singleton
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        // Persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Play sound effect clip
    public void PlayEffect(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    // Play music clip
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
