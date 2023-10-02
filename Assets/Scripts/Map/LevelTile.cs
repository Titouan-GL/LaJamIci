using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static LevelCreator;

public class LevelTile
{
    private UtilitiesNonStatic uns;
    private LevelCreator levelCreator;

    public Vector2Int position;
    public bool empty = false;
    public bool exists = false;
    public RessourcesType ressourceType;
    public GameObject go = null;
    public LevelTile parent = null;
    public int distanceToPlayer = -1;
    public LevelTile(Vector2Int newposition, RessourcesType newrt, LevelTile nextTileToPlayer = null)
    {
        position = newposition;
        ressourceType = newrt;
        this.parent = nextTileToPlayer;
        uns = UtilitiesStatic.GetUNS();
        levelCreator = uns.levelCreator;
    }

    public int CountDistance()
    {
        if(parent == null)
        {
            return 0;
        }
        else
        {
            return 1 + parent.CountDistance();
        }
    }
}
