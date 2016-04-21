using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRespawn))]
[RequireComponent(typeof(Leveling))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int currentWeapon = 1;

    public float maxHealth = 100;
    public float currentHealth;

    [SerializeField]
    private Vector2 startingPosition;
    private float armor = 5;

    private PlayerMovement Movement;
    private PlayerRespawn Respawn;
    private Leveling Leveler;
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;
    private Weapon weapon;

    private WeaponHolder primary;  //Holds value for player's primary weapon.
    private WeaponHolder secondary; //Holds value for player's secondary weapon.

    private Vector2 position;
    private float rotation;
    public float timer;

    private int spawn = 0; //Used to insure that the respawn code (OnEnable) doesn't run when player origanlly spawns. Also counts how many times the player spawns.

    void Start()
    {
        Movement = this.GetComponent<PlayerMovement>();
        Respawn = this.GetComponent<PlayerRespawn>();
        Leveler = this.GetComponent<Leveling>();
        Collider = this.GetComponent<BoxCollider2D>();
        Renderer = this.GetComponent<SpriteRenderer>();
        weapon = this.GetComponent<Weapon>();

        Respawn.enabled = false;
        currentHealth = maxHealth;

        transform.position = startingPosition;
        position = transform.position;

        timer = 0;

        primary = new WeaponHolder();
        primary.initialize(50, 8, 1, 5, 8); // 8 is the player's layer
        secondary = new WeaponHolder();
        secondary.initialize(5, 10, 0.5f, 0.50f, 8);

        ChangeWeapon(primary);
    }

    void OnEnable() //Used for Respawning
    {
        if (spawn != 0 && Respawn.enabled == true)
        {
            Respawn.enabled = false;
            currentHealth = maxHealth;

            transform.position = startingPosition;
            position = transform.position;

            timer = 0;

            ChangeWeapon(primary);
            currentWeapon = 1;
        }
        spawn++;
    }

    void Update()
    {
        Shoot();
        PrepareChangeWeapon();
        Death();
    }

    void Shoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (currentWeapon == 1)
            {
                if (timer > primary.fireRate)
                {
                    weapon.Fire();
                    timer = 0;
                }
            }
            else
            {
                if (timer > secondary.fireRate)
                {
                    weapon.Fire();
                    timer = 0;
                }
            }
        }
        timer += Time.deltaTime;
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
            Respawn.enabled = true;
            Collider.enabled = false;
            Renderer.enabled = false;
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
        Movement.speed = Leveler.stats.speed;
        maxHealth = Leveler.stats.life;
        armor = Leveler.stats.armor;
    }
}
