using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] ressourceQuantityText;
    [SerializeField] private TextMeshProUGUI[] recipe1;
    [SerializeField] private TextMeshProUGUI[] recipe2;
    [SerializeField] private TextMeshProUGUI[] recipe3;
    [SerializeField] private TextMeshProUGUI[] ammoCosts;
    [SerializeField] private GameObject[] upgradeMenus;
    [SerializeField] private GameObject[] levelMaxMenus;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject[] artefacts;
    [SerializeField] private GameObject[] artefactsParts;

    [SerializeField] private Button artefactbutton;
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private Button[] ammoButtons;

    [SerializeField] private Button healButton;
    [SerializeField] private TextMeshProUGUI healText;

    [SerializeField] private Craft craft;

    private int numberOfLevels = 3;


    private void Awake()
    {
        SetInteractables();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ressourceQuantityText.Length; i++)
        {
            ressourceQuantityText[i].text = playerController.ore[i].ToString();
        }

        if (craft.toolsLevel[0] < numberOfLevels)
        {
            upgradeMenus[0].SetActive(true);
            levelMaxMenus[0].SetActive(false);
            for (int i = 0; i < recipe1.Length; i++)
            {
                recipe1[i].text = playerController.ore[i] + "/" + craft.recipes[0][craft.toolsLevel[0]][i];
            }
        }
        else
        {
            upgradeMenus[0].SetActive(false);
            levelMaxMenus[0].SetActive(true);
        }


        if (craft.toolsLevel[1] < numberOfLevels)
        {
            upgradeMenus[1].SetActive(true);
            levelMaxMenus[1].SetActive(false);
            for (int i = 0; i < recipe2.Length; i++)
            {
                recipe2[i].text = playerController.ore[i] + "/" + craft.recipes[1][craft.toolsLevel[1]][i];
            }
        }
        else
        {
            upgradeMenus[1].SetActive(false);
            levelMaxMenus[1].SetActive(true);
        }


        if (craft.toolsLevel[2] < numberOfLevels)
        {
            upgradeMenus[2].SetActive(true);
            levelMaxMenus[2].SetActive(false);
            for (int i = 0; i < recipe3.Length; i++)
            {
                recipe3[i].text = playerController.ore[i] + "/" + craft.recipes[2][craft.toolsLevel[2]][i];
            }
        }
        else
        {
            upgradeMenus[2].SetActive(false);
            levelMaxMenus[2].SetActive(true);
        }


        for(int i = 0; i < ammoCosts.Length; i++)
        {
            ammoCosts[i].text = playerController.ore[i] + "/" + craft.ammoCosts[i];
        }

        for(int i = 0; i < playerController.artefacts; i++)
        {
            artefactsParts[i].SetActive(true);
        }
        SetInteractables();
        SetHealable();
    }

    public void SetInteractables()
    {

        artefactbutton.interactable = playerController.artefacts >= 3;
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].interactable = craft.CanUpgrade(i);
        }
        for (int i = 0; i < ammoButtons.Length; i++)
        {
            ammoButtons[i].interactable = craft.CanBuyAmmo(i);
        }
    }

    public void Merge()
    {
        if(playerController.artefacts >= 3)
        {
            playerController.artefactsMerged = true;
            artefacts[0].SetActive(false);
            artefacts[1].SetActive(true);
        }
    }

    public void SetHealable()
    {
        healButton.interactable = craft.CanHeal();
        healText.text = playerController.ore[2] + "/" + craft.healCost;
    }
}
