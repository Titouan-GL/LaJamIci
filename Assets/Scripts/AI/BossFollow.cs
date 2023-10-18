using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollow : EnnemyFollow
{


    override public void TakeDamage(float amount)
    {
        life -= amount;
        if (life <= 0)
        {

            Destroy(gameObject);
        }
    }
}
