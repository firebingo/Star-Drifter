using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private Vector2 startingPosition;

    private Rigidbody2D rb;

    [SerializeField]
    private int cases = 0;

    [SerializeField]
    private GameObject faceObject;

    private GameObject target;

    private bool playerInRange = false;

    [SerializeField]
    private float cooldown = 3;
    
    [SerializeField]
    private float time = 0;

	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame

    void Update()
    {
        switch (cases)
        {
            case 1:
                FaceMouse();
                ShipExit();
                break;
            default:
                break;
        }
    }

	void FixedUpdate () 
    {
        switch (cases)
        {
            case 1:
                Move();
                Sync();
                break;
            default:
                break;
        }
	}

    void Move()
    {
        if (Input.GetAxis("Horizontal") < 0)
            rb.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 100);
        else if (Input.GetAxis("Horizontal") > 0)
            rb.AddForce(Vector2.right * speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 100);

        if (Input.GetAxis("Vertical") > 0)
            rb.AddForce(Vector2.up * speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime * 100);
        else if (Input.GetAxis("Vertical") < 0)
            rb.AddForce(Vector2.up * speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime * 100);
    }

    void FaceMouse() //Allows the player to rotate towards the mouse's position.
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, faceObject.transform.position - transform.position); //Assumes sprite is facing up can be changed.
    }

    void Sync()
    {
        target.transform.position = transform.position;
    }

    public void ShipEnter(GameObject tar)
    {
        if (cases != 1)
        {
            time = 0;
            target = tar;
            foreach (MonoBehaviour c in tar.GetComponents<MonoBehaviour>())
                c.enabled = false;
            tar.GetComponent<SpriteRenderer>().enabled = false;
            cases = 1;
        }
    }

    public void ShipExit()
    {
        time += Time.deltaTime;
        if (Input.GetButton("Enter Ship") && time >= cooldown) //e to enter/leave ship
        {
            GetComponentInChildren<ShipRange>().time = 0;
            foreach (MonoBehaviour c in target.GetComponents<MonoBehaviour>())
                c.enabled = true;
            target.GetComponent<SpriteRenderer>().enabled = true;
            cases = 0;
        }
    }
}
