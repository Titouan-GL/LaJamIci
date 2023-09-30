using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diggable : MonoBehaviour
{
    [HideInInspector] public LevelCreator levelCreator;


    public float life = 10;

    public void TakeDamage(float amount)
    {
        life -= amount;
        if (life <= 0)
        {
            levelCreator.DestroyBlock(transform.position / 2);
            Destroy(gameObject);
        }
    }


}
