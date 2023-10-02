using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Diggable
{
    [HideInInspector] public PlayerController playerController;
    public int logNumber;

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
