using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Craft : MonoBehaviour
{
    private PlayerController playerController;

    [HideInInspector] public int[][][] recipes; // [tool][level][oretype]
    [HideInInspector] public int[] ammoCosts; // [tool][level][oretype]
    [HideInInspector] public int[] toolsLevel;

    [SerializeField] private Object[] tools;
    [SerializeField] private Rifle rifle;

    private int maxToolLevel = 3;

    [HideInInspector] public int healCost = 2;

    public void Awake()
    {
        int[][] recipesTool1;
        int[][] recipesTool2;
        int[][] recipesTool3;

        recipesTool1 = new int[3][];
        recipesTool1[0] = new int[] { 5, 0, 0 };
        recipesTool1[1] = new int[] { 15, 6, 0 };
        recipesTool1[2] = new int[] { 25, 9, 3 };

        recipesTool2 = new int[3][];
        recipesTool2[0] = new int[] { 5, 0, 0 };
        recipesTool2[1] = new int[] { 15, 6, 0 };
        recipesTool2[2] = new int[] { 25, 9, 3 };

        recipesTool3 = new int[3][];
        recipesTool3[0] = new int[] { 5, 0, 0 };
        recipesTool3[1] = new int[] { 15, 6, 0 };
        recipesTool3[2] = new int[] { 25, 9, 3 };

        recipes = new int[3][][];
        recipes[0] = recipesTool1;
        recipes[1] = recipesTool2;
        recipes[2] = recipesTool3;

        toolsLevel = new int[] { 0, 0, 0 };
        ammoCosts = new int[] { 6, 2, 0 };

        playerController = GetComponentInChildren<PlayerController>();
    }

    public void Upgrade(int tool)
    {
        if(CanUpgrade(tool))
        {
            playerController.ore[0] -= recipes[tool][toolsLevel[tool]][0];
            playerController.ore[1] -= recipes[tool][toolsLevel[tool]][1];
            playerController.ore[2] -= recipes[tool][toolsLevel[tool]][2];
            toolsLevel[tool] += 1;
        }
        tools[tool].level = (toolsLevel[tool]);

        if(tool == 1)
        {
            tools[tool].gameObject.GetComponent<Rifle>().FillFreeAmmo();
        }
    }

    public bool CanUpgrade(int tool)
    {

        if (toolsLevel[tool] < maxToolLevel)
        {
            if (playerController.ore[0] >= recipes[tool][toolsLevel[tool]][0] &&
                playerController.ore[1] >= recipes[tool][toolsLevel[tool]][1] &&
                playerController.ore[2] >= recipes[tool][toolsLevel[tool]][2])
            {
                return true;
            }
        }
        return false;
    }

    public void BuyAmmo(int ressourceType)
    {
        if(CanBuyAmmo(ressourceType))
        {
            playerController.ore[ressourceType] -= ammoCosts[ressourceType];
            rifle.currentRecharges += 1;
        }
    }
    public bool CanBuyAmmo(int ressourceType)
    {

        return playerController.ore[ressourceType] >= ammoCosts[ressourceType] && rifle.currentRecharges < rifle.maxRecharges;
    }
    public void Heal()
    {
        if (CanHeal())
        {
            playerController.ore[2] -= healCost;
            playerController.life = playerController.lifemax;
        }
    }
    public bool CanHeal()
    {

        return playerController.ore[2] >= healCost && playerController.life < playerController.lifemax;
    }
}
