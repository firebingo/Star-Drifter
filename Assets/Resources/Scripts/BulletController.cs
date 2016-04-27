using UnityEngine;
using System.Collections;

public enum bulletTypes
{
    Basic
}

public class BulletController : MonoBehaviour
{
    public bulletTypes bulletType;

    private float bulletSpeed;

    private float bulletTime;

    private float bulletTimer;

    private float damage;

    /// <summary>
    /// Unity's Update Function
    /// </summary>
    void Update()
    {
        transform.position += transform.up.normalized * bulletSpeed * Time.deltaTime;

        if (bulletTimer >= bulletTime)
            Destroy(this.gameObject);

        bulletTimer += Time.deltaTime;
    }

    public void Initialize(bulletTypes bType, float speed, float time, float bulletDamage, weaponLayers type)
    {
        bulletType = bType;
        bulletSpeed = speed;
        bulletTime = time;
        damage = bulletDamage;
        gameObject.layer = (int)type; //Determines collision layers (8 = player vs 9 = enemy)
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.SendMessage("ApplyDamage", damage);
        Destroy(this.gameObject);
    }
}
