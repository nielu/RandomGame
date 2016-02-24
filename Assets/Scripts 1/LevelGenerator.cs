using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public int Seed;
    public float MaxLevelLength;
    public float MinLevelLength;

    public GameObject StartingTerrain;
    public GameObject EndingTerrain;

    public GameObject[] TerrainBlocks;



    private Vector3 EndingPosition;
    private Vector3 ActualPosition;

	// Use this for initialization
	void Start () 
    {
        if (Seed == 0)
        {
            Seed = System.DateTime.Now.GetHashCode();
        }

        Random.seed = Seed;

        ActualPosition = new Vector3(0, 0, 0);
        EndingPosition = new Vector3(Random.Range(MinLevelLength, MaxLevelLength), 0, 0);

        SpawnTerrain(StartingTerrain);

        while (ActualPosition.x < EndingPosition.x)
        {
            GameObject terrain = SelectTerrain();
            SpawnTerrain(terrain);
        }

        SpawnTerrain(EndingTerrain);

	}

    private GameObject SelectTerrain()
    {
        var r = Random.Range(0, TerrainBlocks.Length);
        return TerrainBlocks[r];
    }
	
	// Update is called once per frame
	void Update ()
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
