using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private Pickaxe pickaxe;
    [SerializeField] private CameraScript cam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Diggable>() != null)
        {
            collision.GetComponent<Diggable>().TakeDamage(pickaxe.GetDamage());
            cam.Shake();
        }
        gameObject.SetActive(false);
    }
}
