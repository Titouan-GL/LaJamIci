using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private Pickaxe pickaxe;
    public AudioClip dirthit;
    public AudioClip enemyHit;
    public AudioSource audioSource;
    private CameraScript cam;
    private UtilitiesNonStatic uns;

    public GameObject impact;

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
            audioSource.PlayOneShot(dirthit);
            cam.Shake(0.5f);
        }
        if (collision.GetComponent<EnnemyFollow>() != null)
        {
            collision.GetComponent<EnnemyFollow>().TakeDamage(pickaxe.GetDamage());
            audioSource.PlayOneShot(enemyHit);
            cam.Shake(0.5f);
        }
        if(impact != null) Instantiate(impact, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
