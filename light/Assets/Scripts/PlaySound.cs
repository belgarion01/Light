using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource source;
    public SoundAsset audio;

    public virtual void Play() {
        source.clip = audio.clip;
        source.volume = audio.volume;
        source.pitch = audio.pitch;
        source.PlayOneShot(audio.clip);
    }
}
