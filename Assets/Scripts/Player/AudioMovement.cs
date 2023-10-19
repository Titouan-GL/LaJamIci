using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMovement : MonoBehaviour
{
    public AudioSource movementSound;
    private UtilitiesNonStatic uns;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
    }

    void FixedUpdate()
    {
        if (uns.player.isMoving)
        {
            movementSound.volume = Mathf.Lerp(movementSound.volume, uns.soundVolume, 0.2f);
        }
        else
        {
            movementSound.volume = Mathf.Lerp(movementSound.volume, uns.soundVolume, 0.2f);
        }
    }
}