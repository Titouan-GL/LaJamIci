using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Object
{
    public GameObject ammoSpriteForUI;
    public GameObject ammoEmptySpriteForUI;
    public GameObject rechargeSpriteForUI;
    public int maxAmmo;
    public int currentAmmo;
    public int currentRecharges;
    public int maxRecharges;


    float reloadTime = 0f;
    float reloadTimeMax = 0.1f;
    [SerializeField] private CameraScript cam;
    [SerializeField] Transform endRifle;

    [SerializeField]Transform shellPoint;
    [SerializeField]GameObject shellObject;
    [SerializeField]GameObject bulletObject;
    [SerializeField]GameObject fireLight;
    [SerializeField]GameObject rechargeObject;
    float lightTime = 0f;

    private GameObject fireAngleR;
    private GameObject fireAngleL;
    private GameObject fireAngle2R;
    private GameObject fireAngle2L;
    private GameObject fireAngle3R;
    private GameObject fireAngle3L;
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;
    public AudioClip reloadAudioClip;

    float fireAngleMin = 4f;
    float fireAngleMax = 15f;
    float currentFireAngle;
    float recalibration = 5f;
    float decalibration = 2f;
    float pushbackDuration = 0.2f;

    public AnimationCurve curve;
    [SerializeField] private Transform rifleModel;
    Vector3 riflePosition;


    [SerializeField] private Animator rifleAnim;

    private bool isReloading;

    public override void SwitchIn()
    {
    }

    public override void SwitchOut()
    {
    }


    public void Start(){
        fireLight.SetActive(false);
        currentFireAngle = fireAngleMin;

        riflePosition = rifleModel.localPosition;
        CreateFireAngle();

    }

    public void Update(){

        fireAngleR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, endRifle.rotation.eulerAngles.z + currentFireAngle));
        fireAngleL.transform.rotation = Quaternion.Euler(new Vector3(0, 0, endRifle.rotation.eulerAngles.z - currentFireAngle));

        if(currentFireAngle > fireAngleMin){
            currentFireAngle -= recalibration * Time.deltaTime;
        }
        else{
            currentFireAngle = fireAngleMin;
        }


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
            GameObject go2 = Instantiate(bulletObject, endRifle.position, Quaternion.Euler(endRifle.rotation.eulerAngles - new Vector3(0, 0, Random.Range(-currentFireAngle, currentFireAngle))));
            cam.Shake();
            reloadTime = reloadTimeMax;
            if(currentFireAngle < fireAngleMax){
                currentFireAngle += decalibration;
                if(currentFireAngle > fireAngleMax){
                    currentFireAngle = fireAngleMax;
                }
            }
            StartCoroutine(PushBack());
            currentAmmo -= 1;
        }
    }

    public override void Action2()
    {
        if (currentRecharges > 0 && currentAmmo < maxAmmo && !isReloading){
            audioSource.PlayOneShot(reloadAudioClip);
            rifleAnim.Play("ReloadRifle");
            isReloading = true;
        }
    }

    private void FillAmmo(){
        currentAmmo = maxAmmo;
        currentRecharges -= 1;
    }

    private void EvacuateRecharge(){
        GameObject go = Instantiate(rechargeObject, shellPoint.position, Quaternion.Euler(shellPoint.rotation.eulerAngles - new Vector3(0, 0, 0f))); 
    }


    void CreateFireAngle(){
        fireAngleR = new GameObject("fireAngleR");
        fireAngleL = new GameObject("fireAngleL");
        fireAngle2R = new GameObject("fireAngle2R");
        fireAngle2L = new GameObject("fireAngle2L");
        fireAngle3R = new GameObject("fireAngle3R");
        fireAngle3L = new GameObject("fireAngle3L");

        fireAngleR.transform.parent = endRifle;
        fireAngleL.transform.parent = endRifle;
        fireAngleR.transform.localPosition = new Vector3(0, 0, 0);
        fireAngleL.transform.localPosition = new Vector3(0, 0, 0);

        fireAngle2R.transform.parent = fireAngleR.transform;
        fireAngle2L.transform.parent = fireAngleL.transform;
        fireAngle2R.transform.localPosition = new Vector3(0, 1, 0);
        fireAngle2L.transform.localPosition = new Vector3(0, 1, 0);

        fireAngle3R.transform.parent = fireAngle2R.transform;
        fireAngle3L.transform.parent = fireAngle2L.transform;
        fireAngle3R.transform.localPosition = new Vector3(0, 3, 0);
        fireAngle3L.transform.localPosition = new Vector3(0, 3, 0);
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
