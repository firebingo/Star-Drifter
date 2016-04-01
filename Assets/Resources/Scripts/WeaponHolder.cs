using UnityEngine;
using System.Collections;

public class WeaponHolder : ScriptableObject
{
        public float damage;
        public float speed;
        public float fireRate;
        public float decay;
        public int type;

        public void initialize(float bulletDamage, float bulletSpeed, float shotTime, float bulletDecay, int layerType)
        {
            damage = bulletDamage;
            speed = bulletSpeed;
            fireRate = shotTime;
            decay = bulletDecay;
            type = layerType;
        }
}
