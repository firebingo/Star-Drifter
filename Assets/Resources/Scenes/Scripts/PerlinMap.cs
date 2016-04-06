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

    /// <summary>
    /// Unity's Start Function
    /// </summary>
    void Start()
    {
        texture = new Texture2D(width, height);
        perlin = new Color[texture.width * texture.height];
        renderPerlin();
    }

    /// <summary>
    /// Samples unity's perlin map and generates a texture from it.
    /// </summary>
    public void renderPerlin()
    {
        int y = 0;
        //The whiles loops run through each pixel on the image.
        while (y < texture.height)
        {
            int x = 0;
            while(x < texture.width)
            {
                //get the x and y coord to sample from the perlin noise.
                float xco = (float)x / texture.width * scale;
                float yco = (float)y / texture.height * scale;
                //get the perlin sample from the coordinates + the seed offset.
                float sample = Mathf.PerlinNoise(xco + seed, yco + seed);
                //set the pixel in the color array at the current x and y to be the sample.
                perlin[y * texture.width + x] = new Color(sample, sample, sample);
                ++x;
            }
            ++y;
        }
        //set the texture with the created color array
        texture.SetPixels(perlin);
        texture.Apply();
    }
}
