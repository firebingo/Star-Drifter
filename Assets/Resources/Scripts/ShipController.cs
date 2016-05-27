//4-20; Allen Changed BoxCollider to Circle Collider

using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ShipController : MonoBehaviour
{
    [SerializeField]
    private Vector2 startingPosition;

    [SerializeField]
    private int cases = 0;

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

    private Inventory shipInventory;
    private Guid primaryWeapon;  //Hold the id of the player's primary weapon so it can be accessed from the inventory.
    private Guid secondaryWeapon; //Hold the id of the player's secondary weapon so it can be accessed from the inventory.
    bool usingPrimary; //whether the player is using the primary or secondary weapon.

    private GameObject player;

    private PlayerMovement Movement;
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;

    // Use this for initialization
    void Start()
    {
        Movement = this.GetComponent<PlayerMovement>();
        Collider = this.GetComponent<BoxCollider2D>();
        Renderer = this.GetComponent<SpriteRenderer>();

        Movement.enabled = false;

        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

        Weapon tempPrimary = new Weapon();
        tempPrimary.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 100f, 12f, 30f, 5f, 6f, 32, weaponTypes.Pistol, weaponLayers.Player);
        primaryWeapon = tempPrimary.itemId;
        Weapon tempSecondary = new Weapon();
        tempPrimary.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 25f, 15f, 5f, 0.5f, 10f, 32, weaponTypes.Pistol, weaponLayers.Player);
        secondaryWeapon = tempSecondary.itemId;

        ChangeWeapon(true);
    }

    // Update is called once per frame

    void Update()
    {
        switch (cases)
        {
            case 1:
                ShipExit();
                Shoot();
                CheckChangeWeapon();
                Death();
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        switch (cases)
        {
            case 1:
                Sync();
                break;
            default:
                break;
        }
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
            Movement.enabled = true;
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
            Movement.enabled = false;
        }
    }

    void Shoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (usingPrimary)
            {
                var tempWeapon = shipInventory.items[primaryWeapon] as Weapon;
                if (tempWeapon)
                    tempWeapon.Fire(this.transform);
            }
            else
            {
                var tempWeapon = shipInventory.items[secondaryWeapon] as Weapon;
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

    void ApplyDamage(float damage)
    {
        float damageDealt = damage;
        if (damageDealt <= 0)
            damageDealt = 1;
        currentHealth -= damageDealt;
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
