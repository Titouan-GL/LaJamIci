using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Camera cam;
    private PlayerController playerController;
    Vector2 mousePos;
    public Transform debug;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInChildren<PlayerController>();
        myRB = GetComponentInChildren<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawLine( ray.origin, ray.origin+ray.direction *1000, Color.red);
        if (Physics.Raycast(ray.origin, ray.direction * 1000, out hit, Mathf.Infinity))
        {
            mousePos = hit.point;
        }
    }

    void FixedUpdate(){

        
        float multiplierRotation = 1f;

        Vector2 lookDir = mousePos - myRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        myRB.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(0, 0, myRB.rotation)), Quaternion.Euler(new Vector3(0, 0, angle)), 0.2f * multiplierRotation).eulerAngles.z;

    }
}
