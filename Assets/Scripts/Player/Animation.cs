using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Animation : MonoBehaviour
{
    [SerializeField] private Rifle rifle;
    [SerializeField] private Pickaxe pickaxe;

    public void ActivateHitBox()
    {
        pickaxe.ActivateHitBox();
    }

    public void DeactivateHitBox()
    {
        pickaxe.DeactivateHitBox();
    }
    public void NotReloading()
    {
        rifle.NotReloading();
        pickaxe.NotReloading();
    }
    public void FillAmmo()
    {
        rifle.FillAmmo();
    }
    public void EvacuateRecharge()
    {
        rifle.EvacuateRecharge();
    }
}
