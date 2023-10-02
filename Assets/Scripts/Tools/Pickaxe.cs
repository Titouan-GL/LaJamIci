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

    public void FixedUpdate(){
        if(reloadTime > 0){
            reloadTime -= Time.fixedDeltaTime;
        }
    }

    public override void SwitchIn()
    {
    }

    public override void SwitchOut()
    {
    }

    public void NotReloading(){
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

    public override void Use(){
        if(reloadTime <= 0 && !isReloading){
            reloadTime = reloadTimeMax[level];
            audioSource.PlayOneShot(whooshAudioClip);
            PickaxeAnim.Play("PickaxeAttack"+level);
        }
    }

    public override void Action2(){
    }
}
