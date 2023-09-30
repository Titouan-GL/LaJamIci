using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5;

    private Rigidbody2D myRB;
    [SerializeField] Transform endRifle;


    Vector2 movement;
    bool isMoving;

    [HideInInspector] public int currentWeaponIndex = 0;
    [SerializeField] List<Object> weapons;
    [SerializeField] private Animator screenAnimator;

    int life;
    int lifemax = 100;

    // Start is called before the first frame update
    void Start()
    {
        life = lifemax;
        myRB = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weapons[currentWeaponIndex].SwitchOut();
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex += 1;
            if (currentWeaponIndex >= weapons.Count)
            {
                currentWeaponIndex = 0;
            }
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].SwitchIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weapons[currentWeaponIndex].SwitchOut();
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex -= 1;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = weapons.Count-1;
            }
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].SwitchIn();
        }
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        Debug.DrawLine(endRifle.position,transform.position+(endRifle.position - transform.position)*2);
        int layerMask = 1 << 9;
        layerMask += 1 << 17;


        HandleShooting();
    }

    void FixedUpdate()
    {
        float multiplierSpeed = 1f;
        myRB.MovePosition(myRB.position + movement * moveSpeed* multiplierSpeed * Time.fixedDeltaTime);
    }

    private void HandleShooting(){
        if(Input.GetButton("Fire1")){
            weapons[currentWeaponIndex].Use();
        }
        if(Input.GetButton("Reload")){
            weapons[currentWeaponIndex].Action2();
        }
    }

    public float GetLifeFraction(){
        return (life*1.0f)/lifemax;
    }

    public void IsDamaged(int damage){
        life -= damage;
        screenAnimator.Play("DamageFade");
    }
}
