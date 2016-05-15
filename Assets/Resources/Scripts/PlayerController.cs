using UnityEngine;
using System;

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

    [SerializeField]
    public float maxHealth { get; private set; }
    [SerializeField]
    public float currentHealth { get; private set; }

    [SerializeField]
    private Vector2 startingPosition;
    private float armor = 5;

    private PlayerMovement Movement;
    private PlayerRespawn Respawn;
    private Leveling Leveler;
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;

    public Inventory playerInventory;
    private Guid primaryWeapon;  //Hold the id of the player's primary weapon so it can be accessed from the inventory.
    private Guid secondaryWeapon; //Hold the id of the player's secondary weapon so it can be accessed from the inventory.
    bool usingPrimary; //whether the player is using the primary or secondary weapon.

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
        playerInventory = this.GetComponent<Inventory>();

        Respawn.enabled = false;
        currentHealth = maxHealth;

        transform.position = startingPosition;
        position = transform.position;

        timer = 0;

        maxHealth = 100;

        Weapon tempPrimary = new Weapon();
        tempPrimary.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 50f, 8f, 1f, 5f, weaponTypes.Pistol, weaponLayers.Player);
        primaryWeapon = tempPrimary.itemId;
        Weapon tempSecondary = new Weapon();
        tempSecondary.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 5f, 10f, 0.5f, 0.5f, weaponTypes.Pistol, weaponLayers.Player);
        secondaryWeapon = tempSecondary.itemId;

        playerInventory.items.Add(tempPrimary.itemId, tempPrimary);
        playerInventory.items.Add(tempSecondary.itemId, tempSecondary);

        //secondary = new WeaponHolder();
        //secondary.initialize(5, 10, 0.5f, 0.50f, 8);

        ChangeWeapon(true);
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

            ChangeWeapon(true);
            currentWeapon = 1;
        }
        spawn++;
    }

    void Update()
    {
        Shoot();
        CheckChangeWeapon();
        Death();
    }

    void Shoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (usingPrimary)
            {
                var tempWeapon = playerInventory.items[primaryWeapon] as Weapon;
                if (tempWeapon)
                    tempWeapon.Fire(this.transform);
            }
            else
            {
                var tempWeapon = playerInventory.items[secondaryWeapon] as Weapon;
                if (tempWeapon)
                    tempWeapon.Fire(this.transform);
            }
        }
    }

    void CheckChangeWeapon()
    {
        if (Input.GetButtonDown("Swap") && usingPrimary)
            ChangeWeapon(false);
        else if (Input.GetButtonDown("Swap") && !usingPrimary)
            ChangeWeapon(true);
    }

    void ChangeWeapon(bool usingPrimary)
    {
        this.usingPrimary = usingPrimary;
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
