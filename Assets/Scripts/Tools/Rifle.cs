using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rifle : Object
{
    public GameObject ammoSpriteForUI;
    public GameObject ammoEmptySpriteForUI;
    public GameObject rechargeSpriteForUI;

    public int currentRecharges;

    public int[] maxAmmo;
    public int currentAmmo;

    [HideInInspector] public int maxRecharges = 5;

    private float reloadTime = 0f;
    public float[] reloadTimeMax;


    [SerializeField] private GameObject[] bulletObject;
    [SerializeField] private Transform shellPoint;
    [SerializeField] private GameObject shellObject;
    [SerializeField] private GameObject fireLight;
    [SerializeField] private GameObject rechargeObject;

    [SerializeField] private CameraScript cam;
    [SerializeField] Transform endRifle;
    float lightTime = 0f;
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;
    public AudioClip reloadAudioClip;

    float pushbackDuration = 0.2f;

    public AnimationCurve curve;
    [SerializeField] private Transform rifleModel;
    Vector3 riflePosition;


    [SerializeField] private Animator rifleAnim;

    private bool isReloading;

    public override void SwitchIn()
    {
        isReloading = false;
    }

    public override void SwitchOut()
    {
        isReloading = false;
    }


    public void Start(){
        fireLight.SetActive(false);
        currentAmmo = maxAmmo[0];

        riflePosition = rifleModel.localPosition;

    }

    public void Update(){



        if(lightTime < 0){
            lightTime = 0;
            fireLight.SetActive(false);
        }
        else if(lightTime > 0){

            lightTime -= Time.deltaTime;
        }
    }

    public void FixedUpdate(){
        if(reloadTime > 0){
            reloadTime -= Time.fixedDeltaTime;
        }
    }

    public int GetMaxAmmo()
    {
        return maxAmmo[level];
    }
    public int GetMaxAmmoLevelMax()
    {
        return maxAmmo[maxAmmo.Length-1];
    }

    public void NotReloading()
    {
        isReloading = false;
    }

    public override void Use(){
        if(reloadTime <= 0 && currentAmmo > 0 && !isReloading){
            audioSource.PlayOneShot(shootingAudioClip);
            fireLight.SetActive(true);
            lightTime = 0.05f;
            GameObject go = Instantiate(shellObject, shellPoint.position, Quaternion.Euler(shellPoint.rotation.eulerAngles - new Vector3(0, 0, 90f))); 
            GameObject go2 = Instantiate(bulletObject[level], endRifle.position, Quaternion.Euler(endRifle.rotation.eulerAngles));
            go2.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            cam.Shake();
            reloadTime = reloadTimeMax[level];
            StartCoroutine(PushBack());
            currentAmmo -= 1;
        }
    }

    public override void Action2()
    {
        if (currentRecharges > 0 && currentAmmo < maxAmmo[level] && !isReloading){
            audioSource.PlayOneShot(reloadAudioClip);
            rifleAnim.Play("ReloadRifle");
            isReloading = true;
        }
    }

    public void FillAmmo(){
        currentAmmo = maxAmmo[level];
        currentRecharges -= 1;
    }

    public void EvacuateRecharge(){
        GameObject go = Instantiate(rechargeObject, shellPoint.position, Quaternion.Euler(shellPoint.rotation.eulerAngles - new Vector3(0, 0, 0f))); 
    }


    IEnumerator PushBack(){
        Vector3 startPosition = riflePosition;
        float elapsedTime = 0f;

        while(elapsedTime < pushbackDuration){
            elapsedTime += Time.deltaTime;
            float distance = curve.Evaluate(elapsedTime/pushbackDuration);
            rifleModel.localPosition = startPosition + new Vector3(0, -distance, 0);
            yield return null; 
        }
        
        rifleModel.localPosition = riflePosition;
    }
}
