using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip break_blockAudioClip;
    public void PlayBreakSound()
    {
        Debug.Log("couille");
        audioSource.PlayOneShot(break_blockAudioClip);
    }
}
