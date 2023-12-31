using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private PlayerController player;
    private UtilitiesNonStatic uns;
    [SerializeField] RectTransform barCurrent;
    [SerializeField] RectTransform barEmpty;
    float maxwidth;
    float baseXpos;

    void Awake(){
        uns = UtilitiesStatic.GetUNS();
        player = uns.player;
        maxwidth = barEmpty.sizeDelta.x;
        baseXpos = barCurrent.anchoredPosition.x;
    }

    void Update(){
        barCurrent.sizeDelta = new Vector2(maxwidth * player.GetLifeFraction(), barEmpty.sizeDelta.y);
        barCurrent.anchoredPosition = new Vector2(baseXpos - ((barEmpty.sizeDelta.x - barCurrent.sizeDelta.x)/2), barCurrent.anchoredPosition.y);
    }
    
}
