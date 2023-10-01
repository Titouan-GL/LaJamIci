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

    public void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
    }

    public void Update()
    {
        arrow.position = new Vector3(transform.position.x, transform.position.y, arrow.position.z);
        coordinates.text = (uns.player.positionOffset.x + levelCreator.tilePlayerIsOn.position.x).ToString() + " | " + (uns.player.positionOffset.y + levelCreator.tilePlayerIsOn.position.y).ToString();
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
        arrow.rotation = Quaternion.Lerp(arrow.rotation, Quaternion.Euler(0f, 0f, angleDegrees), 0.3f);
    }

    public override void SwitchIn()
    {
        arrow.gameObject.SetActive(true);
    }

    public override void SwitchOut()
    {
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
}
