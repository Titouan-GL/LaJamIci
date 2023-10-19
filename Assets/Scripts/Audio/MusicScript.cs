using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private Boss boss;

    public AudioSource audioSourceMusic;
    public AudioSource audioSourceBoss;

    public Volume volume;
    private FilmGrain grain;
    private ColorAdjustments colorAdjustments;

    private float currentBossMusic = 0;
    private float currentAmbiantMusic = 1f;

    private UtilitiesNonStatic uns;

    private void Awake()
    {
        volume.profile.TryGet(out grain);
        volume.profile.TryGet(out colorAdjustments);
        uns = UtilitiesStatic.GetUNS();
    }
    void FixedUpdate()
    {
        float lerpvalue = 0.01f;
        if (boss.active)
        {
            currentBossMusic = Mathf.Lerp(currentBossMusic, 1, lerpvalue);
            currentAmbiantMusic = Mathf.Lerp(currentAmbiantMusic, 0, lerpvalue);
            grain.intensity.value = Mathf.Lerp(grain.intensity.value, 1, lerpvalue);
            colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, 40, lerpvalue);
        }
        else
        {
            currentAmbiantMusic = Mathf.Lerp(currentAmbiantMusic, 1, lerpvalue);
            currentBossMusic = Mathf.Lerp(currentBossMusic, 0, lerpvalue);
            grain.intensity.value = Mathf.Lerp(grain.intensity.value, 0, lerpvalue);
            colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, 0, lerpvalue);
        }
    }

    private void Update()
    {
        audioSourceBoss.volume = currentBossMusic * uns.musicVolume;
        audioSourceMusic.volume = currentAmbiantMusic * uns.musicVolume;
    }
}
