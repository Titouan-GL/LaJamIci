using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject ironBlock;
    [SerializeField] private GameObject groundBlock;
    [SerializeField] public int mapSize = 100;
    [SerializeField] private int numberOfTier1Tiles = 1000;
    [SerializeField] private int numberOfTier2Tiles = 400;
    [SerializeField] private int numberOfTier3Tiles = 100;

    [HideInInspector] public LevelTile[][] map;

    //private List<Tile> emptyTiles = new List<Tile>();
    private Dictionary<Vector2Int, Path> paths = new Dictionary<Vector2Int, Path>();
    [HideInInspector] public Dictionary<Vector2Int, LevelTile> tier1Tiles = new Dictionary<Vector2Int, LevelTile>();
    [HideInInspector] public Dictionary<Vector2Int, LevelTile> tier2Tiles = new Dictionary<Vector2Int, LevelTile>();
    [HideInInspector] public Dictionary<Vector2Int, LevelTile> tier3Tiles = new Dictionary<Vector2Int, LevelTile>();

    [SerializeField] private PlayerController player;

    private LevelTile tilePlayerIsOn;

    public enum RessourcesType
    {
        Dirt,
        Tier1,
        Tier2,
        Tier3

    }

    void Awake()
    {
        map = new LevelTile[mapSize][];

        for (int i = 0; i < mapSize; i++)
        {
            LevelTile[] mapPart = new LevelTile[mapSize];
            for (int j = 0; j < mapSize; j++)
            {
                mapPart[j] = new LevelTile(new Vector2Int(i, j), RessourcesType.Dirt);
            }
            map[i] = mapPart;
        }

        CreateGround(map[10][10]);
        DetectPlayerPos();
        DestroyBlock(new Vector2Int(10, 11));
        DestroyBlock(new Vector2Int(10, 12));
        DestroyBlock(new Vector2Int(10, 13));
        DestroyBlock(new Vector2Int(10, 14));
        DestroyBlock(new Vector2Int(10, 15));
        DestroyBlock(new Vector2Int(10, 16));
        DestroyBlock(new Vector2Int(10, 17));
        DestroyBlock(new Vector2Int(10, 18));
        DestroyBlock(new Vector2Int(10, 19));
        DestroyBlock(new Vector2Int(10, 20));

        for (int i = 0; i < numberOfTier1Tiles; i++)
        {
            Vector2Int pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            while (map[pos.x][pos.y].ressourceType != RessourcesType.Dirt && map[pos.x][pos.y].empty == false)
            {
                pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            }
            map[pos.x][pos.y].ressourceType = RessourcesType.Tier1;
            tier1Tiles[pos] = map[pos.x][pos.y];
        }

        for (int i = 0; i < numberOfTier2Tiles; i++)
        {
            Vector2Int pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            while (map[pos.x][pos.y].ressourceType != RessourcesType.Dirt && map[pos.x][pos.y].empty == false)
            {
                pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            }
            map[pos.x][pos.y].ressourceType = RessourcesType.Tier2;
            tier2Tiles[pos] = map[pos.x][pos.y];
        }

        for (int i = 0; i < numberOfTier3Tiles; i++)
        {
            Vector2Int pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            while (map[pos.x][pos.y].ressourceType != RessourcesType.Dirt && map[pos.x][pos.y].empty == false)
            {
                pos = new Vector2Int(Random.Range(0, mapSize), Random.Range(0, mapSize));
            }
            map[pos.x][pos.y].ressourceType = RessourcesType.Tier3;
            tier3Tiles[pos] = map[pos.x][pos.y];
        }

    }

    public void Update()
    {
        DetectPlayerPos();
    }

    private void DetectPlayerPos()
    {
        Vector2Int playerpos = new Vector2Int((int)((player.transform.position.x + 1f) / 2), (int)((player.transform.position.y + 1f) / 2));
        LevelTile tilePlayerIsOnCurrently = map[playerpos.x][playerpos.y];
        if (tilePlayerIsOn == null)
        {
            tilePlayerIsOn = map[playerpos.x][playerpos.y];
        }
        if (tilePlayerIsOnCurrently != tilePlayerIsOn)
        {
            tilePlayerIsOn.parent = tilePlayerIsOnCurrently;
            tilePlayerIsOn = tilePlayerIsOnCurrently;
            tilePlayerIsOn.distanceToPlayer = 0;
            tilePlayerIsOn.parent = null;
            UpdateNavMesh();
        }
    }

    public void UpdateNavMesh()
    {
        List<LevelTile> Left2Explore = new List<LevelTile> { tilePlayerIsOn };
        List<LevelTile> explored = new List<LevelTile>();
        int insurancy = 100;
        while (Left2Explore.Count > 0 && tilePlayerIsOn != null && insurancy > 0)
        {
            LevelTile exploringTile = Left2Explore[0];
            foreach (LevelTile tile in Left2Explore)
            {
                if (tile.distanceToPlayer < exploringTile.distanceToPlayer)
                {
                    exploringTile = tile;
                }
            }
            Vector2Int pos = exploringTile.position;
            if (map[pos.x + 1][pos.y].empty && !explored.Contains(map[pos.x + 1][pos.y]))
            {
                map[pos.x + 1][pos.y].parent = exploringTile;
                map[pos.x + 1][pos.y].distanceToPlayer = exploringTile.distanceToPlayer + 1;
                Left2Explore.Add(map[pos.x + 1][pos.y]);

            }
            if (map[pos.x - 1][pos.y].empty && !explored.Contains(map[pos.x - 1][pos.y]))
            {
                map[pos.x - 1][pos.y].parent = exploringTile;
                map[pos.x - 1][pos.y].distanceToPlayer = exploringTile.distanceToPlayer + 1;
                Left2Explore.Add(map[pos.x - 1][pos.y]);

            }
            if (map[pos.x][pos.y - 1].empty && !explored.Contains(map[pos.x][pos.y - 1]))
            {
                map[pos.x][pos.y - 1].parent = exploringTile;
                map[pos.x][pos.y - 1].distanceToPlayer = exploringTile.distanceToPlayer + 1;
                Left2Explore.Add(map[pos.x][pos.y - 1]);

            }
            if (map[pos.x][pos.y + 1].empty && !explored.Contains(map[pos.x][pos.y + 1]))
            {
                map[pos.x][pos.y + 1].parent = exploringTile;
                map[pos.x][pos.y + 1].distanceToPlayer = exploringTile.distanceToPlayer + 1;
                Left2Explore.Add(map[pos.x][pos.y + 1]);
            }
            insurancy -= 1; 
            Left2Explore.Remove(exploringTile);
            explored.Add(exploringTile);
        }
    }

    private void CreateGround(LevelTile tile)
    {
        GameObject go = Instantiate(groundBlock, new Vector2(tile.position.x, tile.position.y) * 2, Quaternion.identity, transform);
        tile.empty = true;
        map[tile.position.x][tile.position.y].empty = true;
        Path path = go.GetComponent<Path>();
        path.levelCreator = this;
        path.tile = tile;
        path.UpdateSurrounding(tile.position);
        paths[tile.position] = path;
        SpawnSurrounding(tile);
    }

    private void SpawnSurrounding(LevelTile tile)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2Int pos = tile.position + new Vector2Int(i, j);
                if (pos.x < mapSize && pos.x >= 0 && pos.y < mapSize && pos.y >= 0)
                {
                    LevelTile otherTile = map[pos.x][pos.y];
                    if (!otherTile.empty && !otherTile.exists)
                    {
                        otherTile.exists = true;

                        GameObject go = Instantiate(block, new Vector2(pos.x, pos.y) * 2, Quaternion.identity, transform);
                        go.GetComponent<Diggable>().levelCreator = this;

                        otherTile.go = go;

                    }
                    else if (paths.ContainsKey(pos) && !(i == 0 && j == 0))
                    {
                        paths[pos].UpdateSurrounding(pos);
                    }
                }
            }
        }
    }

    public void DestroyBlock(Vector2 position)
    {
        Vector2Int index = new Vector2Int((int)(position.x + 0.1f), (int)(position.y + 0.1f));
        LevelTile currentTile = map[index.x][index.y];
        Destroy(currentTile.go);
        if (tier1Tiles.ContainsKey(currentTile.position))
        {
            tier1Tiles.Remove(currentTile.position);
            player.IncreaseOre(1, 0);
        }
        if (tier2Tiles.ContainsKey(currentTile.position))
        {
            tier2Tiles.Remove(currentTile.position);
            player.IncreaseOre(1, 1);
        }
        if (tier3Tiles.ContainsKey(currentTile.position))
        {
            tier3Tiles.Remove(currentTile.position);
            player.IncreaseOre(1, 2);
        }
        CreateGround(currentTile);
        UpdateNavMesh();
    }
}
