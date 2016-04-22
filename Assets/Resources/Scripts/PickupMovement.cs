using UnityEngine;
using System.Collections;

/// <summary>
/// The resource movement class is designed to simply rotate 
/// a resource clockwise slowly, until the lifespan of the 
/// resource is depleated.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class ResourceMovement : MonoBehaviour
{
    public float rotSpeed = 2;

    private Rigidbody2D rb;

   
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    /// <summary>
    /// Rotates the sprite based on rotSpeed
    /// </summary>
    void Rotate() 
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
       }

}
