//5-25; Allen: Added the SetRandStats function to randomize all the loot based on character level, loot type, and loot rarity.
//5-27; Allen: Completed the setRandStats function

using UnityEngine;
using System.Collections;
using System;


public enum weaponTypes
{
    Pistol = 0,
    SMG = 1,
    Shotgun = 2,
    Rifle = 3,
    Rocket = 4,
    Grenade = 5   //Added fore grenades
}

//Rarity based on percentage
public enum weaponRarity
{
    Common = 100,
    Uncommon = 45,
    Rare = 15,
    Legendary = 3
}

//Classes, currently unused
/*
public enum weaponClasses
{
    Bullet = 0, //Currently the only in use
    Laser = 1,
    Electric = 2,
    Kinetic = 3,
    
}
*/

//used for determing what collision layer bullets from this weapon should be on.
public enum weaponLayers
{
    Player = 8,
    Enemy = 9
}


public class Weapon : ScriptableObject, inventoryItem
{
    //Minimum values (for a lv 1 common)
    private static readonly float[] MINIMUM = {
    5.0F,   //0//Damage
    2.0F,   //1//bullet speed
    0.3F,   //2//rate of fire
    2.0F,   //3//bullet decay
    0.2F,   //4//bullet spread
    0.0F,   //5//burst count
    5.0F,   //6//clip
    1.0F    //7//reload
    };

    //Maximum values (for a lv 1 common)
    private static readonly float[] MAXIMUM = {
    30.0F,   //0//Damage
    10.0F,   //1//bullet speed
    1.5F,   //2//rate of fire
    5.0F,   //3//bullet decay
    1.0F,   //4//bullet spread
    1.0F,    //5//burst count
    30.0F,   //6//clip
    4.0F    //7//reload
    };

    [SerializeField]
    public int level = 1; //Upgrade level of weapon

    public int upgradePoints = 0; //Points to increase weapon stats

    public itemType inventoryItemType { get { return itemType.Weapon; } }

    [SerializeField]
    private GameObject Bullet;

    public bulletTypes bulletType = bulletTypes.Basic;

    [SerializeField]
    public float bulletSpeed { get; private set; }

    //Accuracy of the weapon, determines the spread
    [SerializeField]
    public float bulletSpread { get; private set; }

    //Number of shots per fire
    [SerializeField]
    public float burstCount { get; private set; }


    [SerializeField]
    public float bulletTime { get; private set; } //How long the bullet lasts

    [SerializeField]
    public float Damage { get; private set; }

    public float shotTimer { get; private set; } //Fire rate

    public float reloadTime { get; private set; } //Time to reload

    public bool reload { get; private set; } //Checks if reloading

    public float reloadTimer { get; private set; } //Counts up to the reload time

    public float clipSize { get; private set; }

    public float clip { get; private set; }

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
    private PlayerController player; //Will always be null??

    public void updateItem()
    {
        if (Timer < shotTimer)
            Timer += Time.deltaTime;

        if (clip <= 0)
            reload = true;

        Reload();
        Upgrade(); //Can throw this when upgrade screen is present only

        if (Input.GetKeyDown(KeyCode.Alpha0)) //Only for testing, will be moved to upgrade UI
        {
            LevelUp();
        }
    }

