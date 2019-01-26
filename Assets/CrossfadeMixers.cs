using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CrossfadeMixers : MonoBehaviour
{
    private bool _fading;

    public void CrossfadeSnapshots(AudioMixer mixerToFadeOut, AudioMixer mixerToFadeIn, float duration)
    {
        if (!_fading)
        {
            StartCoroutine(Crossfade(mixerToFadeOut, mixerToFadeIn, duration));
        }
    }

    private IEnumerator Crossfade(AudioMixer mixer1, AudioMixer mixer2, float fadeTime)
    {
        _fading = true;
        float currentTime = 0;

        float[] mixer1Weights = { 1, 0.0001f };
        float[] mixer2Weights = { 0.0001f, 1 };

        AudioMixerSnapshot[] mixer1Shapshots = { mixer1.FindSnapshot("On"), mixer1.FindSnapshot("Off") };
        AudioMixerSnapshot[] mixer2Snapshots = { mixer2.FindSnapshot("On"), mixer2.FindSnapshot("Off") };

        while (currentTime <= fadeTime)
        {
            currentTime += Time.deltaTime;

            mixer1Weights[0] = Mathf.Log10(Mathf.Lerp(1, 0.0001f, currentTime / fadeTime)) * 20;
            mixer1Weights[1] = mixer1Weights[0] / -80;
            mixer1Weights[0] = 1 - (mixer1Weights[0] / -80);

            mixer2Weights[0] = Mathf.Log10(Mathf.Lerp(0.0001f, 1, currentTime / fadeTime)) * 20;
            mixer2Weights[1] = mixer2Weights[0] / -80;
            mixer2Weights[0] = 1 - (mixer2Weights[0] / -80);

            mixer1.TransitionToSnapshots(mixer1Shapshots, mixer1Weights, Time.deltaTime);
            mixer2.TransitionToSnapshots(mixer2Snapshots, mixer2Weights, Time.deltaTime);

            yield return null;
        }

        _fading = false;
    }
}
