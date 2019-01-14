using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour {

    public GameObject player;
    public int refineValue;

    [Range(0, 100)]
    public int iniChance;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;
    [Range(1, 10)]
    public int numR;
    private int count = 0;

    private int[,] terrainMap;
    public Vector3Int tmpSize;
    public Tilemap topMap;
    public Tilemap botMap;
    public TerrainTile topTile;
    public Tile botTile;

    int width;
    int height;

    private PlayerSpawner playerSpawner;

    private void Awake()
    {
        width = tmpSize.x;
        height = tmpSize.y;
        //say what
    }

    private void Start()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();

        for (int x = 0; x < refineValue; x++)
        {
            DoSim(numR);
        }
    }

    public void DoSim(int nu)
    {
        ClearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            InitPos();
            
        }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = GenTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
            }
        }

        GeneratePlayer();
    }

    public Vector2Int GetSpawnPoint() {
        int value = 0, spawnWidth, spawnHeight;
        while (value != 1) {
            spawnWidth = UnityEngine.Random.Range(0, width);
            spawnHeight = UnityEngine.Random.Range(0, height);
            Debug.Log(spawnWidth + "  " + spawnHeight);
            value = terrainMap[spawnWidth, spawnHeight];
            if (value == 1) {
                return new Vector2Int(-spawnWidth + width / 2, -spawnHeight + height / 2);
            }
        }
        Debug.Log("No Land Found");
        return new Vector2Int(0,0);
    }

    public void InitPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = UnityEngine.Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }
        //playerSpawner.SpawnPlayer();
    }


    public int[,] GenTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);
        //BoundsInt myB = new BoundsInt(-2, -2, 0, 2, 2, 1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 1;

                    else
                    {
                        newMap[x, y] = 0;

                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 0;

                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

            }

        }



        return newMap;
    }

    // runs evey frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            for (int x = 0; x < refineValue; x++)
            {
                DoSim(numR);
                
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            ClearMap(true);
        }



        if (Input.GetMouseButton(2))
        {
            SaveAssetMap();
            count++;
        }








    }


    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath, mf))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
            }


        }


    }

    public void ClearMap(bool complete)
    {

        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }

    public void GeneratePlayer()
    {
        Vector2Int spawnValue = GetSpawnPoint();
        Debug.Log("spawning player");
        Instantiate(player, new Vector3Int(spawnValue.x, spawnValue.y, 0), Quaternion.identity);
    }

}
