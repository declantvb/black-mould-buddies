using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public CrossfadeMixers crossfader;
    public AudioMixer calm;
    public AudioMixer stress;
    public AudioSource stingSource;

    public float bpm = 130;

    private float _transitionToStress;
    private float _transitionToCalm;
    private float _quarterNote;

    void Start()
    {
        _quarterNote = 60 / bpm;
        _transitionToStress = _quarterNote * 2;
        _transitionToCalm = _quarterNote * 8;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            GoStress();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            GoCalm();
        }
    }

    void GoStress()
    {
        crossfader.CrossfadeSnapshots(calm, stress, _transitionToStress);
        stingSource.Play();
    }

    void GoCalm()
    {
        crossfader.CrossfadeSnapshots(stress, calm, _transitionToCalm);
    }
}
