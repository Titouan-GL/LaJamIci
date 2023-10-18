using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourcesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] ressourceQuantityTextInGame;
    [SerializeField] private PlayerController playerController;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ressourceQuantityTextInGame.Length; i++)
        {
            ressourceQuantityTextInGame[i].text = playerController.ore[i].ToString();
        }
    }
}
