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
    private bool _isCalm;

    void Start()
    {
        _quarterNote = 60 / bpm;
        _transitionToStress = _quarterNote * 2;
        _transitionToCalm = _quarterNote * 8;
        _isCalm = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GoStress();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GoCalm();
        }
    }

    void GoStress()
    {
        if (_isCalm)
        {
            crossfader.CrossfadeSnapshots(calm, stress, _transitionToStress);
            stingSource.Play();
            _isCalm = false;
        }
    }

    void GoCalm()
    {
        if (!_isCalm)
        {
            crossfader.CrossfadeSnapshots(stress, calm, _transitionToCalm);
            _isCalm = true;
        }
    }
}
