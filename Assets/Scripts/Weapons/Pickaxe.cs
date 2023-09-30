using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Object
{
    float reloadTime = 0f;
    float reloadTimeMax = 1f;

    [SerializeField] private GameObject hitbox;

    [SerializeField] private Animator PickaxeAnim;

    private bool isReloading;


    public void Awake(){
        hitbox.SetActive(false);
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
            reloadTime = reloadTimeMax;
            PickaxeAnim.Play("PickaxeAttack");
        }
    }

    public override void Action2(){
    }
}
