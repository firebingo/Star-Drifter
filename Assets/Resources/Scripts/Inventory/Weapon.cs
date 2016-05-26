//5-25; Allen: Added the SetRandStats function to randomize all the loot based on character level, loot type, and loot rarity.

using UnityEngine;
using System.Collections;
using System;

public enum weaponTypes
{
    Pistol  = 0,
    SMG     = 1,
    Rifle   = 2,
    Rocket  = 3
}

//Rarity based on percentage
public enum weaponRarity
{
    Common      = 100,
    Uncommon    = 45,
    Rare        = 15,
    Legendary   = 3
}


//used for determing what collision layer bullets from this weapon should be on.
public enum weaponLayers
{
    Player = 8,
    Enemy = 9
}

public class Weapon : ScriptableObject, inventoryItem
{
    public itemType inventoryItemType { get { return itemType.Weapon; } }

    [SerializeField]
    private GameObject Bullet;

    public bulletTypes bulletType = bulletTypes.Basic;

    [SerializeField]
    public float bulletSpeed { get; private set; }

    //Accuracy of the weapon, determines the spread
    [SerializeField]
    public float bulletSpread { get; private set; }


    [SerializeField]
    public float bulletTime { get; private set; } //How long the bullet lasts

    [SerializeField]
    public float Damage { get; private set; }

    public float shotTimer { get; private set; } //Fire rate

    [SerializeField]
    public weaponTypes Type { get; private set; }

    [SerializeField]
    public weaponRarity Rarity { get; private set; }

    [SerializeField]
    private weaponLayers Layer;

    [SerializeField]
    public float Power { get; private set; }

    private float Timer;

    public Guid itemId { get; private set; }  //a unique id for the weapon.

    public int count { get; set; }

    public string itemName { get; private set; }

    //The player for reference of the current level
    [SerializeField]
    private PlayerController player;

    public void updateItem()
    {
        if (Timer < shotTimer)
            Timer += Time.deltaTime;
    }

    public void Fire(Transform initTransform)
    {
        if (Timer >= shotTimer)
        {
            GameObject temp = Instantiate(Bullet, initTransform.position, initTransform.rotation) as GameObject;
            temp.GetComponent<BulletController>().Initialize(bulletType, bulletSpeed, bulletTime, Damage + Power, Layer);
            Timer = 0f;
        }
    }

    public void Initialize(GameObject Bullet, float bulletDamage, float speed, float shotTime, float bulletDecay, weaponTypes weaponType, weaponLayers layerType) //Allows the creation of weapon types
    {
       // this.setRandStats();

        this.Bullet = Bullet;
        this.bulletType = Bullet.GetComponent<BulletController>().bulletType;
        this.Damage = bulletDamage;
        this.bulletSpeed = speed;
        this.shotTimer = shotTime;
        this.bulletTime = bulletDecay;
        this.Type = weaponType;
        this.Layer = layerType;
        this.itemId = Guid.NewGuid();
    }

    void setStats(float iPower)
    {
        Power = iPower;
    }

    /// <summary>
    /// Sets the weapon stats based on character level and rarity level
    /// </summary>
   /* public void setRandStats()
    {
        //Random percentage
        int Rand = UnityEngine.Random.Range(1, 100);

        //Boost from rarity
        float Boost = 0.0F;
        //Boost from
        float[] TypeBoost = { 1.0F, 1.0F, 1.0F, 1.0F, 1.0F }; //Damage, bullet speed, rate of fire, bullet decay, bullet spread

        //Set up the chances
        int[] RareValues = { (int)weaponRarity.Legendary, (int)weaponRarity.Rare, (int)weaponRarity.Uncommon, (int)weaponRarity.Common };
     
        //Find matching rarity
        if (Rand > RareValues[2])
        {
            Rarity = weaponRarity.Common;
            Boost = 1.0F;
        }
        else if (Rand > RareValues[1])
        {
            Rarity = weaponRarity.Uncommon;
            Boost = 1.5F;
        }
        else if (Rand > RareValues[0])
        {
            Rarity = weaponRarity.Rare;
            Boost = 2.5F;
        }
        else
        {
            Rarity = weaponRarity.Legendary;
            Boost = 4.0F;
        }

        //Determine boost by type
        Rand = UnityEngine.Random.Range(0, 3);

        switch (Rand)
        {
            case 0:
                Type = weaponTypes.Pistol;
                TypeBoost[0] = 0.6F;
                TypeBoost[2] = 0.4F;
                TypeBoost[4] = 1.4F;
                break;
            case 1:
                Type = weaponTypes.SMG;
                TypeBoost[0] = 0.8F;
                TypeBoost[2] = 2.0F;
                TypeBoost[4] = 1.5F;
                break;

            case 2:
                Type = weaponTypes.Rifle;
                TypeBoost[0] = 1.2F;
                TypeBoost[2] = 2.2F;
                TypeBoost[4] = 1.6F;
                break;

            case 3:
                Type = weaponTypes.Rocket;
                TypeBoost[0] = 10.0F;
                TypeBoost[1] = 0.8F;
                TypeBoost[2] = 0.3F;
                TypeBoost[4] = 1.2F;
                break;
            
        }

        //Player level
        int PlayerLevel = player.Leveler.level;

        //Set the Min and Maxes
        //int Min = 

    }*/
}