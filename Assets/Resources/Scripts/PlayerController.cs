using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 5;

    private Vector2 position;
    private float rotation;
    private Vector3 mousePosition;

	void Start () 
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
	}

	void Update () 
    {
        Move();
        FaceMouse();
	}

    void Move() //Allows the player to move using wasd, speed stat effects the speed.
    {
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            position.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            position.y -= speed * Time.deltaTime;
        }

        transform.position = position;
    }

    void FaceMouse() //Allows the player to rotate towards the mouse's position.
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Converts the mouses screen position to a position within the world space
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position); //Assumes sprite is facing up can be changed.
    }
}
