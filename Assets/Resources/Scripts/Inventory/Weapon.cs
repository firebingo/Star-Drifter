using UnityEngine;
using System.Collections;
using System;

public enum weaponTypes
{
    Pistol
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
    private float bulletSpeed;

    [SerializeField]
    private float bulletTime; //How long the bullet lasts

    [SerializeField]
    private float Damage;

    public float shotTimer; //Fire rate

    [SerializeField]
    private weaponTypes Type;

    [SerializeField]
    private weaponLayers Layer;

    [SerializeField]
    private float Power;

    private float Timer;

    public Guid itemId { get; private set; }  //a unique id for the weapon.

    public int count { get; set; }

    public string itemName { get; private set; }

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
}
