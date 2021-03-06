﻿using UnityEngine;
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
    public Leveling Leveler { get; private set; }
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;
    public HUDManager HudManager;

    public Inventory playerInventory;
    public Guid primaryWeapon;  //Hold the id of the player's primary weapon so it can be accessed from the inventory.
    public Guid secondaryWeapon; //Hold the id of the player's secondary weapon so it can be accessed from the inventory.
    private Guid grenade; //Holds equipped grenade's id.
    public bool usingPrimary { get; private set; } //whether the player is using the primary or secondary weapon.

    private Vector2 position;
    private float rotation;
    public float timer;

    private int spawn = 0; //Used to insure that the respawn code (OnEnable) doesn't run when player origanlly spawns. Also counts how many times the player spawns.

    //Speed measure
    public Vector2 velocity;
    private Vector2 prevPos;


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

        Weapon tempPrimary = ScriptableObject.CreateInstance("Weapon") as Weapon;
		tempPrimary.Initialize(weaponLayers.Player);
        primaryWeapon = tempPrimary.itemId;
        Weapon tempSecondary = ScriptableObject.CreateInstance("Weapon") as Weapon;
		tempSecondary.Initialize(weaponLayers.Player);
        secondaryWeapon = tempSecondary.itemId;

        //Grenades
        Weapon tempGrenade = ScriptableObject.CreateInstance("Weapon") as Weapon;
		tempGrenade.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 20f, 10f, 1f, 4f, 1f, 4, 5, 1, weaponTypes.Grenade, weaponLayers.Player);
        grenade = tempGrenade.itemId;

        playerInventory.items.Add(tempPrimary.itemId, tempPrimary);
        playerInventory.items.Add(tempSecondary.itemId, tempSecondary);

        playerInventory.items.Add(tempGrenade.itemId, tempGrenade); //Adds starting grenade

        //secondary = new WeaponHolder();
        //secondary.initialize(5, 10, 0.5f, 0.50f, 8);

        ChangeWeapon(true);

        //speed measure
        prevPos.x = 0.0F;
        prevPos.y = 0.0F;

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
        UpdatePos();
        Shoot();
        Grenade();
        CheckChangeWeapon();
        if(HudManager)
        {
            if (Input.GetButtonDown("Inventory"))
                HudManager.inventoryParent.SetActive(!HudManager.inventoryParent.activeSelf);
        }
        Death();
    }

    void UpdatePos()
    {
        velocity.x = transform.position.x - prevPos.x;
        velocity.y = transform.position.y - prevPos.y;

        prevPos.x = transform.position.x;
        prevPos.y = transform.position.y;


    }

    void Shoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (GetComponent<Building>().building == false)
            {
                if (usingPrimary)
                {
                    var tempWeapon = playerInventory.items[primaryWeapon] as Weapon;
                    if (tempWeapon)
                        StartCoroutine(tempWeapon.Fire(this.transform));
                }
                else
                {
                    var tempWeapon = playerInventory.items[secondaryWeapon] as Weapon;
                    if (tempWeapon)
                        StartCoroutine(tempWeapon.Fire(this.transform));
                }
            }
        }
    }

    void Grenade()
    {
        if (Input.GetButton("Grenade"))
        {
            var tempGrenade = playerInventory.items[grenade] as Weapon;
            if (tempGrenade)
                StartCoroutine(tempGrenade.Fire(this.transform));
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
	
	public PlayerControllerSave savePlayer()
	{
		var saveObject = new PlayerControllerSave(maxHealth, currentHealth, startingPosition.x, startingPosition.y, armor, primaryWeapon.ToString(), secondaryWeapon.ToString(),
		grenade.ToString(), usingPrimary, position.x, position.y, rotation, timer, spawn);
		return saveObject;
	}
	
	public void loadPlayer(PlayerControllerSave saveFile)
	{
		this.maxHealth = saveFile.maxHealth;
		this.currentHealth = saveFile.currentHealth;
		this.startingPosition = new Vector2(saveFile.startingPositionX, saveFile.startingPositionY);
		this.armor = saveFile.armor;
		this.primaryWeapon = new Guid(saveFile.primaryWeapon);
		this.secondaryWeapon = new Guid(saveFile.secondaryWeapon);
		this.grenade = new Guid(saveFile.grenade);
		this.usingPrimary = saveFile.usingPrimary;
		this.transform.position = new Vector3(saveFile.positionX, saveFile.positionY);
		this.position = new Vector2(saveFile.positionX, saveFile.positionY);
		this.rotation = saveFile.rotation;
		this.timer = saveFile.timer;
		this.spawn = saveFile.spawn;
	}
}

[Serializable]
public class PlayerControllerSave
{
	public PlayerControllerSave(float mHealth, float cHealth, float startX, float startY, float armor, string primWeapon, string secWeapon, string grenade, bool uPrimary, float posX, float posY,
	float rot, float timer, int spawn)
	{
		this.maxHealth = mHealth;
		this.currentHealth = cHealth;
		this.startingPositionX = startX;
		this.startingPositionY = startY;
		this.armor = armor;
		this.primaryWeapon = primWeapon;
		this.secondaryWeapon = secWeapon;
		this.grenade = grenade;
		this.usingPrimary = uPrimary;
		this.positionX = posX;
		this.positionY = posY;
		this.rotation = rot;
		this.timer = timer;
		this.spawn = spawn;
	}
	
    public float maxHealth;
    public float currentHealth;
    public float startingPositionX;
	public float startingPositionY;
    public float armor;
	
    //public Inventory playerInventory;
    public string primaryWeapon;
    public string secondaryWeapon;
    public string grenade;
    public bool usingPrimary;

    public float positionX;
	public float positionY;
    public float rotation;
    public float timer;

    public int spawn;
}
