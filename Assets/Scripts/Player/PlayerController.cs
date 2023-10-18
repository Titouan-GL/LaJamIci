using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5;
    [SerializeField] GameObject craftMenu;

    private Rigidbody2D myRB;
    [SerializeField] Transform endRifle;

    [HideInInspector] public Vector2Int positionOffset;

    public int[] ore;

    Vector2 movement;
    bool isMoving;

    [HideInInspector] public int currentWeaponIndex = 0;
    [SerializeField] List<Object> weapons;
    [SerializeField] private Animator screenAnimator;

    float life;
    float lifemax = 100;

    public float artefacts = 0;
    public bool artefactsMerged = false;

    [HideInInspector] public List<int> logsIndexes;
    [SerializeField] private Logs logScript;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip openMenuAudioClip;
    [SerializeField] public AudioClip hitPlayerAudioClip;

    [SerializeField] private MainUIScript uiScript;

    [SerializeField] private GameObject riflleModel;


    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ore = new int[3];
        life = lifemax;
        myRB = GetComponentInChildren<Rigidbody2D>();
        positionOffset = new Vector2Int(Random.Range(0, 100000), Random.Range(0, 10000));
    }

    void Update()
    {
        if (Input.GetButtonDown("Craft"))
        {
            uiScript.ActivateCraftMenu();
            audioSource.PlayOneShot(openMenuAudioClip);
        }
        if (Input.GetButtonDown("Logs"))
        {
            uiScript.ActivateLogsMenu();
            audioSource.PlayOneShot(openMenuAudioClip);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            uiScript.ActivateOptionMenu();
            audioSource.PlayOneShot(openMenuAudioClip);
        }


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weapons[currentWeaponIndex].SwitchOut();
            weapons[currentWeaponIndex].gameObject.SetActive(false);

            currentWeaponIndex += 1;
            if (currentWeaponIndex >= weapons.Count)
            {
                currentWeaponIndex = 0;
            }
            if (currentWeaponIndex == 1 && weapons[1].level == 0)
            {
                currentWeaponIndex = 2;
            }
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].SwitchIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weapons[currentWeaponIndex].SwitchOut();
            weapons[currentWeaponIndex].gameObject.SetActive(false);

            currentWeaponIndex -= 1;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = weapons.Count-1;
            }
            if (currentWeaponIndex == 1 && weapons[1].level == 0)
            {
                currentWeaponIndex = 0;
            }
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].SwitchIn();
        }
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        animator.SetInteger("Weapon", currentWeaponIndex);

        Debug.DrawLine(endRifle.position,transform.position+(endRifle.position - transform.position)*2);
        int layerMask = 1 << 9;
        layerMask += 1 << 17;


        HandleShooting();
        riflleModel.SetActive(weapons[1].level > 0);
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
        if(Input.GetButtonDown("Reload")){
            weapons[currentWeaponIndex].Action2();
        }
    }

    public float GetLifeFraction(){
        return (life*1.0f)/lifemax;
    }

    public void IsDamaged(float damage){
        life -= damage;
        screenAnimator.Play("DamageFade");
        audioSource.PlayOneShot(hitPlayerAudioClip);
    }

    public void IncreaseOre(int amount, int oreTier)
    {
        ore[oreTier] += amount;
    }

    public void AddLog(int index)
    {
        logsIndexes.Add(index);
        logScript.AddLog(index);
        uiScript.ActivateLogsMenu();
    }
}
