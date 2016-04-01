using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float bulletSpeed = 5;

    [SerializeField]
    private float bulletTime = 3; //How long the bullet lasts

    private float damage = 5; //Default if no weapon is specifically set.

    public float shotTimer = 50; //Fire rate

    private int type = 9;

    public void Fire()
    {
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        temp.GetComponent<BulletController>().Initialize(bulletSpeed, bulletTime, damage, type);
    }

    public void WeaponSwap(WeaponHolder weapon) //Takes in the variables of the new weapon type.
    {
        damage = weapon.damage;
        bulletSpeed = weapon.speed;
        shotTimer = weapon.fireRate;
        bulletTime = weapon.decay;
        type = weapon.type;
    }

    public void Initialize(float bulletDamage, float speed, float shotTime, float bulletDecay, int layerType) //Allows the creation of weapon types
    {
        damage = bulletDamage;
        bulletSpeed = speed;
        shotTimer = shotTime;
        bulletTime = bulletDecay;
        type = layerType;
    }
}
