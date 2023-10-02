using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private UtilitiesNonStatic uns;
    private EnnemyFollow ia;
    public float distanceDamage = 5f;
    public float damagePerSecondsMax = 60;
    PlayerController player;
    public bool active = false;
    private LevelCreator level;
    [SerializeField] private GameObject childobject;
    [SerializeField] private Object pickaxe;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip bossAmbAudioClip;

    public float speed;
    private BoxCollider2D boxCollider;

    public float timeBeforeSpawn;
    [SerializeField] private Vector2 respawnTimeRange;
    [SerializeField] private Vector2 despawnTimeRange;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        player = uns.player;
        level = uns.levelCreator;
        ia = GetComponent<EnnemyFollow>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        speed = 1+ 0.5f * pickaxe.level;
        ia.speed = speed;
        //damages
        timeBeforeSpawn -= Time.fixedDeltaTime;
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (active)
        {
            if (distanceFromPlayer < distanceDamage)
            {
                audioSource.PlayOneShot(bossAmbAudioClip);
                player.IsDamaged((1 - (distanceFromPlayer / distanceDamage)) * damagePerSecondsMax * Time.fixedDeltaTime);
            }
            if(timeBeforeSpawn < 0)
            {
                //ia.enabled = distanceFromPlayer > 10;
            }
            if (distanceFromPlayer > 40 || (timeBeforeSpawn < 0 && distanceFromPlayer > 20))
            {
                active = false;
                childobject.SetActive(false);
                ia.enabled = false;
                timeBeforeSpawn = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
            }
            boxCollider.enabled = player.artefacts >= 3;
        }
        else
        {
            if(timeBeforeSpawn <= 0)
            {
                foreach(Vector2Int pos in level.paths.Keys)
                {
                    if(Vector2.Distance(player.transform.position, pos * 2)> 30 && Vector2.Distance(player.transform.position, pos * 2) < 40 && level.map[pos.x][pos.y].parent != null)
                    {
                        transform.position = new Vector2(pos.x, pos.y)*2;
                        active = true;
                        childobject.SetActive(true);
                        ia.enabled = true;
                        ia.FindNextdestination();
                        timeBeforeSpawn = Random.Range(despawnTimeRange.x, despawnTimeRange.y);
                        return;
                    }
                }
            }
        }

    }
}
