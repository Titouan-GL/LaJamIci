using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMovement : MonoBehaviour
{
    public AudioSource movementSound;

    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            movementSound.volume = Mathf.Lerp(movementSound.volume, 1, 0.2f);
        }
        else
        {
            movementSound.volume = Mathf.Lerp(movementSound.volume, 0, 0.2f);
        }
    }
}