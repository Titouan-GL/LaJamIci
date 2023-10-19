using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightIntensity : MonoBehaviour
{
    public Light spotLight;
    private UtilitiesNonStatic uns;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
    }

    private void Update()
    {
        spotLight.intensity = uns.lightIntensity;
       
    }
}
