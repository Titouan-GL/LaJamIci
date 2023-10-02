using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactScript : Diggable
{
    [HideInInspector] public PlayerController playerController;
    private UtilitiesNonStatic uns;

    public void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        playerController = uns.player;
    }

    public override void TakeDamage(float amount)
    {
        life -= amount;
        if (life <= 0)
        {
            playerController.artefacts++;
            Destroy(gameObject);
        }
    }


}
