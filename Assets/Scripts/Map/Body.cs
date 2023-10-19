using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Diggable
{
    [HideInInspector] public PlayerController playerController;
    private UtilitiesNonStatic uns;
    public int logNumber;
    public GameObject explosion;

    private AudioSource audioSource;
    [SerializeField] public AudioClip explosionSound;

    public void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        playerController = uns.player;
        audioSource = uns.playerAudioSource;
    }

    public override void TakeDamage(float amount)
    {
        life -= amount;
        if (life <= 0)
        {
            audioSource.PlayOneShot(explosionSound);
            Instantiate(explosion, transform.position, Quaternion.identity);
            playerController.AddLog(logNumber);
            playerController.enabled = false;
            Destroy(gameObject);
        }
    }


}
