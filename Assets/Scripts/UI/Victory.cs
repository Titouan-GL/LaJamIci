using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject boss;
    public MenuOpen victoryScreen;
    public PlayerController playerController;

    float timer = 5;

    private void Update()
    {
        if (boss.gameObject == null)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0)
        {
            playerController.enabled = false;
            victoryScreen.gameObject.SetActive(true);
            victoryScreen.OpenSelf();
            this.enabled = false;
        }
    }
}
