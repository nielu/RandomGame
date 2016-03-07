using UnityEngine;
using Random = UnityEngine.Random;

class TerrainChunk : MonoBehaviour
{
    public int X_Size = 16;
    public int Y_Size = 16;
    public float Ground_Y = 3.0f;
    public GameObject Ground;
    public GameObject Dirt;

    public float Frequency = 100f;
    public int Octaves = 8;
    public float Amplitude = 0.5f;

    private int seed;

    private MapTileEnum[][] map;

    private enum MapTileEnum
    {
        Empty = 0,
        Sky,
        Ground, 
        Dirt
    }

    private enum BlockSideEnum
    {
        GroundClear = 0, 
        GroundLeft = 256,
        GroundLeftTop = 2,
        GroundTop = 4,
        GroundRightTop = 8,
        GroundRight = 16,
        GroundRightBottom = 32,
        GroundBottom = 64,
        GroundLeftBottom = 128
    }
    

    public void Start()
    {
        generateMap();
    }

    public void Update()
    {

    }

    public void Destroy()
    {

    }

    private void generateMap()
    {
        var src = PerlinNoise1D.PerlinFractalDataArray1D(X_Size, Vector2.zero, Random.Range(0, int.MaxValue), Octaves, Frequency, Amplitude);

        var parent = Instantiate(new GameObject("Chunk"));
        

        for (int x = 0; x < X_Size; x++)
        {
            float y = roundTo(src[x] * 16f, 0.7f);
            float x_f = x * 0.7f;
            
            var o = (GameObject)Instantiate(Ground, new Vector3(x_f, y - 5f), Quaternion.Euler(0, 0, 0));
            o.transform.parent = parent.transform;
            while (y >= 0f)
            {
                y -= 0.7f;
                var g = (GameObject)Instantiate(Dirt, new Vector3(x_f, y - 5f), Quaternion.Euler(0, 0, 0));
                g.transform.parent = o.transform;
            }
        }

    }

    private int getGroundSides(int x, int y)
    {
        int blockType = 0;

        if (y > 0 && isGroundTile(x, y - 1))
            blockType += (int)BlockSideEnum.GroundBottom;
        if (y < Y_Size - 1 && isGroundTile(x, y + 1))
            blockType += (int)BlockSideEnum.GroundTop;

        if (x > 0) // left side
        {
            if (isGroundTile(x - 1, y))
                blockType += (int)BlockSideEnum.GroundLeft;

            if (y > 0) //left bottom
            {
                if (isGroundTile(x - 1, y - 1))
                    blockType += (int)BlockSideEnum.GroundLeftBottom;
            }

            if (y < Y_Size - 1) //left top
            {
                if (isGroundTile(x - 1, y + 1))
                    blockType += (int)BlockSideEnum.GroundLeftTop;
            }
        } 
        if (x < X_Size - 1) // right side
        {
            if (isGroundTile(x + 1, y))
                blockType += (int)BlockSideEnum.GroundRight;

            if (y > 0) //left bottom
            {
                if (isGroundTile(x + 1, y - 1))
                    blockType += (int)BlockSideEnum.GroundRightBottom;
            }

            if (y < Y_Size - 1) //left top
            {
                if (isGroundTile(x + 1, y + 1))
                    blockType += (int)BlockSideEnum.GroundRightTop;
            }
        }
        

        return blockType;
    }

    private bool isGroundTile(int x, int y)
    {
        return map[x][y] == MapTileEnum.Ground;
    }

    private float roundTo(float x, float multiple)
    {
        if (multiple == 0)
            return x;

        float r = x % multiple;
        if (Mathf.Approximately(r, 0f))
            return x;

        return x + multiple - r;
    }

    

}
