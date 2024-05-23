using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlayAudio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void MuteAudio()
    {
        audioSource.mute = true;
    }

    public void UnmuteAudio()
    {
        audioSource.mute = false;
    }
}
