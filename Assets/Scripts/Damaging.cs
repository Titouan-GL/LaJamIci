using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private Pickaxe pickaxe;
    [SerializeField] private CameraScript cam;
    [SerializeField] private Break casser;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Diggable>() != null)
        {
            collision.GetComponent<Diggable>().TakeDamage(pickaxe.GetDamage());
            casser.PlayBreakSound();
            cam.Shake();
        }
        gameObject.SetActive(false);
    }
}
