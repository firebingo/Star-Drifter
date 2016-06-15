using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour 
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 5;
   
    [SerializeField]
    private GameObject faceObject;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
    }

    void FixedUpdate()
    {
        Move();
    }

    void FaceMouse() //Allows the player to rotate towards the mouse's position.
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, faceObject.transform.position - transform.position); //Assumes sprite is facing up can be changed.
    }

    void Move()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 100);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 100);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(Vector2.up * speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime * 100);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rb.AddForce(Vector2.up * speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime * 100);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        collider.gameObject.SendMessage("ApplyDamage", rb.velocity.magnitude*5);
    }

    
}
