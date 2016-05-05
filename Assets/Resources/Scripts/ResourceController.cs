using UnityEngine;
using System.Collections;

/// <summary>
/// The resource manager, a single script to control 
/// every type of resource pickup: Money, ammo, resources
/// </summary>
/// 
[RequireComponent(typeof(ResourceMovement))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ResourceController : MonoBehaviour
{
    //Basic Variables
    private PlayerMovement Movement;
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;

    // Unity's start funtion
    void Start()
    {
        Movement = this.GetComponent<PlayerMovement>();
        Collider = this.GetComponent<BoxCollider2D>();
        Renderer = this.GetComponent<SpriteRenderer>();

        Movement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// A function to destry an item and add it to inventory 
    /// of the character that collided. 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI"))
        {
            Destroy(gameObject);

            //Add item to inventory
        }
    }
}