using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    public float damage = 3;
    public Break casser;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Diggable>() != null)
        {
            collision.GetComponent<Diggable>().TakeDamage(damage);
            casser.PlayBreakSound();
        }
        gameObject.SetActive(false);
    }
}
