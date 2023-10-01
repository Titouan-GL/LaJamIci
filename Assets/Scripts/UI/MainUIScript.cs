using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Craft"))
        {
            playerController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
