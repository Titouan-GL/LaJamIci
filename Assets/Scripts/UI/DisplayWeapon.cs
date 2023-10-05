using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWeapon : MonoBehaviour
{
    UtilitiesNonStatic uns;
    PlayerController player;
    [SerializeField] Rifle rifle;

    [SerializeField] GameObject[] tool1;
    [SerializeField] GameObject[] tool2;
    [SerializeField] GameObject[] tool3;

    void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        player = uns.player;
    }

    // Update is called once per frame
    void Update()
    {
        //reinitialize
        foreach (GameObject go in tool1)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in tool2)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in tool3)
        {
            go.SetActive(false);
        }

        if (rifle.level == 0)
        {
            if (player.currentWeaponIndex == 0)
            {
                tool1[0].SetActive(true);
                tool2[2].SetActive(true);
            }
            if (player.currentWeaponIndex == 2)
            {
                tool1[2].SetActive(true);
                tool2[0].SetActive(true);
            }
        }
        else
        {
            if (player.currentWeaponIndex == 0)
            {
                tool1[0].SetActive(true);
                tool2[1].SetActive(true);
                tool3[2].SetActive(true);
            }
            if (player.currentWeaponIndex == 1)
            {
                tool1[1].SetActive(true);
                tool2[2].SetActive(true);
                tool3[0].SetActive(true);
            }
            if (player.currentWeaponIndex == 2)
            {
                tool1[2].SetActive(true);
                tool2[0].SetActive(true);
                tool3[1].SetActive(true);
            }
        }
    }
}
