using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    public float damage = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Diggable>() != null)
        {
            collision.GetComponent<Diggable>().TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
