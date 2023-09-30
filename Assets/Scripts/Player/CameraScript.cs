using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public AnimationCurve curve;
    public float shakeDuration = 0.2f;
    private Quaternion intialRotation;

    private void Awake()
    {
        intialRotation = transform.localRotation;
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

    public void Shake(){
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking(){
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration){
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/shakeDuration);
            transform.localPosition = startPosition + Random.insideUnitSphere * strength;
            yield return null; 
        }
        
        transform.localPosition = startPosition;
    }
}
