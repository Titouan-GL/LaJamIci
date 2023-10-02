using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] private Craft craft;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < ressourceQuantityText.Length; i++)
        {
            ressourceQuantityText[i].text = playerController.ore[i].ToString();
        }

        if (craft.toolsLevel[0] < 3)
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


        if (craft.toolsLevel[1] < 3)
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


        if (craft.toolsLevel[2] < 3)
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
    }
}
