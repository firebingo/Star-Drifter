using UnityEngine;
using System.Collections;

public class PerlinMap : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private float scale;

    public int seed;
    public Texture2D texture { get; private set; }
    private Color[] perlin;

    // Use this for initialization
    void Start()
    {
        texture = new Texture2D(width, height);
        perlin = new Color[texture.width * texture.height];
        renderPerlin();

    }

    public void renderPerlin()
    {
        int y = 0;
        while (y < texture.height)
        {
            int x = 0;
            while(x < texture.width)
            {
                float xco = (float)x / texture.width * scale;
                float yco = (float)y / texture.height * scale;
                float sample = Mathf.PerlinNoise(xco + seed, yco + seed);
                perlin[y * texture.width + x] = new Color(sample, sample, sample);
                ++x;
            }
            ++y;
        }
        texture.SetPixels(perlin);
        texture.Apply();
    }
}
