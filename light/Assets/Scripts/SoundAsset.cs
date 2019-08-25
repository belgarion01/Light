using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Sound", menuName = "Sound/Sound asset")]
public class SoundAsset : ScriptableObject
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(0.1f, 2f)]
    public float pitch = 1f;
    public bool loop = false;

    public float volumeVariation = 0;
    public float pitchVariation = 0;

    public void Play(AudioSource source) {
        source.clip = clip;
        source.volume = Random.Range(volume - volumeVariation, volume + volumeVariation);
        source.pitch = Random.Range(pitch - pitchVariation, pitch + pitchVariation);
        source.loop = loop;

        source.PlayOneShot(clip);
    }
}
