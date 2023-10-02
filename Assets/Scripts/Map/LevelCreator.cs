using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject indestructibleBlock;
    [SerializeField] private GameObject groundBlock;
    [SerializeField] public int mapSize = 100;
    [SerializeField] private int numberOfTier1Tiles = 1000;
    [SerializeField] private int numberOfTier2Tiles = 400;
    [SerializeField] private int numberOfTier3Tiles = 100;

    [HideInInspector] public LevelTile[][] map;

    //private List<Tile> emptyTiles = new List<Tile>();
    [HideInInspector] public Dictionary<Vector2Int, Path> paths = new Dictionary<Vector2Int, Path>();
    [HideInInspector] public Dictionary<Vector2Int, LevelTile> tier1Tiles = new Dictionary<Vector2Int, LevelTile>();
    [HideInInspector] public Dictionary<Vector2Int, LevelTile> tier2Tiles = new Dictionary<Vector2Int, LevelTile>();
    [HideInInspector] public Dictionary<Vector2Int, LevelTile> tier3Tiles = new Dictionary<Vector2Int, LevelTile>();

    [SerializeField] private PlayerController player;

    [HideInInspector] public LevelTile tilePlayerIsOn;
    private bool gamestarted = false;

    [SerializeField]  private List<Zone> zones = new List<Zone>();
    private Vector2Int[] zonesPlacements;

    public enum RessourcesType
    {
        Dirt,
        Tier1,
        Tier2,
        Tier3,
        Indestructible

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


        for (int i = 0; i < mapSize; i++)
        {
            map[i][0].ressourceType = RessourcesType.Indestructible;
            map[i][mapSize-1].ressourceType = RessourcesType.Indestructible;
            map[0][i].ressourceType = RessourcesType.Indestructible;
            map[mapSize-1][i].ressourceType = RessourcesType.Indestructible;
        }

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

        CreateGround(map[50][48]);
        DetectPlayerPos();
        DestroyBlock(new Vector2Int(50, 49));
        DestroyBlock(new Vector2Int(50, 50));
        DestroyBlock(new Vector2Int(49, 49));
        DestroyBlock(new Vector2Int(49, 50));
        DestroyBlock(new Vector2Int(51, 50));

        CreateZones();

        gamestarted = true;

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
                    if (!otherTile.empty && !otherTile.exists && otherTile.ressourceType != RessourcesType.Indestructible)
                    {
                        otherTile.exists = true;

                        GameObject go = Instantiate(block, new Vector2(pos.x, pos.y) * 2, Quaternion.identity, transform);
                        go.GetComponent<Diggable>().levelCreator = this;

                        otherTile.go = go;

                    }
                    else if(otherTile.ressourceType == RessourcesType.Indestructible)
                    {
                        otherTile.exists = true;

                        GameObject go = Instantiate(indestructibleBlock, new Vector2(pos.x, pos.y) * 2, Quaternion.identity, transform);
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
            if(gamestarted) player.IncreaseOre(1, 0);
        }
        if (tier2Tiles.ContainsKey(currentTile.position))
        {
            tier2Tiles.Remove(currentTile.position);
            if (gamestarted) player.IncreaseOre(1, 1);
        }
        if (tier3Tiles.ContainsKey(currentTile.position))
        {
            tier3Tiles.Remove(currentTile.position);
            if (gamestarted) player.IncreaseOre(1, 2);
        }
        
        CreateGround(currentTile);
        UpdateNavMesh();
    }

    private void CreateZones()
    {
        zonesPlacements = new Vector2Int[] {
        new Vector2Int(20, 20),
        new Vector2Int(20, 85),
        new Vector2Int(61, 82),
        new Vector2Int(89, 45),
        new Vector2Int(67, 24),
        new Vector2Int(55, 11),
        new Vector2Int(91, 85),
        new Vector2Int(80, 70),
        new Vector2Int(11, 31),
        new Vector2Int(44, 88),
        new Vector2Int(12, 62),
        new Vector2Int(39, 36),
        new Vector2Int(8, 9),
        };
        foreach (var p in zonesPlacements)
        {
            if(zones.Count <= 0)
            {
                return;
            }
            int indice = Random.Range(0, zones.Count);
            foreach (var z in zones[indice].emptyTiles)
            {
                DestroyBlock(z + p);
            }
            for(int i = 0; i < zones[indice].specialObjectsInstances.Length; i++)
            {
                Vector2 pos = new Vector2(zones[indice].specialObjectsLocations[i].x + p.x, zones[indice].specialObjectsLocations[i].y + p.y);
                Instantiate(zones[indice].specialObjectsInstances[i], pos*2, Quaternion.Euler(0, 0, Random.Range(0,360)), transform);
            }
            zones.RemoveAt(indice);
        }
    }
}
