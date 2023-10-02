using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] GameObject craftMenu;
    [SerializeField] GameObject logsMenu;
    [SerializeField] GameObject optionMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Craft") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Logs"))
        {
            playerController.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void ActivateCraftMenu()
    {
        gameObject.SetActive(true);
        craftMenu.SetActive(true);
        logsMenu.SetActive(false);
        optionMenu.SetActive(false);
        playerController.enabled = false;
    }
    public void ActivateLogsMenu()
    {
        gameObject.SetActive(true);
        craftMenu.SetActive(false);
        logsMenu.SetActive(true);
        optionMenu.SetActive(false);
        playerController.enabled = false;
    }
    public void ActivateOptionMenu()
    {
        gameObject.SetActive(true);
        craftMenu.SetActive(false);
        logsMenu.SetActive(false);
        optionMenu.SetActive(true);
        playerController.enabled = false;
    }
}
