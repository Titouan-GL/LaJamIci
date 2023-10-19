using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    public AudioSource[] audioSource;
    private UtilitiesNonStatic uns;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
    }

    private void Update()
    {
        foreach (AudioSource source in audioSource)
        {
            source.volume = uns.soundVolume;
        }
    }
}
