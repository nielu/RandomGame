using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public int Seed;
    public int OctaveCount;
    public float Persistence;

    public float MaxLevelLength;
    public float MinLevelLength;

    public GameObject StartingTerrain;
    public GameObject EndingTerrain;

    public GameObject[] TerrainBlocks;



    private Vector3 EndingPosition;
    private Vector3 ActualPosition;

    // Use this for initialization
    void Start()
    {
        var xPos = 0.0f;

        float[,] map = new float[16,16];

        for (int x = 0; x < 16; x++)
            for (int y = 0; y < 16; y++)
            {
                map[x, y] = Mathf.PerlinNoise(x * 1.01f, y * 1.01f);
                if (map[x, y] > .5f)
                    Instantiate(TerrainBlocks[0], new Vector3(x * 0.7f, y * 0.7f, 0), Quaternion.Euler(0, 0, 0));
            }



        //for (int i = 0; i < 100; i++)
        //{
        //    float h = Mathf.PerlinNoise(Time.time, 0f);
        //    Instantiate(TerrainBlocks[0], new Vector3(xPos, h, 0), Quaternion.Euler(0, 0, 0));
        //    xPos += 0.7f;
        //}

        //    if (Seed == 0)
        //    {
        //        Seed = System.DateTime.Now.GetHashCode();
        //    }

        //    Random.seed = Seed;

        //    ActualPosition = new Vector3(0, 0, 0);
        //    EndingPosition = new Vector3(Random.Range(MinLevelLength, MaxLevelLength), 0, 0);

        //    SpawnTerrain(StartingTerrain);

        //    while (ActualPosition.x < EndingPosition.x)
        //    {
        //        GameObject terrain = SelectTerrain();
        //        SpawnTerrain(terrain);
        //    }

        //    SpawnTerrain(EndingTerrain);

    }

    private GameObject SelectTerrain()
    {
        var r = Random.Range(0, TerrainBlocks.Length);
        return TerrainBlocks[r];
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnTerrain(GameObject terrain)
    {
        SpawnTerrain(terrain, ActualPosition);
    }

    void SpawnTerrain(GameObject terrain, Vector3 Position)
    {
        Instantiate(terrain, Position, Quaternion.Euler(0, 0, 0));
        ActualPosition = UpdatePosition(terrain, Position);
    }

    Vector3 UpdatePosition(GameObject terrain, Vector3 Position)
    {
        var t = terrain.GetComponent<Terrain>();
        return new Vector3(t.Length + Position.x, t.ExitHeight + Position.y);
    }
}
