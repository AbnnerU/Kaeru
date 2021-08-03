using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private bool playAudioSourceClip=true;

    private bool playing=false;

    public void PlayAudioClip()
    {
        if (playAudioSourceClip == false)
        {
            if (audioSource != null)
                audioSource.clip = audioClip;
        }

        if(audioSource!=null)
        audioSource.Play();
    }

    public void PlayAudioClip(bool playAudio)
    {
        if (playAudio)
        {
            if (playAudioSourceClip == false)
            {
                if (audioSource != null)
                    audioSource.clip = audioClip;
            }

            if (playing == false)
            {
                if (audioSource != null)
                {
                    playing = true;
                    audioSource.Play();
                }
            }
        }
        else
        {
            if (playing == true)
            {
                if (audioSource != null)
                {
                    playing = false;
                    audioSource.Stop();
                }
            }
        }
    }
}
