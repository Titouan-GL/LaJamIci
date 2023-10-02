using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Animation : MonoBehaviour
{
    [SerializeField] private GameObject pickaxeHitbox;
    [SerializeField] private Rifle rifle;

    public void ActivateHitBox()
    {
        pickaxeHitbox.SetActive(true);
    }

    public void DeactivateHitBox()
    {
        pickaxeHitbox.SetActive(false);
    }
    public void NotReloading()
    {
        rifle.NotReloading();
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
