using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Compass : Object
{
    [SerializeField] LevelCreator levelCreator;
    [SerializeField] GameObject Arrow;

    public void Update()
    {
        LevelCreator.Tile closestTile = levelCreator.ironTiles[0];
        foreach (LevelCreator.Tile tile in levelCreator.ironTiles)
        {
            Vector3 pos = new Vector3(tile.position.x, tile.position.y, 0);
            Vector3 closestpos = new Vector3(closestTile.position.x, closestTile.position.y, 0);
            if (Vector3.Distance(pos, transform.position) < Vector3.Distance(closestpos, transform.position))
            {
                closestTile = tile;
            }
        }
        Debug.Log(closestTile.position);
    }

    public override void SwitchIn()
    {
        Arrow.SetActive(true);
    }

    public override void SwitchOut()
    {
        Arrow.SetActive(false);
    }

    public override void Use(){
    }

    public override void Action2(){
    }
}
