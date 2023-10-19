using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilitiesNonStatic : MonoBehaviour
{
    public PlayerController player;
    public CameraScript playerCamera;
    public LevelCreator levelCreator;
    public LayerMask levelLayerMask;

    public float soundVolume = 0.5f;
    public float musicVolume = 0.5f;
    public float shakeIntensity = 0.5f;
    public float lightIntensity = 0.5f;

    public Slider soundSlider;
    public Slider musicSlider;
    public Slider shakeSlider;
    public Slider lightSlider;

    public AudioSource playerAudioSource;

    public void Awake()
    {
        Application.targetFrameRate = 60;

        if (PlayerPrefs.HasKey("soundVolume")) soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        if (PlayerPrefs.HasKey("musicVolume")) musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.HasKey("shakeIntensity")) shakeSlider.value = PlayerPrefs.GetFloat("shakeIntensity");
        if (PlayerPrefs.HasKey("lightIntensity")) lightSlider.value = PlayerPrefs.GetFloat("lightIntensity");
        SetSoundVolume();
        SetMusicVolume();
        SetShakeIntensity();
        SetLightIntensity();
    }

    public void SetSoundVolume()
    {
        soundVolume = soundSlider.value;
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
    }
    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }
    public void SetShakeIntensity()
    {
        shakeIntensity = shakeSlider.value;
        PlayerPrefs.SetFloat("shakeIntensity", shakeIntensity);
    }
    public void SetLightIntensity()
    {
        lightIntensity = lightSlider.value;
        PlayerPrefs.SetFloat("lightIntensity", lightIntensity);
    }
    public void ResetSliders()
    {
        soundSlider.value = 0.5f;
        musicSlider.value = 0.5f;
        shakeSlider.value = 0.5f;
        lightSlider.value = 0.5f;
        SetSoundVolume();
        SetMusicVolume();
        SetShakeIntensity();
        SetLightIntensity();
    }
}
