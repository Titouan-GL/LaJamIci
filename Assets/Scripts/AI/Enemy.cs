using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private UtilitiesNonStatic uns;
    private EnnemyFollow ia;
    public float range = 1f;
    public float damage = 10;
    public float speed = 4;
    PlayerController player;
    [SerializeField] GameObject hitbox;
    [SerializeField] Animator animator;

    public float attackSpeed = 0;

    public float attackDuration = 0;

    private void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        player = uns.player;
        ia = GetComponent<EnnemyFollow>();
    }

    private void FixedUpdate()
    {
        attackSpeed -= Time.fixedDeltaTime;
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer < range && attackSpeed <= 0 && attackDuration <= 0)
        {
            ia.speed = 0;
            animator.Play("PickaxeAttack");
            hitbox.SetActive(true);
            attackSpeed = 1;
            attackDuration = 0.5f;
        }
        else if(attackDuration > 0)
        {
            attackDuration -= Time.fixedDeltaTime;
            ia.speed = 0;
        }
        else if(distanceFromPlayer < range)
        {
            ia.speed = 0;
        }
        else
        {
            hitbox.SetActive(false);
            ia.speed = speed;
        }
    }

    public void ActivateHitbox(bool b)
    {
        hitbox.SetActive(b);
    }
}
