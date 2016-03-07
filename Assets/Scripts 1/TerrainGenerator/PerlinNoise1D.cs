
using UnityEngine;

class PerlinNoise1D
{

    public static float[] PerlinFractalDataArray1D(int x, Vector2 Position, int seed, int octaves = 8, float frequency = 100f, float amplitude = 0.5f)
    {
        return PerlinFractalDataArray2D(1, x, Position, seed, octaves, frequency, amplitude)[0];
    }

    public static float[][] PerlinFractalDataArray2D(int X, int Y, Vector2 Position, int seed, int octaves = 8, float frequency = 100f, float amplitude = 0.5f)
    {
        float[][] returnArray = new float[X][];
        for (int i = 0; i < X; i++)
            returnArray[i] = new float[Y];

        Random.seed = seed;

        var noise = 0f;
        var gain = 1.0f;

        Vector2 sample = new Vector2();

        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                noise = 0f;
                gain = 1f;

                for (int i = 0; i < octaves; i++)
                {
                    sample.x = Position.x + x * (gain / frequency);
                    sample.y = Position.y + y * (gain / frequency);

                    noise += Mathf.PerlinNoise(sample.x, sample.y) * (amplitude / gain);
                    gain *= 2.0f;
                }

                returnArray[x][y] = noise;

            }
        }

        return returnArray;
    }


    public float[] GetHeightMap(float step, int size, int octaveCount = 6, float persistence = 0.25f, int seed = 0)
    {
        if (seed != 0)
            Random.seed = seed;

        float[] map = new float[size];
        float xValue = 0f, frequency, amplitude;


        for (int x = 0; x < size; x++)
        {
            for (int i = 0; i < octaveCount; i++)
            {
                frequency = Mathf.Pow(2f, i);
                amplitude = Mathf.Pow(persistence, i);
                map[x] += Mathf.Abs(InterpolatedNoise((xValue * frequency) * amplitude, i));
            }

            xValue += step;


        }

        return map;
    }


    #region private gizmos

    int[,] FrequencyConstants = new int[,]
  {
   {76110271, 52540511, 58479247, 19719523}, //0
   {58903567, 44545867, 97724351, 11014337}, //1
   {95527463, 11832889, 42657833, 46083953}, //2
   {77732243, 84008501, 90387379, 99237839}, //4
   {27880309, 13004851, 19782041, 52999277}, //8
   {76068331, 54706733, 16166879, 37400413}, //16
   {93003259, 12093661, 45989353, 84157771}, //32
   {32836619, 80143639, 75566947, 50322581}, //64
   {68649947, 29840071, 56690693, 18072403}, //128
   {92121413, 67043341, 53373751, 44614249}, //256
   {12185419, 95852989, 41161069, 16871537}, //512
   {59025347, 56526941, 31538137, 47065453}, //1024
   {23879389, 70746817, 26248931, 13557217}, //2048
   {32715091, 16204561, 38084737, 72414983}, //4096
   {75376069, 82822331, 25016093, 66703993} //?
  };

    float Noise(int x, int f)
    {

        int ret = ((int)(x) << 13) ^ x;
        return (1.0f - (((ret * ret * FrequencyConstants[f, 0] + FrequencyConstants[f, 1]) + FrequencyConstants[f, 2]) & 0x7fffffff) / (float)FrequencyConstants[f, 3]);
    }

    float Noise(int x)
    {
        Random.seed = x;
        return Random.Range(-10.0f, 10.0f);
    }

    float SmoothedNoise(float x, int f)
    {
        return Noise((int)x, f) / 2.0f + Noise((int)x - 1, f) / 4.0f + Noise((int)x + 1, f) / 4.0f;
    }

    float CosineInterpolation(float a, float b, float x)
    {
        float ft = x * Mathf.PI;
        float f = (1 - Mathf.Cos(ft)) * .5f;
        return a * (1 - f) + b * f;
    }

    float LinearInterpolation(float a, float b, float x)
    {
        return a * (1 - x) + b * x;
    }


    float InterpolatedNoise(float x, int f)
    {
        int xInt = (int)x;
        float xFrac = x - xInt;

        float v1 = SmoothedNoise(xInt, f);
        float v2 = SmoothedNoise(xInt + 1, f);

        return CosineInterpolation(v1, v2, xFrac);

    }
    #endregion

}

