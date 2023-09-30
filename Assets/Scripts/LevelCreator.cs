using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject ironBlock;
    [SerializeField] private GameObject groundBlock;
    [SerializeField] public int mapSize = 100;
    [SerializeField] private int numberOfIronTiles = 1000;

    [HideInInspector] public Tile[][] map;

    //private List<Tile> emptyTiles = new List<Tile>();
    private List<Path> paths = new List<Path>();
    [HideInInspector] public List<Tile> ironTiles = new List<Tile>();

    [System.Serializable]
    public struct Tile
    {
        public Vector2Int position;
        public bool empty;
        public bool exists;
        public RessourcesType ressourceType;
        public Tile(Vector2Int newposition, RessourcesType newrt)
        {
            position = newposition;
            ressourceType = newrt;
            empty = false;
            exists = false;
        }
    }

    public enum RessourcesType
    {
        Dirt,
        Iron
    }

    void Awake()
    {
        map = new Tile[mapSize][];

        for (int i = 0; i < mapSize; i++)
        {
            Tile[] mapPart = new Tile[mapSize];
            for(int j = 0; j < mapSize; j++)
            {
                mapPart[j] = new Tile(new Vector2Int(i, j), RessourcesType.Dirt);
            }
            map[i] = mapPart;
        }

        CreateGround(map[10][10]);

        for(int i  = 0; i < numberOfIronTiles; i++)
        {
            Vector2Int pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            while (map[pos.x][pos.y].ressourceType != RessourcesType.Dirt && map[pos.x][pos.y].empty == false)
            {
                pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            }
            map[pos.x][pos.y].ressourceType = RessourcesType.Iron;
            ironTiles.Add(map[pos.x][pos.y]);
        }

    }

    private void CreateGround(Tile tile)
    {
        GameObject go = Instantiate(groundBlock, new Vector2(tile.position.x, tile.position.y) * 2, Quaternion.identity, transform);
        tile.empty = true;
        map[tile.position.x][tile.position.y].empty = true;
        Path path = go.GetComponent<Path>();
        path.levelCreator = this;
        path.tile = tile;
        path.UpdateSurrounding(new Vector2Int(tile.position.x, tile.position.y));
        paths.Add(path);
        SpawnSurrounding(tile);
    }

    private void SpawnSurrounding(Tile tile)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2Int pos = tile.position + new Vector2Int(i, j);
                if (!map[pos.x][pos.y].empty && !map[pos.x][pos.y].exists)
                {
                    map[pos.x][pos.y].exists = true;
                    if (map[pos.x][pos.y].ressourceType == RessourcesType.Dirt)
                    {
                        GameObject go = Instantiate(block, new Vector2(pos.x, pos.y) * 2, Quaternion.identity, transform);
                        go.GetComponent<Diggable>().levelCreator = this;
                    }
                    else
                    {
                        GameObject go = Instantiate(ironBlock, new Vector2(pos.x, pos.y) * 2, Quaternion.identity, transform);
                        go.GetComponent<Diggable>().levelCreator = this;
                    }
                }
            }
        }
    }

    public void DestroyBlock(Vector2 position)
    {
        Vector2Int index = new Vector2Int((int)position.x, (int)position.y);
        Tile currentTile = map[index.x][index.y];
        CreateGround(currentTile);
    }
}
