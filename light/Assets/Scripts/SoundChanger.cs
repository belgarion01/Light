using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour
{
    public AudioSource source;
    public SoundAsset sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!IsSame(source.clip, sound))
        {
            SetClip(sound);
            source.Play();
        }
    }

    void SetClip(SoundAsset s)
    {
        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
    }

    bool IsSame(AudioClip clip, SoundAsset asset) {
        if (clip == asset.clip) return true;
        else return false;
    }
}
