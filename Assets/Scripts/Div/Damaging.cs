using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private Pickaxe pickaxe;
    private CameraScript cam;
    private UtilitiesNonStatic uns;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        cam = uns.playerCamera;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Diggable>() != null)
        {
            collision.GetComponent<Diggable>().TakeDamage(pickaxe.GetDamage());
            cam.Shake(0.5f);
        }
        if (collision.GetComponent<EnnemyFollow>() != null)
        {
            collision.GetComponent<EnnemyFollow>().TakeDamage(pickaxe.GetDamage());
            cam.Shake(0.5f);
        }
        gameObject.SetActive(false);
    }
}
