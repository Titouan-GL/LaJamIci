using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public EnnemyFollow boss;
    public MenuOpen victoryScreen;
    public MenuOpen defeatScreen;
    public PlayerController playerController;

    float timer = 5;

    private void Update()
    {

        if(timer < 0)
        {
            playerController.enabled = false;
            victoryScreen.gameObject.SetActive(true);
            victoryScreen.OpenSelf();
            this.enabled = false;
        }
        else if (boss.life <= 0)
        {
            timer -= Time.deltaTime;
        }
        else if (playerController.life < 0)
        {
            defeatScreen.gameObject.SetActive(true);
            defeatScreen.OpenSelf();
            this.enabled = false;
            playerController.Save();
        }


    }
}
