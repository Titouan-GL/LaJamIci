using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Dictionary<Vector2Int, Path> paths = new Dictionary<Vector2Int, Path>();
    [HideInInspector] public Dictionary<Vector2Int, Tile> ironTiles = new Dictionary<Vector2Int, Tile>();

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
            ironTiles[pos] = map[pos.x][pos.y];
        }

    }

    private void CreateGround(Tile tile)
    {
        GameObject go = Instantiate(groundBlock, new Vector2(tile.position.x, tile.position.y) * 2, Quaternion.identity, transform);
        if (ironTiles.ContainsKey(tile.position))
        {
            Debug.Log("deleted");
            ironTiles.Remove(tile.position);
        }
        tile.empty = true;
        map[tile.position.x][tile.position.y].empty = true;
        Path path = go.GetComponent<Path>();
        path.levelCreator = this;
        path.tile = tile;
        path.UpdateSurrounding(tile.position);
        paths[tile.position] = path;
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
                else if (paths.ContainsKey(pos) && !(i == 0 && j == 0))
                {
                    paths[pos].UpdateSurrounding(pos);
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
