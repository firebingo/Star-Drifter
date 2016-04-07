using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private int currentWeapon = 1;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private Vector2 startingPosition;
    private float armor = 5;

    private WeaponHolder primary;  //Holds value for player's primary weapon.
    private WeaponHolder secondary; //Holds value for player's secondary weapon.

    private Vector2 position;
    private float rotation;
    public float timer;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject faceObject;

    private int spawn = 0; //Used to insure that the respawn code (OnEnable) doesn't run when player origanlly spawns. Also counts how many times the player spawns.

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<PlayerRespawn>().enabled = false;
        currentHealth = maxHealth;

        transform.position = startingPosition;
        position = transform.position;

        timer = 0;

        primary = new WeaponHolder();
        primary.initialize(50, 8, 50, 5, 8); // 8 is the player's layer
        secondary = new WeaponHolder();
        secondary.initialize(5, 10, 10, 0.50f, 8);

        ChangeWeapon(primary);
	}

    void OnEnable() //Used for Respawning
    {
        if (spawn != 0)
        {
            GetComponent<PlayerRespawn>().enabled = false;
            currentHealth = maxHealth;

            transform.position = startingPosition;
            position = transform.position;

            timer = 0;

            ChangeWeapon(primary);
            currentWeapon = 1;
        }
        spawn++;
    }

	void Update () 
    {
        FaceMouse();
        Shoot();
        PrepareChangeWeapon();
        Death();
	}

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Player Movement
    /// </summary>
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

    void ChangeWeapon(WeaponHolder weapon)
    {
        SendMessage("WeaponSwap", weapon);
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            foreach (MonoBehaviour c in GetComponents<MonoBehaviour>())
                c.enabled = false;
            GetComponent<PlayerRespawn>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            SendMessage("Respawn");
        }
    }

    void ApplyDamage(float damage)
    {
        float damageDealt = damage - armor;
        if (damageDealt <= 0)
            damageDealt = 1;
        currentHealth -= damageDealt;
    }

    void setStats()
    {
        speed = GetComponent<Leveling>().stats.speed;
        maxHealth = GetComponent<Leveling>().stats.life;
        armor = GetComponent<Leveling>().stats.armor;
    }
}
