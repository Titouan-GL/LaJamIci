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
    [SerializeField] private Object pickaxe;

    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private ParticleSystem[] particleSystemsPhysical;

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
        NoEmissions();
        ia.boss = true;
    }

    private void FixedUpdate()
    {
        speed = 1.3f+ 0.3f * pickaxe.level;
        ia.speed = speed;
        //damages
        timeBeforeSpawn -= Time.fixedDeltaTime;
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(ia.life <= 0)
        {
            Emission(false);
            ia.enabled = false;
            active = false;
            boxCollider.enabled = false;
        }
        else if (active)
        {
            if (distanceFromPlayer < distanceDamage)
            {
                player.IsDamaged((1 - (distanceFromPlayer / distanceDamage)) * damagePerSecondsMax * Time.fixedDeltaTime);
            }
            if(timeBeforeSpawn < 0)
            {
                //ia.enabled = distanceFromPlayer > 10;
            }
            if (distanceFromPlayer > 40 || (timeBeforeSpawn < 0 && distanceFromPlayer > 20))
            {
                active = false;
                Emission(false);
                ia.enabled = false;
                timeBeforeSpawn = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
            }
            boxCollider.enabled = player.artefactsMerged;
        }
        else
        {
            if(timeBeforeSpawn <= 0 || player.artefactsMerged)
            {
                foreach(Vector2Int pos in level.paths.Keys)
                {
                    if(Vector2.Distance(player.transform.position, pos * 2)> 30 && Vector2.Distance(player.transform.position, pos * 2) < 40 && level.map[pos.x][pos.y].parent != null && level.map[pos.x][pos.y].distanceToPlayer <= 40)
                    {
                        transform.position = new Vector2(pos.x, pos.y)*2;
                        active = true;
                        Emission(true);
                        ia.enabled = true;
                        ia.FindNextdestination();
                        timeBeforeSpawn = Random.Range(despawnTimeRange.x, despawnTimeRange.y);
                        return;
                    }
                }
            }
        }

    }

    private void Emission(bool b)
    {
        if (!player.artefactsMerged)
        {
            foreach (ParticleSystem ps in particleSystems)
            {
                if (b)
                {
                    ps.Play();
                }
                else
                {
                    ps.Stop();
                }
            }
        }
        else
        {
            foreach (ParticleSystem ps in particleSystemsPhysical)
            {
                if (b)
                {
                    ps.Play();
                }
                else
                {
                    ps.Stop();
                }
            }
        }
    }

    private void NoEmissions()
    {
        foreach (ParticleSystem ps in particleSystemsPhysical)
        {
            ps.Stop();
        }
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }
}
