using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    UtilitiesNonStatic uns;
    PlayerController player;
    Animator animator;

    void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        player = uns.player;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