    public IEnumerator Fire(Transform initTransform)
    {
        if (Timer >= shotTimer && reload == false)
        {

            for (int i = 0; i < burstCount; i++)
            {
                GameObject temp = Instantiate(Bullet, initTransform.position, initTransform.rotation) as GameObject;
                temp.GetComponent<BulletController>().Initialize(bulletType, bulletSpeed, bulletTime, Damage + Power, Layer);
                clip--;
                Timer = 0f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void Reload()
    {
        if (reload == true)
        {
            reloadTimer += Time.deltaTime;
            Debug.Log("Reloading");

            if (reloadTimer >= reloadTime)
            {
                Debug.Log("Reloaded");
                reload = false;
                clip = clipSize;
                reloadTimer = 0;
            }
        }
    }

    public void Initialize(weaponLayers layerType)
    {
        //Set the randomized stats  
        float[] Stats = setRandStats();

        reload = false; //insuring that we don't start off having to reload
        reloadTimer = 0; //Starts the reload timer at 0 for when we need to reload;

        //new ones to be randomized
        this.clipSize = Stats[8];
        this.reloadTime = Stats[9];

        this.clip = clipSize; //throw this in once clip size is determined


        this.Bullet = Resources.Load("Prefabs/Bullet") as GameObject;

        //this.bulletType = Bullet.GetComponent<BulletController>().bulletType;  Removed due to new bullet type setting above.

        this.Layer = layerType;

        this.Damage = Stats[0];
        this.bulletSpeed = Stats[1];
        this.shotTimer = Stats[2];
        this.bulletTime = Stats[3];
        this.bulletSpread = Stats[4];
        this.burstCount = Stats[5];
        this.Type = (weaponTypes)Stats[6];
        this.Rarity = (weaponRarity)Stats[7];

        if (Type == weaponTypes.Grenade || Type == weaponTypes.Rocket)
            bulletType = bulletTypes.Explosive;
        else
        {
            bulletType = bulletTypes.Basic;
        }

        this.itemId = Guid.NewGuid();
    }

    public void Initialize(GameObject Bullet, float bulletDamage, float speed, float shotTime, float bulletDecay, float spread, float burst, float reloadTime, float clipSize, weaponTypes weaponType, weaponLayers layerType) //Allows the creation of weapon types
    {
        reload = false; //insuring that we don't start off having to reload
        reloadTimer = 0; //Starts the reload timer at 0 for when we need to reload;

        //new ones to be randomized
        this.clipSize = clipSize;
        this.reloadTime = reloadTime ;

        this.clip = clipSize; //throw this in once clip size is determined


        this.Bullet = Bullet;

        //this.bulletType = Bullet.GetComponent<BulletController>().bulletType;  Removed due to new bullet type setting above.

        this.Layer = layerType;

        this.Damage = bulletDamage;
        this.bulletSpeed = speed;
        this.shotTimer = shotTime;
        this.bulletTime = bulletDecay;
        this.bulletSpread = spread;
        this.burstCount = burst;
        this.Type = weaponType;
        //this.Rarity = (weaponRarity);

        if (Type == weaponTypes.Grenade || Type == weaponTypes.Rocket)
            bulletType = bulletTypes.Explosive;
        else
        {
            bulletType = bulletTypes.Basic;
        }

        this.itemId = Guid.NewGuid();

    }

    void LevelUp()
    {
        level++;
        upgradePoints += 5;
    }

    void Upgrade()
    {
        if (Input.anyKeyDown)
        {
            switch (Input.inputString) //Placeholder system till UI is implemented
            {
                case "y":
                    if (upgradePoints >= 1)
                    {
                        upgradePoints -= 1;
                        this.Damage += 5;
                    }
                    break;
                case "u":
                    if (upgradePoints >= 1)
                    {
                        upgradePoints -= 1;
                        this.bulletSpeed += 1;
                    }
                    break;
                case "i":
                    if (upgradePoints >= 1)
                    {
                        if (shotTimer >= 0.06)
                        {
                            upgradePoints -= 1;
                            this.shotTimer -= 0.05f;
                        }
                    }
                    break;
                case "o":
                    if (upgradePoints >= 1)
                    {
                        upgradePoints -= 1;
                        this.clipSize += 2;
                    }
                    break;
                case "p":
                    if (upgradePoints >= 1)
                    {
                        upgradePoints -= 1;
                        this.reloadTime -= 0.25f;
                    }
                    break;
            }
        }
    }

    void setStats(float iPower)
    {
        Power = iPower;
    }

    /// <summary>
    /// Sets the weapon stats based on character level and rarity level
    /// </summary>
    private float[] setRandStats()
    {
        //Random percentage
        int Rand = UnityEngine.Random.Range(1, 100);


        //Boost from type & rarity
        float[] Boost = {   1.0F,   //0//Damage
                            1.0F,   //1//bullet speed
                            1.0F,   //2//rate of fire
                            1.0F,   //3//bullet decay
                            1.0F,   //4//bullet spread
                            1.0F,   //5//burst count
                            1.0F,   //6//overall
                            1.0F,   //7//Level
                            1.0F,   //8//Clip size
                            1.0F  };//9//Reload
        //Array to hold stats
        float[] GunStats = {1.0F,   //0//Damage
                            1.0F,   //1//bullet speed
                            1.0F,   //2//rate of fire
                            1.0F,   //3//bullet decay
                            1.0F,   //4//bullet spread
                            1.0F,   //5//burst count
                            1.0F,   //6//Type
                            1.0F,   //7//Rarity 
                            1.0F,   //8//Clip size
                            1.0F  };//9//Reload

        //Set up the chances
        int[] RareValues = { (int)weaponRarity.Legendary, (int)weaponRarity.Rare, (int)weaponRarity.Uncommon, (int)weaponRarity.Common };

        //Find matching rarity
        if (Rand > RareValues[2])
        {
            GunStats[7] = (float)weaponRarity.Common;
            Boost[6] = 1.0F;
        }
        else if (Rand > RareValues[1])
        {
            GunStats[7] = (float)weaponRarity.Uncommon;
            Boost[6] = 1.3F;
        }
        else if (Rand > RareValues[0])
        {
            GunStats[7] = (float)weaponRarity.Rare;
            Boost[6] = 2.0F;
            Boost[5] = 2.0F;
        }
        else
        {
            GunStats[7] = (float)weaponRarity.Legendary;
            Boost[6] = 3.0F;
            Boost[5] = 3.0F;
        }

        //Determine boost by type
        Rand = UnityEngine.Random.Range(0, 4);
        GunStats[6] = (float)weaponTypes.Pistol;

        switch (Rand)
        {
            case 0:
                GunStats[6] = (float)weaponTypes.Pistol;
                Boost[0] = 0.6F;
                Boost[2] = 0.9F;
                Boost[3] = 0.8F;
                Boost[4] = 1.4F;
                Boost[8] = 0.9F;
                Boost[9] = 0.7F;
                break;
            case 1:
                GunStats[6] = (float)weaponTypes.SMG;
                Boost[0] = 0.8F;
                Boost[2] = 0.5F;
                Boost[3] = 0.9F;
                Boost[4] = 1.5F;
                Boost[8] = 1.3F;
                Boost[9] = 0.8F;
                break;

            case 2:
                GunStats[6] = (float)weaponTypes.Shotgun;
                Boost[0] = 0.4F;
                Boost[2] = 1.2F;
                Boost[3] = 0.6F;
                Boost[4] = 1.6F;
                Boost[5] *= 6.0F;
                Boost[8] = 0.7F;
                Boost[9] = 1.5F;
                break;

            case 3:
                GunStats[6] = (float)weaponTypes.Rifle;
                Boost[0] = 1.2F;
                Boost[2] = 0.4F;
                Boost[3] = 1.4F;
                Boost[4] = 1.6F;
                Boost[8] = 2.3F;
                Boost[9] = 1.2F;
                break;

            case 4:
                GunStats[6] = (float)weaponTypes.Rocket;
                Boost[0] = 10.0F;
                Boost[1] = 0.8F;
                Boost[2] = 2.0F;
                Boost[3] = 1.8F;
                Boost[4] = 1.2F;
                Boost[8] = 0.3F;
                Boost[9] = 2.3F;
                break;

        }

        //Player level
        // The following causes the Key error
        ////float PlayerLevel = player.Leveler.level;//

        //Determine boost by Player level
        //Removed due to KeyFound Error
        Boost[7] = 1.0F;


        //Set the stat, based on the equation a * b * c * d, where a is the randomly selected stat (from min to max), 
        // b is the respective boost from type, c is the level boost, and d is the rarity boost.
        // Needs work


        //Damage
        GunStats[0] = UnityEngine.Random.Range(MINIMUM[0], MAXIMUM[0]) * Boost[0] * Boost[7] * Boost[6];

        //1//bullet speed
        GunStats[1] = UnityEngine.Random.Range(MINIMUM[1], MAXIMUM[1]);

        //2//rate of fire
        GunStats[2] = (UnityEngine.Random.Range(MINIMUM[2], MAXIMUM[2]) * Boost[2]) / Boost[7] / Boost[6];

        //3//bullet decay
        GunStats[3] = UnityEngine.Random.Range(MINIMUM[3], MAXIMUM[3]);

        //4//bullet spread
        GunStats[4] = UnityEngine.Random.Range(MINIMUM[4], MAXIMUM[4]) * Boost[4] / Boost[7] / Boost[6];

        //5//burst count
        GunStats[5] = Boost[5] * UnityEngine.Random.Range((int)MINIMUM[5], (int)MAXIMUM[5]);
        if (GunStats[5] < 1.0F)
            GunStats[5] = 1.0F;

        //8//clip size
        GunStats[8] = UnityEngine.Random.Range(MINIMUM[6], MAXIMUM[6]) * Boost[8] * Boost[7] * Boost[6];

        //9//Reload time
        GunStats[9] = UnityEngine.Random.Range(MINIMUM[7], MAXIMUM[7]) * Boost[9] / Boost[7] / Boost[6];

        //Apply the effect
        return GunStats;


        //Stat setup completed
    }
}