using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Compass : Object
{
    [SerializeField] LevelCreator levelCreator;
    [SerializeField] Transform arrow;

    public void Update()
    {

        Vector2 closestpos = new Vector2(Mathf.Infinity, Mathf.Infinity);
        foreach (KeyValuePair<Vector2Int, LevelCreator.Tile> kvp in levelCreator.tier1Tiles)
        {
            Vector2Int key = kvp.Key;
            LevelCreator.Tile value = kvp.Value;

            Vector2 pos = value.position*2;
            if (Vector2.Distance(pos, arrow.position) < Vector2.Distance(closestpos, arrow.position))
            {
                closestpos = pos;
            }

        }

        float angleRadians = Mathf.Atan2(closestpos.y -arrow.position.y, closestpos.x - arrow.position.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        arrow.position = new Vector3(transform.position.x, transform.position.y, arrow.position.z);
        arrow.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
    }

    public override void SwitchIn()
    {
        arrow.gameObject.SetActive(true);
    }

    public override void SwitchOut()
    {
        arrow.gameObject.SetActive(false);
    }

    public override void Use(){
    }

    public override void Action2(){
    }
}
