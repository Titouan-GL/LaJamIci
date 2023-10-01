using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [HideInInspector] public LevelCreator levelCreator;

    public GameObject dirtSide;
    public GameObject tier1Side;
    public GameObject tier2Side;
    public GameObject tier3Side;

    public LevelCreator.Tile tile;

    private List<GameObject> borders = new List<GameObject>();
    public void UpdateSurrounding(Vector2Int pos)
    {
        clearList();
        if (pos.y > 0 && !levelCreator.map[pos.x][pos.y - 1].empty)
        {
            InstantiateSide(Quaternion.Euler(0f, 0f, -90f), levelCreator.map[pos.x][pos.y - 1]);
        }

        if (pos.y < levelCreator.mapSize - 1 && !levelCreator.map[pos.x][pos.y + 1].empty)
        {
            InstantiateSide(Quaternion.Euler(0f, 0f, 90f), levelCreator.map[pos.x][pos.y + 1]);
        }

        if (pos.x > 0 && !levelCreator.map[pos.x - 1][pos.y].empty)
        {
            InstantiateSide(Quaternion.Euler(0f, 0f, 180f), levelCreator.map[pos.x - 1][pos.y]);
        }

        if (pos.x < levelCreator.mapSize - 1 && !levelCreator.map[pos.x + 1][pos.y].empty)
        {
            InstantiateSide(Quaternion.Euler(0f, 0f, 0f), levelCreator.map[pos.x + 1][pos.y]);
        }
    }

    private void InstantiateSide(Quaternion rotation, LevelCreator.Tile tile)
    {
        GameObject go = null;
        if (tile.ressourceType == LevelCreator.RessourcesType.Dirt)
        {
            go = Instantiate(dirtSide, transform.position, rotation, transform);
        }
        else if (tile.ressourceType == LevelCreator.RessourcesType.Tier1)
        {
            go = Instantiate(tier1Side, transform.position, rotation, transform);
        }
        else if (tile.ressourceType == LevelCreator.RessourcesType.Tier2)
        {
            go = Instantiate(tier2Side, transform.position, rotation, transform);
        }
        else if (tile.ressourceType == LevelCreator.RessourcesType.Tier3)
        {
            go = Instantiate(tier3Side, transform.position, rotation, transform);
        }
        borders.Add(go);
    }

    public void clearList()
    {
        foreach(GameObject go in borders)
        {
            Destroy(go);
        }
        borders.Clear();
    }
}
