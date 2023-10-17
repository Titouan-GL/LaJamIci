using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] MenuOpen craftMenu;
    [SerializeField] MenuOpen logsMenu;
    [SerializeField] MenuOpen optionMenu;

    private int currentMenu = 0;


    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("Craft") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Logs"))
        {
            Debug.Log(currentMenu);
            if(playerController.enabled == false)
            {
                playerController.enabled = true;
                if (currentMenu != 0)
                {
                    if (craftMenu.gameObject.activeSelf) craftMenu.CloseSelf();
                    if (logsMenu.gameObject.activeSelf) logsMenu.CloseSelf();
                    if (optionMenu.gameObject.activeSelf) optionMenu.CloseSelf();
                    currentMenu = 0;
                }
            }
            else
            {
                playerController.enabled = false;
            }
        }
    }

    public void ActivateCraftMenu()
    {
        gameObject.SetActive(true);
        craftMenu.gameObject.SetActive(true);
        craftMenu.OpenSelf();
        if(logsMenu.gameObject.activeSelf) logsMenu.CloseSelf();
        if (optionMenu.gameObject.activeSelf)  optionMenu.CloseSelf();
    }
    public void ActivateLogsMenu()
    {
        gameObject.SetActive(true);
        if (craftMenu.gameObject.activeSelf)  craftMenu.CloseSelf();
        logsMenu.gameObject.SetActive(true);
        logsMenu.OpenSelf();
        if (optionMenu.gameObject.activeSelf)  optionMenu.CloseSelf();
    }
    public void ActivateOptionMenu()
    {
        gameObject.SetActive(true);
        if (craftMenu.gameObject.activeSelf)  craftMenu.CloseSelf();
        if (logsMenu.gameObject.activeSelf)  logsMenu.CloseSelf();
        optionMenu.gameObject.SetActive(true);
        optionMenu.OpenSelf();
    }

    public void SetCurrentMenu(int m)
    {
        currentMenu = m;
    }

}
