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
        Debug.Log("1");
        if(collision.GetComponent<PlayerController>() != null)
        {
            Debug.Log("2");
            collision.GetComponent<PlayerController>().IsDamaged(damage);
            cam.Shake();
        }
        gameObject.SetActive(false);
    }
}
