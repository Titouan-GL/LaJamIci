using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMovement : MonoBehaviour
{
    public AudioSource movementSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q))
        {
            movementSound.enabled = true;
        }
        else
        {
            movementSound.enabled = false;
        }
    }
}
