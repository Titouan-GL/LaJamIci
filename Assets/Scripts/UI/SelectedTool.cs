using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTool : MonoBehaviour
{
    [SerializeField] PlayerController pc;

    [SerializeField] private Image[] icons;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < icons.Length; i++)
        {
            if(i == pc.currentWeaponIndex)
            {
                icons[i].material.color = new Color(0.8f, 0.8f, 0.8f);
            }
            else
            {
                icons[i].material.color = new Color(1f, 1f, 1f);
            }
        }
    }
}
