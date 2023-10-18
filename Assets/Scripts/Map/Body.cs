using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Diggable
{
    [HideInInspector] public PlayerController playerController;
    private UtilitiesNonStatic uns;
    public int logNumber;

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
            playerController.AddLog(logNumber);
            playerController.enabled = false;
            Destroy(gameObject);
        }
    }


}
