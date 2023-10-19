using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5;
    [SerializeField] GameObject craftMenu;

    private Rigidbody2D myRB;
    [SerializeField] Transform endRifle;


    public int[] ore;

    Vector2 movement;
    [HideInInspector] public bool isMoving;

    [HideInInspector] public int currentWeaponIndex = 0;
    [SerializeField] List<Object> weapons;
    [SerializeField] private Animator screenAnimator;

    [HideInInspector] public float life;
    [HideInInspector] public float lifemax = 100;

    public float artefacts = 0;
    public bool artefactsMerged = false;

    [HideInInspector] public List<int> logsIndexes;
    [SerializeField] private Logs logScript;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip openMenuAudioClip;
    [SerializeField] public AudioClip hitPlayerAudioClip;

    [SerializeField] private MainUIScript uiScript;

    [SerializeField] private GameObject rifleModel;

    [SerializeField] private Rifle rifleScript;

    private bool ZQSD = true;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ore = new int[3];
        life = lifemax;
        myRB = GetComponentInChildren<Rigidbody2D>();

        if (PlayerPrefs.HasKey("tier1Ore")) ore[0] = PlayerPrefs.GetInt("tier1Ore");
        if (PlayerPrefs.HasKey("tier2Ore")) ore[1] = PlayerPrefs.GetInt("tier2Ore");
        if (PlayerPrefs.HasKey("tier3Ore")) ore[2] = PlayerPrefs.GetInt("tier3Ore");

        if (PlayerPrefs.HasKey("tool1level")) weapons[0].level = PlayerPrefs.GetInt("tool1level");
        if (PlayerPrefs.HasKey("tool2level")) weapons[1].level = PlayerPrefs.GetInt("tool2level");
        if (PlayerPrefs.HasKey("tool3level")) weapons[2].level = PlayerPrefs.GetInt("tool3level");

        if(PlayerPrefs.HasKey("recharges")) rifleScript.currentRecharges = PlayerPrefs.GetInt("recharges");
        if (PlayerPrefs.HasKey("ZQSD"))
        {
            if (PlayerPrefs.GetInt("ZQSD") == 1)
            {
                ZQSD = true;
            }
            else
            {
                ZQSD = false;
            }
        }
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
        if (ZQSD)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        }
        else
        {
            movement.x = Input.GetAxis("HorizontalAlt");
            movement.y = Input.GetAxis("VerticalAlt");
            isMoving = Input.GetAxisRaw("HorizontalAlt") != 0 || Input.GetAxisRaw("VerticalAlt") != 0;
        }
        animator.SetInteger("Weapon", currentWeaponIndex);


        Debug.DrawLine(endRifle.position,transform.position+(endRifle.position - transform.position)*2);
        int layerMask = 1 << 9;
        layerMask += 1 << 17;


        HandleShooting();
        rifleModel.SetActive(weapons[1].level > 0);
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

    public void Save()
    {
        PlayerPrefs.SetInt("tier1Ore", ore[0]);
        PlayerPrefs.SetInt("tier2Ore", ore[1]);
        PlayerPrefs.SetInt("tier3Ore", ore[2]);

        PlayerPrefs.SetInt("tool1level", weapons[0].level);
        PlayerPrefs.SetInt("tool2level", weapons[1].level);
        PlayerPrefs.SetInt("tool3level", weapons[2].level);

        PlayerPrefs.SetInt("recharges", rifleScript.currentRecharges);
    }

    public void SwitchToZQSD()
    {
        ZQSD = true;
        PlayerPrefs.SetInt("ZQSD", 1);
    }
    public void SwitchToWASD()
    {
        ZQSD = false;
        PlayerPrefs.SetInt("ZQSD", 0);
    }
}
