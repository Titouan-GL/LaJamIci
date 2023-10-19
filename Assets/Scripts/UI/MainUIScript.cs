using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] MenuOpen craftMenu;
    [SerializeField] MenuOpen logsMenu;
    [SerializeField] MenuOpen optionMenu;

    [SerializeField] MenuOpen logDisplayed;
    [SerializeField] MenuOpen[] settings;

    private int currentMenu = 0;


    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("Craft") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Logs"))
        {
            if(playerController.enabled == false)
            {
                playerController.enabled = true;
                if (currentMenu != 0)
                {
                    if (craftMenu.gameObject.activeSelf) craftMenu.CloseSelf();
                    if (logsMenu.gameObject.activeSelf) logsMenu.CloseSelf();
                    if (optionMenu.gameObject.activeSelf) optionMenu.CloseSelf();
                    CloseLogs();
                    CloseSettings();
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

    public void CloseLogs()
    {
        if (logDisplayed.gameObject.activeSelf) logDisplayed.CloseSelf();
    }

    public void CloseSettings()
    {
        foreach(MenuOpen m in settings)
        {
            if (m.gameObject.activeSelf) m.CloseSelf();
        }
    }

}
