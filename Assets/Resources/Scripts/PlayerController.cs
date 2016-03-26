using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private int shotTimer = 50;
    [SerializeField]
    private GameObject bullet;

    private Vector2 position;
    private float rotation;
    public float timer;

	void Start () 
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
        timer = 0;
	}

	void Update () 
    {
        Move();
        FaceMouse();
        Shoot();
	}

    /// <summary>
    /// Player Movement
    /// </summary>
    void Move()
    {
        if (Input.GetAxis("Horizontal") < 0)
            position.x -= speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        else if (Input.GetAxis("Horizontal") > 0)
            position.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;

        if (Input.GetAxis("Vertical") > 0)
            position.y += speed * Input.GetAxis("Vertical") * Time.deltaTime;
        else if (Input.GetAxis("Vertical") < 0)
            position.y -= speed * Input.GetAxis("Vertical") * Time.deltaTime;

        transform.position = position;
    }

    void FaceMouse() //Allows the player to rotate towards the mouse's position.
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Converts the mouses screen position to a position within the world space
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position); //Assumes sprite is facing up can be changed.
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire"))
        {
            if (timer > shotTimer)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                timer = 0;
            }
        }
        timer += 1 + Time.deltaTime;
    }
}
