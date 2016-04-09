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

    private int currentWeapon = 1;

    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float currentHealth;

    public float timer;

    private WeaponHolder primary;  //Holds value for the primary weapon.
    private WeaponHolder secondary; //Holds value for the secondary weapon.

    private GameObject player;

	// Use this for initialization
	void Start () 
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        primary = new WeaponHolder();
        primary.initialize(100, 12, 30, 5, 8); // 8 is the player's layer
        secondary = new WeaponHolder();
        secondary.initialize(25, 15, 5, 0.50f, 8);
        ChangeWeapon(primary);

	}
	
	// Update is called once per frame

    void Update()
    {
        switch (cases)
        {
            case 1:
                FaceMouse();
                ShipExit();
                Shoot();
                PrepareChangeWeapon();
                Death();
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

    void Shoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (currentWeapon == 1)
            {
                if (timer > primary.fireRate)
                {
                    SendMessage("Fire");
                    timer = 0;
                }
            }
            else
            {
                if (timer > secondary.fireRate)
                {
                    SendMessage("Fire");
                    timer = 0;
                }
            }
        }
        timer += 1 + Time.deltaTime;
    }

    void ChangeWeapon(WeaponHolder weapon)
    {
        SendMessage("WeaponSwap", weapon);
    }

    void ApplyDamage(float damage)
    {
        float damageDealt = damage;
        if (damageDealt <= 0)
            damageDealt = 1;
        currentHealth -= damageDealt;
    }

    void PrepareChangeWeapon()
    {
        if (Input.GetButtonDown("Swap") && currentWeapon == 1)
        {
            ChangeWeapon(secondary);
            currentWeapon = 2;
        }
        else if (Input.GetButtonDown("Swap") && currentWeapon == 2)
        {
            ChangeWeapon(primary);
            currentWeapon = 1;
        }
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            foreach (MonoBehaviour c in player.GetComponents<MonoBehaviour>())
                c.enabled = false;
            player.GetComponent<PlayerRespawn>().enabled = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.SendMessage("Respawn");
            Destroy(gameObject);
        }
    }
}
