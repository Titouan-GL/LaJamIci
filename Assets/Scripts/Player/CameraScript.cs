using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public AnimationCurve curve;
    public float shakeDuration = 0.2f;
    private Quaternion intialRotation;
    private UtilitiesNonStatic uns;

    private void Awake()
    {
        intialRotation = transform.localRotation;
        uns = UtilitiesStatic.GetUNS();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = intialRotation;
    }

    void Update()
    {
        transform.rotation = intialRotation;
    }

    public void Shake(float power = 1)
    {
        StartCoroutine(Shaking(power));
    }

    IEnumerator Shaking(float power = 1){
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration){
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/shakeDuration) * power *2* uns.shakeIntensity;
            transform.localPosition = startPosition + Random.insideUnitSphere * strength;
            yield return null; 
        }
        
        transform.localPosition = startPosition;
    }
}
