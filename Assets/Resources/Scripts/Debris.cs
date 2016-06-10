using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour
{
    Sprite[] sprites;
    float timer;
    float time = 5;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        int sprite = Random.Range(7, 10);
        sprites = Resources.LoadAll<Sprite>("Textures/Particles01");
        GetComponent<SpriteRenderer>().sprite = sprites[sprite];
        transform.Rotate(0, 0, Random.Range(0.0f, 360.0f));
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            Destroy(this.gameObject);
        }
    }
}
