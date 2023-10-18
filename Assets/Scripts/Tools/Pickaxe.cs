using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Object
{
    float reloadTime = 0f;

    [SerializeField] private int[] damage;

    [SerializeField] private float[] reloadTimeMax;

    [SerializeField] private GameObject hitbox;

    [SerializeField] private Animator PickaxeAnim;

    private bool isReloading;
    public AudioSource audioSource;
    public AudioClip whooshAudioClip;
    public void Awake(){
        hitbox.SetActive(false);
    }

    public int GetDamage()
    {
        return (damage[level]);
    }

    void FixedUpdate()
    {
    }

    public override void SwitchIn()
    {
    }

    public override void SwitchOut()
    {
        DeactivateHitBox();
        NotReloading();
    }

    public void NotReloading()
    {
        isReloading = false;
    }

    public void ActivateHitBox()
    {
        hitbox.SetActive(true);
    }

    public void DeactivateHitBox()
    {
        hitbox.SetActive(false);
    }

    public override void Use()
    {
        PickaxeAnim.SetFloat("PickaxeLevel", level);
        if (!isReloading)
        {
            reloadTime = reloadTimeMax[level];
            audioSource.PlayOneShot(whooshAudioClip);
            PickaxeAnim.Play("PickaxeAttack");
            isReloading = true;
        }
    }

    public override void Action2(){
    }
}
