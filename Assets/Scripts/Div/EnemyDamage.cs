using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private CameraScript cam;
    private UtilitiesNonStatic uns;


    [SerializeField] private float damage;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        cam = uns.playerCamera;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            collision.GetComponent<PlayerController>().IsDamaged(damage);
            cam.Shake();
        }
        gameObject.SetActive(false);
    }
}
