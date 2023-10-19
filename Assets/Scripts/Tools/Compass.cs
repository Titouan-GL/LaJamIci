using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Compass : Object
{
    [SerializeField] LevelCreator levelCreator;
    [SerializeField] Transform arrow;
    [SerializeField] GameObject compass;
    [SerializeField] GameObject[] compasslevel;
    [SerializeField] TMP_Text coordinates;
    private UtilitiesNonStatic uns;
    public int oreTargeted = 0;

    [SerializeField] GameObject test;


    [SerializeField] GameObject radarPanel;
    [SerializeField] GameObject[] ressourcesUI;

    public void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
    }

    public void Update()
    {
        arrow.position = new Vector3(transform.position.x, transform.position.y, arrow.position.z);
        coordinates.text = levelCreator.GetStringPositionOffseted(levelCreator.tilePlayerIsOn.position.x, levelCreator.tilePlayerIsOn.position.y);
        //coordinates.text = (levelCreator.positionOffset.x + levelCreator.tilePlayerIsOn.position.x).ToString() + " | " + (levelCreator.positionOffset.y + levelCreator.tilePlayerIsOn.position.y).ToString();
    }

    public void FixedUpdate()
    {
        Vector2 closestpos = new Vector2(Mathf.Infinity, Mathf.Infinity);
        Dictionary<Vector2Int, LevelTile> searchedTiles = levelCreator.tier1Tiles;
        if (oreTargeted == 1)
        {
            searchedTiles = levelCreator.tier2Tiles;
        }
        if (oreTargeted == 2)
        {
            searchedTiles = levelCreator.tier3Tiles;
        }
        if (oreTargeted == 3)
        {
            searchedTiles = levelCreator.artefactsTiles;
        }
        foreach (KeyValuePair<Vector2Int, LevelTile> kvp in searchedTiles)
        {
            Vector2Int key = kvp.Key;
            LevelTile value = kvp.Value;

            Vector2 pos = value.position * 2;
            if (Vector2.Distance(pos, arrow.position) < Vector2.Distance(closestpos, arrow.position))
            {
                closestpos = pos;
            }

        }
        float angleRadians = Mathf.Atan2(closestpos.y - arrow.position.y, closestpos.x - arrow.position.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        test.transform.position = closestpos;
        arrow.rotation = Quaternion.Lerp(arrow.rotation, Quaternion.Euler(0f, 0f, angleDegrees), 0.3f);
        if(closestpos.x == Mathf.Infinity)
        {
            arrow.gameObject.SetActive(false);
        }
        UpdateOreTargeted();
    }

    public override void SwitchIn()
    {
        arrow.gameObject.SetActive(true);
    }

    public override void SwitchOut()
    {
        compasslevel[level].SetActive(false);
        compass.SetActive(false);
        arrow.gameObject.SetActive(false);
    }

    public override void Use()
    {
    }

    public override void Action2()
    {
        compasslevel[level].SetActive(!compass.activeSelf);
        compass.SetActive(!compass.activeSelf);
    }

    public void ChangeOreTargeted(int oretype)
    {
        oreTargeted = oretype;
    }

    public void OnEnable()
    {
        radarPanel.SetActive(true);
    }

    public void OnDisable()
    {
        radarPanel.SetActive(false);
    }

    public void UpdateOreTargeted()
    {
        ressourcesUI[0].SetActive(false);
        ressourcesUI[1].SetActive(false);
        ressourcesUI[2].SetActive(false);
        ressourcesUI[3].SetActive(false);
        ressourcesUI[oreTargeted].SetActive(true);
    }
}
