using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [HideInInspector] public LevelCreator levelCreator;

    public GameObject side;
    public GameObject cornerIn;
    public GameObject cornerOut;
    public GameObject cornerStraight;

    public LevelCreator.Tile tile;

    private List<GameObject> borders = new List<GameObject>();
    public void UpdateSurrounding(Vector2Int pos)
    {
        clearList();
        GameObject go = null;
        if (pos.y > 0 && !levelCreator.map[pos.x][pos.y - 1].empty)
        {
            go = Instantiate(side, transform.position, Quaternion.Euler(0f, 0f, -90f), transform);
        }

        if (pos.y < levelCreator.mapSize - 1 && !levelCreator.map[pos.x][pos.y + 1].empty)
        {
            go = Instantiate(side, transform.position, Quaternion.Euler(0f, 0f, 90f), transform);
        }

        if (pos.x > 0 && !levelCreator.map[pos.x - 1][pos.y].empty)
        {
            go = Instantiate(side, transform.position, Quaternion.Euler(0f, 0f, 180f), transform);
        }

        if (pos.x < levelCreator.mapSize - 1 && !levelCreator.map[pos.x + 1][pos.y].empty)
        {
            go = Instantiate(side, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);
        }
        if (go != null)
        {
            borders.Add(go);
        }
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
